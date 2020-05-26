using System;
using TODORoutine.database.general.dao;
using TODORoutine.database.parsers;
using TODORoutine.Database.user.DAO;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.Database.user.DTO {

    /**
     * Main User Data Transfer Implementation that will handle that communication between class for the User 
     **/
    class UserDTOImplementation : UserDTO {

        private static UserDTO userDTO = null;
        private readonly UserDAO userDAO = null;
        
        private UserDTOImplementation() => userDAO = UserDAOImplementation.getInstance();

        public static UserDTO getInstance() {
            if (userDTO == null) userDTO = new UserDTOImplementation();
            return userDTO;
        }

        /**
        * Deleting the user from the Database
        * 
        * @id : the user id that will be deleted
        * 
        * return true if and only if the user was deleted successfully and false otherwise
        **/
        public bool delete(String id) {
            try {
                return userDAO.delete(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
        * Getting the user from it's id
        * 
        * @id : the user id to search for in the Database
        * 
        * return a User if and ony if it was found and null otherwise
        **/
        public User getById(String id) {
            try {
                return userDAO.findById(id);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }

        /**
        * Getting the user from it's username
        * 
        * @username : the user username to search for in the Database
        * 
        * return a User if and ony if it was found and null otherwise
        **/
        public User getByUsername(String username) {
            try {
                return userDAO.findByUsername(username);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }

        /**
         * Getting the user id based on it's username
         * 
         * @username : the user username to search for in the Database
         * 
         * return an id if and ony if it was found and "" otherwise
         **/
        public String getId(String username) {
            try {
                return userDAO.findUserId(username);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return "";
        }

        /**
         * Getting the user notesId based on it's id
         * 
         * @id : the user id to search for in the Database
         * 
         * return an notesId if and ony if it was found and -1
         **/
        public String getNotesId(String id) {
            try {
                return userDAO.findNotesId(id);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return "";
        }

        /**
         * Getting the user username based on it's id
         * 
         * @id : the user id to search for in the Database
         * 
         * return an username if and ony if it was found and -1
         **/
        public String getUsername(String id) {
            try {
                return userDAO.findUsername(id);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return "";
        }

        /**
         * Checking if the user is authenticated based on the username
         * 
         * @username : the username to search for in the Database
         * 
         * return true if the user is authenticated and false otherwise
         **/
        public bool isAuthenticated(String id) {
            try {
                return userDAO.isUserAuthenticated(id);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
        * Saving the user in the Database
        * 
        * @user : the user to save
        * 
        * return true if and only if the saving operation was successfull and false otherwise
        **/
        public bool save(User user) {
            try {
                bool flag = userDAO.save(user);
                if(flag) {
                    user.setId(DatabaseDAOImplementation<User>.getLastId(DatabaseConstants.TABLE_USER));
                    return true;
                }
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Updating User's Info in the Database
         * 
         * @user : the user to update
         * @columns : Info that will get updated
         * 
         * return true if and only if the update operation was successfull and false otherwise
         **/
        public bool update(User user , params String[] columns) {
            try {
                return userDAO.update(user , columns);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }
    }
}
