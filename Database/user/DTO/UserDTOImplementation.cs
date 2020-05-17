using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Database.Shared;
using TODORoutine.Database.user.DAO;
using TODORoutine.exceptions;
using TODORoutine.Models;
using TODORoutine.Shared;
using TODORoutine.database.user.exceptions;
using TODORoutine.database.general;
using TODORoutine.database.parsers;

namespace TODORoutine.Database.user.DTO {

    /**
     * Main User Data Transfer Implementation that will handle that communication between class for the User 
     **/
    class UserDTOImplementation : UserDTO {

        private static UserDTO dto = null;
        private static UserDAO dao = null;
        private static DatabaseDriver driver = null;
        private static DatabaseParser parser = null;

        private UserDTOImplementation() {
            driver = DatabaseDriverImplementation.getInstance();
            parser = DatabaseParserImplementation.getInstance();
            dao = UserDAOImplementation.getInstance();
        }

        public static UserDTO getInstance() {
            if (dto == null) dto = new UserDTOImplementation();
            return dto;
        }

        /**
         * Deleting the user from the Database
         * 
         * @user : the user that will be deleted
         * 
         * return true if and only if the user was deleted successfully and false otherwise
         **/
        public bool deleteUser(User user) {
            try {
                dao.delete(user);
            } catch(Exception e) {
                return false;
            }
            return true;
        }

        /**
         * Getting the user from it's id
         * 
         * @id : the user id to search for in the Database
         * 
         * return a User if and ony if it was found and null otherwise
         **/
        public User getUserById(string id) {
            User user;
            try {
                user = dao.findById(id);
            } catch(Exception e) {
                return null;
            }
            return user;
        }

        /**
         * Getting the user from it's username
         * 
         * @username : the user username to search for in the Database
         * 
         * return a User if and ony if it was found and null otherwise
         **/
        public User getUserByUsername(string username) {
            User user;
            try {
                user = dao.findByUsername(username);
            } catch(Exception e) {
                return null;
            }
            return user;
        }

        /**
         * Getting the user id based on it's username
         * 
         * @username : the user username to search for in the Database
         * 
         * return an id if and ony if it was found and -1
         **/
        public string getUserId(string username) {
            String id;
            try {
                id = dao.findUserId(username);
            } catch(Exception e) {
                if (e is UserException) return UserConstants.USER_FOUND;
                return "-1";
            }
            return id;
        }

        /**
         * Getting the user notesId based on it's id
         * 
         * @id : the user id to search for in the Database
         * 
         * return an notesId if and ony if it was found and -1
         **/
        public string getUserNotesId(string id) {
            String notesid;
            try {
                notesid = dao.findUserNotesId(id);
            } catch(Exception e) {
                if (e is UserException) return UserConstants.USER_FOUND;
                return "-1";
            }
            return notesid;
        }

        /**
         * Getting the user username based on it's id
         * 
         * @id : the user id to search for in the Database
         * 
         * return an username if and ony if it was found and -1
         **/
        public string getUserUsername(string id) {
            String username;
            try {
                username = dao.findUserUsername(id);
            } catch (Exception e) {
                if (e is UserException) return UserConstants.USER_FOUND;
                return "-1";
            }
            return username;
        }

        /**
         * Checking if the user is authenticated based on the username
         * 
         * @username : the username to search for in the Database
         * 
         * return 1 if the user is Authenticated , 0 if not , and -1 if the there is an Error from the Database
         **/
        public int isAuthenticated(string username) {
            bool flag = false;
            try {
                flag = dao.isUserAuthenticated(username);
            } catch(Exception e) {
                return -1;
            }
            return flag ? 1 : 0;
        }

        /**
         * Saving the user in the Database
         * 
         * @user : the user to save
         * 
         * return true if and only if the saving operation was successfull and false otherwise
         **/
        public bool saveUser(User user) {
            try {
                dao.save(user);
            } catch(Exception e) {
                return false;
            }
            return true;
        }

        /**
         * Updating User's Info in the Database
         * 
         * @user : the user to update
         * @columns : Info that will get updated
         * 
         * return true if and only if the update operation was successfull and false otherwise
         **/
        public bool updateUser(User user , params string[] columns) {
            try {
                dao.update(user , columns);
            } catch(Exception e) {
                return false;
            }
            return true;
        }
    }
}
