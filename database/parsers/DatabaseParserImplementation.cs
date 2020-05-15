using System;
using System.Collections;
using System.Text;
using TODORoutine.database.parsers;
using TODORoutine.Database.Shared;
using TODORoutine.Models;

namespace TODORoutine.Shared {
    /**
     * Database Statment Parser Class that handle SQL Statments
     **/
    class DatabaseParserImplementation : DatabaseParser {

        private static DatabaseParserImplementation databaseParser = null;

        public static DatabaseParserImplementation getInstance() {
            if (databaseParser == null) databaseParser = new DatabaseParserImplementation();
            return databaseParser;
        }
        /**
         * This method is for Generic SQL Where Query Statments
         * @filter : the filter for the Where statment
         * @condition : the condition for the Where statment
         * 
         * return an SQL Where Statment
         **/
        public String getWhere(String filter , String condition) {
            //Logging
            Logging.paramenterLogging(nameof(getWhere) , false , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition));
            //Building the SQL Statment
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" WHERE ");
            stringBuilder.Append(filter);
            stringBuilder.Append(" = ");
            stringBuilder.Append(condition);
            return stringBuilder.ToString();
        }
        /**
         * This method is for Generic SQL Select Query Statments
         * @tableName : the table Name in the Database
         * @filter : the filter for the Where statment
         * @condition : the condition for the Where statment
         * @column : the column name in the database
         * 
         * It Throws and Exception when one of the parameters are invalid
         * 
         * return an SQL Select Statment
         **/
        public String getSelect(String tableName , String filter = "" , String column = "*" , String condition = "") {
            if (!DatabaseValidator.isValidParameters(tableName))
                throw new ArgumentException("Invalid Parameters in getSelect\n" + Logging.paramenterLogging(nameof(getSelect)  , true 
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition)
                                            , new Pair(nameof(column) , column)));
            //Logging
            Logging.paramenterLogging(nameof(getSelect) , false , new Pair(nameof(tableName) , tableName)
                                        , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition)
                                        , new Pair(nameof(column) , column));
            //Building the SQL Statment
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT ");
            stringBuilder.Append(column);
            stringBuilder.Append(" FROM ");
            stringBuilder.Append(tableName);
            if (filter != "") stringBuilder.Append(getWhere(filter , condition));
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }
        /**
         * This method is a generic SQL Delete Query statment
         * 
         * @tableName : The Table Name in the Database
         * @filter : the filter for the Where Statment
         * @condition : the condition for the Where statment
         * 
         * It Throws and Exception when one of the parameters are invalid
         * 
         * return an SQL Delete Statment
         **/
        public String getDelete(String tableName , String filter , String condition) {
            if (!DatabaseValidator.isValidParameters(tableName , filter , condition))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getDelete) , true , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition)));
            //Logging
            Logging.paramenterLogging(nameof(getDelete) , false , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition));
            //Building the SQL Statment
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("DELETE FROM ");
            stringBuilder.Append(tableName);
            stringBuilder.Append(getWhere(filter , condition));
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }
        /**
        * This method is a generic SQL Insert Query statment
        * 
        * @tableName : The Table Name in the Database
        * @user : the user object to be inserted in the database
        * It Throws and Exception when one of the parameters are invalid
        * 
        * return an SQL Insert Statment
        **/
        public String getInsert(User user) {
            if (!DatabaseValidator.isValidUser((user)))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true , 
                    new Pair(nameof(user) , user.toString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(user) , user.toString()));
            //Building the SQL Statment 
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("INSERT INTO ");
            stringBuilder.Append(DatabaseConstants.TABEL_TODOROUTINE);
            stringBuilder.Append(" ( ");
            stringBuilder.Append(DatabaseConstants.COLUMN_USERID);
            stringBuilder.Append(" , ");
            stringBuilder.Append(DatabaseConstants.COLUMN_USERNAME);
            stringBuilder.Append(" , ");
            stringBuilder.Append(DatabaseConstants.COLUMN_NOTESID);
            stringBuilder.Append(" , ");
            stringBuilder.Append(DatabaseConstants.COLUMN_FULLNAME);
            stringBuilder.Append(") VALUES ('");
            stringBuilder.Append(user.getId());
            stringBuilder.Append("',");
            stringBuilder.Append(user.getUsername());
            stringBuilder.Append("',");
            stringBuilder.Append(user.getNotesId());
            stringBuilder.Append("',");
            stringBuilder.Append(user.getFullName());
            stringBuilder.Append("');");
            return stringBuilder.ToString();
        }
        /**
        * This method is a generic SQL Update Query statment
        * 
        * @tableName : The Table Name in the Database
        * @filter : the filter for the Where Statment
        * @condition : the condition for the Where statment
        * @column : the column name in the database
        * @id : User id that will be updated
        * It Throws and Exception when one of the parameters are invalid
        * 
        * return an SQL Update Statment
        **/
        public String getUpdate(String tableName , String filter , String condition , ArrayList columns , User newUser) {
            if (!DatabaseValidator.isValidParameters(tableName , filter)
                && !DatabaseValidator.isValidParameters(columns.ToArray())
                && !DatabaseValidator.isValidUser(newUser))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getUpdate) , true 
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(columns) , columns.ToString())
                                            , new Pair(nameof(newUser) , newUser.toString())));

            Logging.paramenterLogging(nameof(getUpdate) , true
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(columns) , columns.ToString())
                                            , new Pair(nameof(newUser) , newUser.toString()));

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE ");
            stringBuilder.Append(tableName);
            stringBuilder.Append("SET ");
            for (int i = 0 ; i < columns.Count ; ++i) {
                stringBuilder.Append(columns[i]);
                stringBuilder.Append(" = ");
                stringBuilder.Append("' ");
                stringBuilder.Append(DatabaseUserParser.getColumnFromUserObject(newUser , columns[i].ToString()));
                stringBuilder.Append("' ");
                if (i != columns.Count - 1) stringBuilder.Append(",");
            }
            stringBuilder.Append(getWhere(filter , condition));
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }
    }
}

