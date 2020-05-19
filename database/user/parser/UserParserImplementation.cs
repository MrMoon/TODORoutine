using System;
using System.Linq;
using System.Text;
using TODORoutine.Database.Shared;
using TODORoutine.exceptions;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.database.parsers.user_parsers {
    class UserParserImplementation : DatabaseParserImplementation<User> , UserParser {

        private static UserParser userParser = null;

        public static UserParser getInstance() {
            if (userParser == null) userParser = new UserParserImplementation();
            return userParser;
        }

        /**
        * This method is a generic SQL User Insert Query statment
        * 
        * @user : the user object to be inserted in the database
        * 
        * It Throws and Exception when one of the parameters are invalid
        * 
        * return an SQL Insert Statment
        **/
        public override String getInsert(User user) {
            //Validation
            if (!DatabaseValidator.isValidUser((user)))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(user) , user.toString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(user) , user.toString()));
            //Building the SQL Statment 
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("INSERT INTO ");
            stringBuilder.Append(DatabaseConstants.TABLE_TODOROUTINE);
            stringBuilder.Append(" ( ");
            stringBuilder.Append(DatabaseConstants.COLUMN_USERNAME);
            stringBuilder.Append(" , ");
            stringBuilder.Append(DatabaseConstants.COLUMN_FULLNAME);
            stringBuilder.Append(" , ");
            stringBuilder.Append(DatabaseConstants.COLUMN_AUTH);
            stringBuilder.Append(") VALUES ('");
            stringBuilder.Append(user.getUsername());
            stringBuilder.Append("','");
            stringBuilder.Append(user.getFullName());
            stringBuilder.Append("','");
            stringBuilder.Append(user.getIsAuthenticated());
            stringBuilder.Append("');");
            return stringBuilder.ToString();
        }

        /**
        * This method is a generic SQL User Update Query statment
        * 
        * @tableName : The Table Name in the Database
        * @filter : the filter for the Where Statment
        * @condition : the condition for the Where statment
        * @column : the column name in the database
        * 
        * It Throws and Exception when one of the parameters are invalid
        * 
        * return an SQL Update Statment
        **/
        public override String getUpdate(String tableName , String filter , String condition , User user , params String[] columns) {
            //Validation
            if (columns.Count() == 0) throw new ArgumentException("There is Nothing to Update\n" + Logging.paramenterLogging(nameof(getUpdate) , true
                , new Pair(nameof(columns) , columns.ToString())));

            if (!DatabaseValidator.isValidParameters(tableName , filter , condition)
                || !DatabaseValidator.isValidUser(user))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getUpdate) , true
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(user) , user.toString())
                                            , new Pair(nameof(condition) , condition)));
            //Logging
            Logging.paramenterLogging(nameof(getUpdate) , false
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(user) , user.toString())
                                            , new Pair(nameof(condition) , condition));
            //Building SQL Statment
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE ");
            stringBuilder.Append(tableName);
            stringBuilder.Append(" SET ");
            String val = "";
            foreach (String columnName in columns) {
                stringBuilder.Append(columnName);
                stringBuilder.Append(" = '");
                try {
                    val = getFieldFromColumn(columnName , user);
                } catch (DatabaseException e) {
                    Logging.logInfo(true , e.Message);
                    return null;
                }
                stringBuilder.Append(val);
                stringBuilder.Append("'");
                if (columnName != columns[columns.Count() - 1]) stringBuilder.Append(",");
            }
            stringBuilder.Append(getWhere(filter , condition));
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }

        /**
        * Column name in the database into a user filed
        * 
        * @column : the column in the database
        * @user : the user to return the field from
        * 
        * return a user field String value based on the database column
        **/
        public override String getFieldFromColumn(String column , User user) {
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , false
                    , new Pair(nameof(column) , column) , new Pair(nameof(user) , user.toString()));
            //Finding user filed
            if (column.Equals(DatabaseConstants.COLUMN_FULLNAME)) return user.getFullName();
            if (column.Equals(DatabaseConstants.COLUMN_NOTESID)) return user.getNotesId();
            if (column.Equals(DatabaseConstants.COLUMN_USERID)) return user.getId();
            if (column.Equals(DatabaseConstants.COLUMN_USERNAME)) return user.getUsername();
            //Column is invalid
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }
    }
}
