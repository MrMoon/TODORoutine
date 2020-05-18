using System;
using System.Data.SQLite;
using TODORoutine.database.general;
using TODORoutine.database.parsers;
using TODORoutine.database.user.exceptions;
using TODORoutine.exceptions;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.Database.user.DAO {

    /**
     * Main User Data Access Implementation that handle database operations
     **/
    class UserDAOImplementation : UserDAO {

        private readonly String tableName = DatabaseConstants.TABLE_TODOROUTINE;
        private static UserDAO dao = null;
        private DatabaseParser parser = null;
        private DatabaseDriver driver = null;

        private UserDAOImplementation() {
            driver = DatabaseDriverImplementation.getInstance();
            parser = DatabaseParserImplementation.getInstance();
        }

        public static UserDAO getInstance() {
            if (dao == null) dao = new UserDAOImplementation();
            return dao;
        }

        /**
         * Reading the user from the database
         * 
         * @dataReader : SQLite Reader that read from the database
         * 
         * return the read User
         **/
        public User getUser(SQLiteDataReader dataReader) {
            User user = new User(dataReader[DatabaseConstants.COLUMN_USERID].ToString());
            user.setFullName(dataReader[DatabaseConstants.COLUMN_FULLNAME].ToString());
            user.setUsername(dataReader[DatabaseConstants.COLUMN_USERNAME].ToString());
            user.setNotesId(dataReader[DatabaseConstants.COLUMN_NOTESID].ToString());
            return user;
        }

        /**
         * inserting the user into the SQL Database
         * 
         * @user : the user that will get inserted
         * 
         * return ture if and only if the user was saved successfully
         **/
        public bool save(User user) {
            //Logging
            Logging.paramenterLogging(nameof(save) , false
                , new Pair(nameof(user) , user.toString()));
            //Inserting User into the Database
            try {
                driver.executeQuery(parser.getInsert(user));
            } catch (SQLiteException e) {
                Logging.logInfo(true , e.Data.ToString());
                return false;
            }
            return true;
        }

        /**
         * updatting the user in the SQL Database
         * 
         * @oldUser : the user that will get updated
         * @newUser : the user that will have the new values
         * 
         * return the number of affected records
         **/
        public bool update(User user , params String[] columns) {
            //Logging
            Logging.paramenterLogging(nameof(update) , false , new Pair(nameof(user) , user.toString()));
            //Updating
            try {
                driver.executeQuery(parser.getUpdate(tableName , DatabaseConstants.COLUMN_USERID , user.getId() , user , columns));
            } catch (SQLiteException e) {
                Logging.logInfo(true , e.Data.ToString());
                return false;
            }
            return true;
        }

        /**
        * deleting the user from the Database
        * 
        * @user : the user that will get deleted
        * 
        * return the number of affected record
        **/
        public bool delete(User user) {
            //Logging
            Logging.paramenterLogging(nameof(delete) , false
                , new Pair(nameof(user) , user.toString()));
            //Deleting user from database
            try {
               driver.executeQuery(parser.getDelete(tableName , DatabaseConstants.COLUMN_USERID , user.getId()));
            } catch(SQLiteException e) {
                Logging.logInfo(true , e.Data.ToString());
                return false;
            }
            return true ;
        }


        /**
         * Getting the user from the database based on the id
         * 
         * @id : the user id to search for
         * 
         * return User if found and throw an Exception otherwise
         **/
        public User findById(String id) {
            //Logging
            Logging.paramenterLogging(nameof(findById) , false , new Pair(nameof(id) , id));
            //Getting the user
            SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName ,
                                            DatabaseConstants.COLUMN_USERID , DatabaseConstants.ALL , id));
            //Reading the the Record from the database
            while (reader.Read()) {
                User user = getUser(reader);
                Logging.logInfo(false , nameof(findById) , UserConstants.USER_FOUND , user.toString());
                reader.Close();
                return user;
            }
            reader.Close();
            //Logging
            Logging.paramenterLogging(nameof(findById) , true , new Pair(nameof(id) , id));
            //User not found in the database
            throw new DatabaseException(UserConstants.NOT_FOUND(id));
        }

        /**
        * Getting the user from the database based on the username
        * 
        * @username : the user username to search for
        * 
        * return User if found and throw an Exception otherwise
        **/
        public User findByUsername(String username) {
            //Logging
            Logging.paramenterLogging(nameof(findByUsername) , false
                , new Pair(nameof(username) , username));
            //Getting the user
            String s = parser.getSelect(tableName , DatabaseConstants.COLUMN_USERNAME , DatabaseConstants.ALL , username);
            SQLiteDataReader reader = driver.getReader(s);
            while (reader.Read()) {
                User user = getUser(reader);
                Logging.logInfo(false , nameof(findByUsername) , UserConstants.USER_FOUND , user.toString());
                reader.Close();
                return user;
            }
            reader.Close();
            //Logging
            Logging.paramenterLogging(nameof(findByUsername) , true
                , new Pair(nameof(username) , username));
            //User not found in the database
            throw new DatabaseException(UserConstants.NOT_FOUND(username));
        }

        /**
         * User ID from the database based on the username
         * 
         * @username : the username in the select statment
         * 
         * Return an id for the user
         **/
        public String findUserId(String username) {
            //Logging
            Logging.paramenterLogging(nameof(findUserId) , false
                , new Pair(nameof(username) , username));
            //Finding the id
            String query = parser.getSelect(tableName , DatabaseConstants.COLUMN_USERNAME , DatabaseConstants.COLUMN_USERID , username);
            SQLiteDataReader reader = driver.getReader(query);
            while (reader.Read()) {
                String id = reader[DatabaseConstants.COLUMN_USERID].ToString();
                Logging.logInfo(false , nameof(findUserId) , id);
                reader.Close();
                return id;
            }
            reader.Close();
            //Logging
            Logging.paramenterLogging(nameof(findUserId) , true
                , new Pair(nameof(username) , username));
            //User not found in the database
            throw new DatabaseException(UserConstants.NOT_FOUND(username));
        }
        /**
         * User Notes Id from the database based on the id
         * 
         * @id : the id in the select statment
         * 
         * Return the notesId for the user if it was founded
         **/
        public String findUserNotesId(String id) {
            //Logging
            Logging.paramenterLogging(nameof(findUserId) , false
                , new Pair(nameof(id) , id));
            //Finding the notesId
            String query = parser.getSelect(tableName , DatabaseConstants.COLUMN_USERNAME , DatabaseConstants.COLUMN_NOTESID , id);
            SQLiteDataReader reader = driver.getReader(query);
            while (reader.Read()) {
                String notesId = reader[DatabaseConstants.COLUMN_NOTESID].ToString();
                Logging.logInfo(false , nameof(findUserNotesId) , notesId);
                reader.Close();
                return notesId;
            }
            reader.Close();
            //Logging
            Logging.paramenterLogging(nameof(findUserNotesId) , true
                , new Pair(nameof(id) , id));
            //User not found in the database
            throw new DatabaseException(UserConstants.NOT_FOUND(id));
        }

        /**
         * User username based on the id
         * 
         * @id : the id in the select statment
         * 
         * Return the username for the user if it was founded
         **/
        public String findUserUsername(String id) {
            //Logging
            Logging.paramenterLogging(nameof(findUserUsername) , false
                , new Pair(nameof(id) , id));
            //Finding the username
            String query = parser.getSelect(tableName , DatabaseConstants.COLUMN_USERNAME , DatabaseConstants.COLUMN_USERNAME , id);
            SQLiteDataReader reader = driver.getReader(query);
            while(reader.Read()) {
                String username = reader[DatabaseConstants.COLUMN_USERNAME].ToString();
                Logging.logInfo(false , nameof(findUserUsername) , username);
                reader.Close();
                return username;
            }
            reader.Close();
            //Logging
            Logging.paramenterLogging(nameof(findUserUsername) , true
                , new Pair(nameof(id) , id));
            //User not found in the database
            throw new DatabaseException(UserConstants.NOT_FOUND(id));
        }

        /**
         * Checks the database if the user is Authenticated or not
         * 
         * @username : the username of the user that we will check
         * 
         * return true if and only if the user is authenticated
         **/
        public bool isUserAuthenticated(String username) {
            //Logging
            Logging.paramenterLogging(nameof(isUserAuthenticated) , false
                , new Pair(nameof(username) , username));
            //Checking if user is Authenticated
            String query = parser.getSelect(tableName , DatabaseConstants.COLUMN_USERNAME , DatabaseConstants.COLUMN_AUTH , username);
            SQLiteDataReader reader = driver.getReader(query);
            while (reader.Read()) {
                int isAuth = int.Parse(reader[DatabaseConstants.COLUMN_AUTH].ToString());
                Logging.logInfo(false , nameof(isUserAuthenticated) , username);
                reader.Close();
                return isAuth == 1 ? true : false;
            }
            reader.Close();
            //Logging
            Logging.paramenterLogging(nameof(isUserAuthenticated) , true
                , new Pair(nameof(username) , username));
            //User not found in the database
            throw new DatabaseException(UserConstants.NOT_FOUND(username));
        }
    }
}
