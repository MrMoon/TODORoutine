using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Database.Shared;
using TODORoutine.Models;

namespace TODORoutine.Shared {
    class Constants {
        //Main Database Name
        public static String DATABASE_NAME = "TODORoutine.sqlite";
        //Table TODORoutine Strings
        public readonly static String TABEL_TODOROUTINE = "TODORoutine";
        public readonly static String COLUMN_USERID = "USERID";
        public readonly static String COLUMN_USERNAME = "USERNAME";
        public readonly static String COLUMN_NOTESID = "NOTESID";
        public readonly static String COLUMN_FULLNAME = "FULLNAME";
        public readonly static String CREATE_TABLE = @"CREATE TABLE" + TABEL_TODOROUTINE + "("
                                            + COLUMN_USERID + "TEXT NOT NULL UNIQUE PRIMARY KEY,"
                                            + COLUMN_USERNAME + "TEXT NOT NULL UNIQUE,"
                                            + COLUMN_NOTESID + "TEXT NOT NULL UNIQUE,"
                                            + COLUMN_FULLNAME + "TEXT NOT NULL);";
        public static String CONNECTION_STRING = "Data Source = TODORoutine.sqlite; Version = 3;";

        //SQL Statments methods

        /**
         * This method is for Generic SQL Where Query Statments
         * @filter : the filter for the Where statment
         * @condition : the condition for the Where statment
         * 
         * return an SQL Where Statment
         **/
        private static String getWhere(String filter , String condition) {
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
        public static String getSelect(String tableName , String filter = "" , String column = "*" , String condition = "") {
            if (!DatabaseValidator.isValidParameters(tableName))
                throw new ArgumentException("Invalid Parameters in getSelect\n" + Logging.paramenterLogging(new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition)
                                            , new Pair(nameof(column) , column)));

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
        public static String getDelete(String tableName , String filter , String condition) {
            if (!DatabaseValidator.isValidParameters(tableName , filter , condition))
                throw new ArgumentException(Logging.paramenterLogging(new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition)));

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
        public static String getInsert(String tableName , User user) {
            if (!DatabaseValidator.isValidParameters(tableName) || !DatabaseValidator.isValidUser((user)))
                throw new ArgumentException(Logging.paramenterLogging(new Pair(nameof(tableName) , tableName) , new Pair(nameof(user) , user.toString())));

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("INSERT INTO ");
            stringBuilder.Append(tableName);
            stringBuilder.Append(" ( ");
            stringBuilder.Append(Constants.COLUMN_USERID);
            stringBuilder.Append(" , ");
            stringBuilder.Append(Constants.COLUMN_USERNAME);
            stringBuilder.Append(" , ");
            stringBuilder.Append(Constants.COLUMN_NOTESID);
            stringBuilder.Append(" , ");
            stringBuilder.Append(Constants.COLUMN_FULLNAME);
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
        public static String getUpdate(String tableName , String filter , String condition , ArrayList columns , User user) {
            if (!DatabaseValidator.isValidParameters(tableName , filter)
                && !DatabaseValidator.isValidParameters(columns.ToArray())
                && !DatabaseValidator.isValidUser(user))
                throw new ArgumentException(Logging.paramenterLogging(new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(columns) , columns.ToString())
                                            , new Pair(nameof(user) , user.toString())));

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE ");
            stringBuilder.Append(tableName);
            stringBuilder.Append("SET ");
            for (int i = 0 ; i < columns.Count ; ++i) {
                stringBuilder.Append(columns[i]);
                stringBuilder.Append(" = ");
                stringBuilder.Append("' ");
                stringBuilder.Append(DatabaseUserParser.getColumnFromUserObject(user , columns[i].ToString()));
                stringBuilder.Append("' ");
                if (i != columns.Count - 1) stringBuilder.Append(",");
            }
            stringBuilder.Append(getWhere(filter , condition));
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }
    }
}
