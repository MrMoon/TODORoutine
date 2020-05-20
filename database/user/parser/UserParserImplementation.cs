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
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(DatabaseConstants.TABLE_USER);
            query.Append(" ( ");
            query.Append(DatabaseConstants.COLUMN_USERNAME);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_FULLNAME);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_AUTH);
            query.Append(") VALUES ('");
            query.Append(user.getUsername());
            query.Append("','");
            query.Append(user.getFullName());
            query.Append("','");
            query.Append(user.getIsAuthenticated());
            query.Append("');");
            return query.ToString();
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
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append(tableName);
            query.Append(" SET ");
            String val = "" , prefix = "";
            foreach (String columnName in columns) {
                query.Append(prefix);
                prefix = ",";
                query.Append(columnName);
                query.Append(" = '");
                try {
                    val = getFieldFromColumn(columnName , user);
                } catch (DatabaseException e) {
                    Logging.logInfo(true , e.Message);
                    return null;
                }
                query.Append(val);
                query.Append("'");
            }
            query.Append(getWhere(filter , condition));
            query.Append(";");
            return query.ToString();
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
            if (column.Equals(DatabaseConstants.COLUMN_ID)) return user.getId();
            if (column.Equals(DatabaseConstants.COLUMN_USERNAME)) return user.getUsername();
            //Column is invalid
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }
    }
}
