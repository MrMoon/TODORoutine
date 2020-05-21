﻿using System;
using System.Data.SQLite;
using TODORoutine.database.authentication.parser;
using TODORoutine.database.general;
using TODORoutine.database.parsers;
using TODORoutine.Database;
using TODORoutine.exceptions;
using TODORoutine.Shared;

namespace TODORoutine.database.authentication {
    class AuthenticationDAOImplentation : AuthenticationDAO {

        private readonly int KEY = 17;
        private static AuthenticationDAO authDAO = null;
        private DatabaseDriver driver = null;
        private AuthenticationParser parser = null;

        private AuthenticationDAOImplentation() {
            parser = AuthenticationParserImplentation.getInstance();
            driver = DatabaseDriverImplementation.getInstance();
            driver.createTable(DatabaseConstants.CREATE_AUTHENTICATE_TABLE);
        }

        public static AuthenticationDAO getInstance() {
            if (authDAO == null) authDAO = new AuthenticationDAOImplentation();
            return authDAO;
        }

        public bool login(Authentication auth) {
            //Logging
            Logging.paramenterLogging(nameof(login) , false , new Pair(nameof(auth) , auth.ToString()));
            //Login
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(DatabaseConstants.TABLE_AUTHENTICATE , 
                                                            DatabaseConstants.COLUMN_USERNAME , DatabaseConstants.COLUMN_PASSWORD , auth.getUsername()));
                if(reader.Read()) return derypt(reader[DatabaseConstants.COLUMN_PASSWORD].ToString()).Equals(auth.getPassword());
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(login) , true , new Pair(nameof(auth) , auth.ToString()));
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(auth.ToString()));
        }

        public bool register(Authentication auth) {
            //Logging
            Logging.paramenterLogging(nameof(register) , false , new Pair(nameof(auth) , auth.ToString()));
            //Login
            try {
                auth.setPassword(encrypt(auth.getPassword()));
                return driver.executeQuery(parser.getInsert(auth)) != -11;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(register) , true , new Pair(nameof(auth) , auth.ToString()));
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(auth.ToString()));
        }

        public String encrypt(String text , int key = 17) {
            String output = "";
            foreach (char c in text) output += cipher(c , key);
            return output;
        }

        public String derypt(String text) {
            return encrypt(text , 26 - KEY);
        }

        public char cipher(char ch , int key) {
            if (!char.IsLetter(ch)) return ch;
            char c = char.IsUpper(ch) ? 'A' : 'a';
            return (char) ((((ch + key) - c) % 26) + c);
        }
    }
}
