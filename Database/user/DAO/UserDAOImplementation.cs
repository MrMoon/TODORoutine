﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TODORoutine.database.general.dao;
using TODORoutine.database.general.driver;
using TODORoutine.database.general.exception;
using TODORoutine.database.general.shared;
using TODORoutine.database.user.parser;
using TODORoutine.general.logging;
using TODORoutine.models;

namespace TODORoutine.database.user.dao {

    /**
     * Main User Data Access Implementation that handle database operations for the User
     **/
    class UserDAOImplementation : DatabaseDAOImplementation<User> , UserDAO {

        private readonly String idColumn = DatabaseConstants.COLUMN_ID;
        private readonly String tableName = DatabaseConstants.TABLE_USER;
        private DatabaseDriver driver = null;
        private UserParser parser = null;
        private static UserDAO userDAO = null;

        private UserDAOImplementation() {
            Logging.singlton(nameof(UserDAO));
            driver = DatabaseDriverImplementation.getInstance();
            parser = UserParserImplementation.getInstance();
            driver.createTable(DatabaseConstants.CREATE_USER_TABLE);
        }

        public static UserDAO getInstance() {
            if (userDAO == null) userDAO = new UserDAOImplementation();
            return userDAO;
        }

        /**
        * Getting the user from the SQLiteReader
        * 
        * @reader : the SQLiteDataReader for access the database
        * 
        * return user object
        **/
        public override User find(SQLiteDataReader reader) {
            if(reader.Read()) {
                User user = new User();
                user.setFullName(reader[DatabaseConstants.COLUMN_FULLNAME].ToString());
                user.setIsAuthenticated(int.Parse(reader[DatabaseConstants.COLUMN_AUTH].ToString()));
                user.setNotebookId(reader[DatabaseConstants.COLUMN_NOTEBOOKID].ToString());
                user.setUsername(reader[DatabaseConstants.COLUMN_USERNAME].ToString());
                user.setId(reader[idColumn].ToString());
                return user;
            }
            throw new DatabaseException(DatabaseConstants.INVALID("No Row to Read"));
        }

        /**
         * Delete User from the database based on the id
         * 
         * @id : the user id to delete
         * 
         * return true if and only if the delete operation was done successfully and throw an exception otherwise
         **/
        public override bool delete(String id) {
            //Logging
            Logging.paramenterLogging(nameof(delete) , false , new Pair(nameof(id) , id));
            //Deleting the user
            try {
                driver.executeQuery(parser.getDelete(tableName , idColumn , id));
                return true;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(delete) , true , new Pair(nameof(id) , id));
            //User not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        /**
         * Find the user in the database based on the id
         * 
         * @id : the id of the user to search for
         * 
         * return a user if it was found and throw an Exception otherwise
         **/
        public override User findById(String id) {
            //Logging
            Logging.paramenterLogging(nameof(findById) , false , new Pair(nameof(id) , id));
            //Finding the user
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , idColumn , DatabaseConstants.ALL , id));
                User user = find(reader);
                Logging.logInfo(false , user.ToString());
                reader.Close();
                return user;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findById) , true , new Pair(nameof(id) , id));
            //User not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        /**
         * Getting user based on the username
         * 
         * @username : the username of the user to find
         * 
         * return a user if it was found and throw and exception otherwise
         **/
        public User findByUsername(String username) {
            //Logging
            Logging.paramenterLogging(nameof(findByUsername) , false , new Pair(nameof(username) , username));
            //Finding user
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName 
                                            , DatabaseConstants.COLUMN_USERNAME , DatabaseConstants.ALL , username));
                User user = find(reader);
                Logging.logInfo(false , user.ToString());
                reader.Close();
                return user;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findByUsername) , true , new Pair(nameof(username) , username));
            //User was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(username));
        }

        /**
         * Getting user id from it's username
         * 
         * @username : username of the user to search in the database
         * 
         * return user id if it was found and throw an exception otherwise
         **/
        public String findUserId(String username) {
            //Logging 
            Logging.paramenterLogging(nameof(findUserId) , false , new Pair(nameof(username) , username));
            //Finding id of the user
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName 
                                            , DatabaseConstants.COLUMN_USERNAME , idColumn , username));
                if(reader.Read()) {
                    String id = reader[idColumn].ToString();
                    reader.Close();
                    return id;
                }
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging 
            Logging.paramenterLogging(nameof(findUserId) , false , new Pair(nameof(username) , username));
            //User was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(username));
        }

        /**
        * Getting user notesId from it's id
        * 
        * @id : the id of the user to search in the database
        * 
        * return user notesid if it was found and throw an exception otherwise
        **/
        public String findNotebookId(String id) {
            //Logging 
            Logging.paramenterLogging(nameof(findNotebookId) , false , new Pair(nameof(id) , id));
            //Finding notesId of the user
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName 
                                        , idColumn , DatabaseConstants.COLUMN_NOTEBOOKID , id));
                if(reader.Read()) {
                    String notesId = reader[DatabaseConstants.COLUMN_NOTEBOOKID].ToString();
                    reader.Close();
                    return notesId;
                }
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            Logging.paramenterLogging(nameof(findNotebookId) , true , new Pair(nameof(id) , id));
            //User was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        /**
        * Getting user username from it's id
        * 
        * @id : the id of the user to search in the database
        * 
        * return user useranme if it was found and throw an exception otherwise
        **/
        public String findUsername(String id) {
            //Logging 
            Logging.paramenterLogging(nameof(findUsername) , false , new Pair(nameof(id) , id));
            //Finding username of the user
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , idColumn , DatabaseConstants.COLUMN_USERNAME , id));
                if(reader.Read()) {
                    String username = reader[DatabaseConstants.COLUMN_USERNAME].ToString();
                    reader.Close();
                    return username;
                }
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging 
            Logging.paramenterLogging(nameof(findUsername) , true , new Pair(nameof(id) , id));
            //User was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        /**
         * Checking if the user is Authenticated or not
         * 
         * @id : the id of the user to check
         * 
         * return true if the user is Authenticated and false if not
         **/
        public bool isUserAuthenticated(String id) {
            //Logging 
            Logging.paramenterLogging(nameof(isUserAuthenticated) , false , new Pair(nameof(id) , id));
            //Finding if the user is Authenticated
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , idColumn , DatabaseConstants.COLUMN_AUTH , id));
                if(reader.Read()) {
                    int isAuth = int.Parse(reader[DatabaseConstants.COLUMN_AUTH].ToString());
                    reader.Close();
                    return isAuth == 1;
                }
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging 
            Logging.paramenterLogging(nameof(isUserAuthenticated) , true , new Pair(nameof(id) , id));
            //User was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        /**
         * Saving the user in the database
         * 
         * @user : the user to save in the database
         * 
         * return true if and only if the user was saved successfully and false otherwise
         **/
        public override bool save(User user) {
            //Logging 
            Logging.paramenterLogging(nameof(save) , false , new Pair(nameof(user) , user.ToString()));
            //Saving the user
            try {
                return driver.executeQuery(parser.getInsert(user)) != -1;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Updating the user in the database 
         * 
         * @user : the user to update
         * @columns : the columns to update in the database
         * 
         * return true if the update was done successfully and false otherwise
         **/
        public override bool update(User user , params String[] columns) {
            //Logging 
            Logging.paramenterLogging(nameof(update) , false , new Pair(nameof(user) , user.ToString()));
            //Saving the user
            try {
                return driver.executeQuery(parser.getUpdate(tableName , idColumn , user.getId() , user , columns)) != -1;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Getting all users 
         * 
         * return users ids
         **/
        public List<string> findAll() {
            //Logging
            Logging.paramenterLogging(nameof(findAll) , false);
            return findAll(parser , tableName , "-1");
        }
    }
}
