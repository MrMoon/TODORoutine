using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.general;
using TODORoutine.database.parsers;
using TODORoutine.database.user.exceptions;
using TODORoutine.Database.Shared;
using TODORoutine.Database.user.DTO;
using TODORoutine.exceptions;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.Database.user.DAO {
    /**
     * Main User Data Access Class that handle database operations
     **/
    class UserDAOImplementation : UserDAO {

        private static UserDAO dao = null;
        private static UserDTO dto = null;
        private static DatabaseParser parser = null;
        private static DatabaseDriver driver = null;

        private UserDAOImplementation() {
            driver = DatabaseDriverImplementation.getInstance();
            parser = DatabaseParserImplementation.getInstance();
            dto = UserDTOImplementation.getInstance();
        }

        public static UserDAO getInstance() {
            if (dao == null) dao = new UserDAOImplementation();
            return dao;
        }

        /**
         * Getting the user from the database based on the id
         * 
         * @id : the user id to search for
         * 
         * return User if found and throw an Exception otherwise
         **/
        public User findById(String id) {
            if (!DatabaseValidator.isValidParameters(id))
                throw new ArgumentException(UserConstants.INVALID(DatabaseConstants.COLUMN_USERID) 
                    + Logging.paramenterLogging(nameof(findById) , true , new Pair(nameof(id) , id)));
            //Logging
            Logging.paramenterLogging(nameof(findById) , false , new Pair(nameof(id) , id));
            //Getting the user
            SQLiteDataReader dataReader = driver.getReader(parser.getSelect(DatabaseConstants.TABEL_TODOROUTINE ,
                                            DatabaseConstants.COLUMN_USERID , DatabaseConstants.ALL , id));
            while (dataReader.Read()) {
                User user = getUser(dataReader);
                Logging.logInfo(nameof(findById) , UserConstants.USER_FOUND , user.toString());
                return user;
            }
            //Logging
            Logging.paramenterLogging(nameof(findById) , true , new Pair(nameof(id) , id));
            //User not found in the database
            throw new UserException(UserConstants.USER_NOT_FOUND);
        }
        /**
        * Getting the user from the database based on the username
        * 
        * @username : the user username to search for
        * 
        * return User if found and throw an Exception otherwise
        **/
        public User findByUsername(String username) {
            if(!DatabaseValidator.isValidParameters(username)) 
                throw new ArgumentException(UserConstants.INVALID(DatabaseConstants.COLUMN_USERNAME)
                + Logging.paramenterLogging(nameof(findByUsername) , true , new Pair(nameof(username) , username)));
            //Logging
            Logging.paramenterLogging(nameof(findByUsername) , false
                , new Pair(nameof(username) , username));
            //Getting the user
            SQLiteDataReader dataReader = driver.getReader(parser.getSelect(DatabaseConstants.TABEL_TODOROUTINE ,
                                            DatabaseConstants.COLUMN_USERNAME , DatabaseConstants.ALL , username));
            while(dataReader.Read()) {
                User user = getUser(dataReader);
                Logging.logInfo(nameof(findByUsername) , UserConstants.USER_FOUND , user.toString());
                return user;
            }
            //Logging
            Logging.paramenterLogging(nameof(findByUsername) , true
                , new Pair(nameof(username) , username));
            //User not found in the database
            throw new UserException(UserConstants.USER_NOT_FOUND);
        }
        /**
         * Reading the user from the database
         * 
         * @dataReader : SQLite Reader that read from the database
         * 
         * return the read User
         **/
        private User getUser(SQLiteDataReader dataReader) {
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
            if (user == null) throw new ArgumentException(UserConstants.USER_INVLAID
                + Logging.paramenterLogging(nameof(save) , true  , new Pair(nameof(user) , user.toString())));
            //Logging
            Logging.paramenterLogging(nameof(save) , false
                , new Pair(nameof(user) , user.toString()));
            //Inserting User into the Database
            return driver.executeQuery(parser.getInsert(user)) > 0;
        }
        /**
         * updatting the user in the SQL Database
         * 
         * @oldUser : the user that will get updated
         * @newUser : the user that will have the new values
         * 
         * return the number of affected records
         **/
        public int update(User oldUser , User newUser) {
            if (oldUser == null || newUser == null || !newUser.getId().Equals(oldUser.getId())) 
                throw new ArgumentException(UserConstants.USER_INVLAID + 
                Logging.paramenterLogging(nameof(update) , true
                , new Pair(nameof(oldUser) , oldUser.toString()) 
                , new Pair(nameof(newUser) , newUser.toString())));
            //Logging
            Logging.paramenterLogging(nameof(update) , true
                , new Pair(nameof(oldUser) , oldUser.toString())
                , new Pair(nameof(newUser) , newUser.toString()));
            //Updating User
            ArrayList userDifferentColumns = dto.compare(oldUser , newUser);
            return driver.executeQuery(parser.getUpdate(DatabaseConstants.TABEL_TODOROUTINE , DatabaseConstants.COLUMN_USERID
                                        , newUser.getId() , userDifferentColumns , newUser));
        }
        /**
        * deleting the user from the Database
        * 
        * @user : the user that will get deleted
        * 
        * return the number of affected record
        **/
        public int delete(User user) {
            if (user == null) throw new ArgumentException(UserConstants.USER_INVLAID
                + Logging.paramenterLogging(nameof(delete) , true , new Pair(nameof(user) , user.toString())));
            //Logging
            Logging.paramenterLogging(nameof(delete) , false
                , new Pair(nameof(user) , user.toString()));
            //Deleting user from database
            return driver.executeQuery(parser.getDelete(DatabaseConstants.TABEL_TODOROUTINE
                , DatabaseConstants.COLUMN_USERID , user.getId()));
        }
    }
}
