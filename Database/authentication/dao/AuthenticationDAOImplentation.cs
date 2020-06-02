using System;
using System.Data.SQLite;
using TODORoutine.database.authentication.parser;
using TODORoutine.database.general.driver;
using TODORoutine.database.general.exception;
using TODORoutine.database.general.shared;
using TODORoutine.general.logging;
using TODORoutine.models;

namespace TODORoutine.database.authentication.dao {

    /**
     * Main Authentication Data Layer Implementation
     * Handles user authentication from the data layer
     **/
    class AuthenticationDAOImplementation : AuthenticationDAO {

        private readonly int KEY = 17;
        private static AuthenticationDAO authDAO = null;
        private DatabaseDriver driver = null;
        private AuthenticationParser parser = null;

        private AuthenticationDAOImplementation() {
            Logging.singlton(nameof(AuthenticationDAO));
            parser = AuthenticationParserImplementation.getInstance();
            driver = DatabaseDriverImplementation.getInstance();
            driver.createTable(DatabaseConstants.CREATE_AUTHENTICATE_TABLE);
        }

        public static AuthenticationDAO getInstance() {
            if (authDAO == null) authDAO = new AuthenticationDAOImplementation();
            return authDAO;
        }

        /**
         * Login for the user
         * 
         * @auth : the usernamae and password for the user (the authentication object)
         * 
         * return true if and only if the login was done successfully
         **/
        public bool login(Authentication auth) {
            //Logging
            Logging.paramenterLogging(nameof(login) , false , new Pair(nameof(auth) , auth.ToString()));
            //Login
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(DatabaseConstants.TABLE_AUTHENTICATE , 
                                                            DatabaseConstants.COLUMN_USERNAME , DatabaseConstants.COLUMN_PASSWORD , auth.getUsername()));
                if(reader.Read()) return decrypt(reader[DatabaseConstants.COLUMN_PASSWORD].ToString()).Equals(auth.getPassword());
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(login) , true , new Pair(nameof(auth) , auth.ToString()));
            //Something went wrong
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(auth.ToString()));
        }

        /**
         * Register for the user
         * 
         * @auth : the usernamae and password for the user (the authentication object)
         * 
         * return true if and only if the register was done successfully
         **/
        public bool register(Authentication auth) {
            //Logging
            Logging.paramenterLogging(nameof(register) , false , new Pair(nameof(auth) , auth.ToString()));
            //Register
            try {
                auth.setPassword(encrypt(auth.getPassword()));
                return driver.executeQuery(parser.getInsert(auth)) != -11;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(register) , true , new Pair(nameof(auth) , auth.ToString()));
            //Something went wrong
            throw new DatabaseException(DatabaseConstants.INVALID(auth.ToString()));
        }

        /**
         * Basic Encryption based on the Caesar Cipher
         * 
         * @text : the text to encrypt
         * @key : the key of encryption
         * 
         * return an encrypted String
         **/
        private String encrypt(String text , int key = 17) {
            //Logging
            Logging.paramenterLogging(nameof(encrypt) , false
                , new Pair(nameof(text) , text) , new Pair(nameof(key) , key.ToString()));
            //Encription
            String output = "";
            foreach (char c in text) output += cipher(c , key);
            return output;
        }

        /**
        * Basic Decryption based on the Caesar Cipher
        * 
        * @text : the text to encrypt
        * 
        * return an derypted String
        **/
        private String decrypt(String text) {
            //Logging
            Logging.paramenterLogging(nameof(decrypt) , false
                , new Pair(nameof(text) , text));
            //Decription
            return encrypt(text , 26 - KEY);
        }

        /**
         * The Caesar Cipher algorithm
         * 
         * @ch : the character ti encrypt
         * @key : the key of encryption
         * 
         * return the encrypted character
         **/
        private char cipher(char ch , int key) {
            //Logging
            Logging.paramenterLogging(nameof(cipher) , false
                , new Pair(nameof(ch) , ch.ToString()) , new Pair(nameof(key) , key.ToString()));
            //applying cipher
            if (!char.IsLetter(ch)) return ch;
            char c = char.IsUpper(ch) ? 'A' : 'a';
            return (char) ((((ch + key) - c) % 26) + c);
        }

        /**
         * Deleting the authentication for the user 
         * 
         * @username : the username of the user to delete the authentication
         * 
         * return ture if and only if the delete was done successfully
         **/
        public bool delete(String username) {
            //Logging
            Logging.paramenterLogging(nameof(delete) , false , new Pair(nameof(username) , username));
            //deleting from the database
            try {
                return driver.executeQuery(parser.getDelete(DatabaseConstants.TABLE_AUTHENTICATE , DatabaseConstants.COLUMN_USERNAME , username)) != -11;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(register) , true , new Pair(nameof(username) , username));
            //Username not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(username));
        }
    }
}
