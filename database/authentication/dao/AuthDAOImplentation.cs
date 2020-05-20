using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.authentication;
using TODORoutine.database.authentication.parser;
using TODORoutine.database.general;
using TODORoutine.database.parsers;
using TODORoutine.Database;
using TODORoutine.exceptions;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.database.authentication.dao {
    /**
     * Main Data Layer Implentation for authentication
     **/
    class AuthDAOImplentation : AuthDAO {

        private readonly String tableName = DatabaseConstants.TABLE_AUTHENTICATE;
        private readonly String usernameColumn = DatabaseConstants.COLUMN_USERNAME;
        private readonly String passwordColumn = DatabaseConstants.COLUMN_PASSWORD;
        private static AuthDAO authDAO = null;
        private AuthParser parser = null;
        private DatabaseDriver driver = null;

        private AuthDAOImplentation() {
            driver = DatabaseDriverImplementation.getInstance();
            parser = AuthParserImplementation.getInstance();
            driver.createTable(DatabaseConstants.CREATE_AUTHENTICATE_TABLE);
        }

        public static AuthDAO getInstance() {
            if (authDAO == null) authDAO = new AuthDAOImplentation();
            return authDAO;
        }

        public bool login(string username , string password) {
            //Logging
            Logging.paramenterLogging(nameof(login) , false , new Pair(nameof(username) , username) , new Pair(nameof(password) , password));
            //Encryption for the password
            password = Authenticate.encrypt(password);
            //Logging in
            try {
                return driver.executeQuery(parser.getSelect(tableName , usernameColumn , passwordColumn , password)) >= 0;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }

            throw new DatabaseException(DatabaseConstants.INVALID(password));

        }

        public bool register(string username , string password) {
            //Logging
            Logging.paramenterLogging(nameof(login) , false , new Pair(nameof(username) , username) , new Pair(nameof(password) , password));
            //Encryption for the password
            password = Authenticate.encrypt(password);
            //Logging in
            try {
                return driver.executeQuery(parser.getInsert(new Authenticate(username , password))) >= 0;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }

            throw new DatabaseException(DatabaseConstants.INVALID(username));
        }
    }
}
