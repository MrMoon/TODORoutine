using System;
using System.Text;
using TODORoutine.database.general.exception;
using TODORoutine.database.general.parser;
using TODORoutine.database.general.shared;
using TODORoutine.general.logging;
using TODORoutine.models;

namespace TODORoutine.database.user.parser {
    /**
     * Main User Parser Implementation 
     * Handles user SQL Statment Parsing
     **/
    class UserParserImplementation : DatabaseParserImplementation<User> , UserParser {

        private static UserParser userParser = null;

        private UserParserImplementation() => Logging.singlton(nameof(UserParser));

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
            if (user == null)
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(user) , user.ToString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(user) , user.ToString()));
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
                    , new Pair(nameof(column) , column) , new Pair(nameof(user) , user.ToString()));
            //Finding user filed
            if (column.Equals(DatabaseConstants.COLUMN_FULLNAME)) return user.getFullName();
            if (column.Equals(DatabaseConstants.COLUMN_NOTEBOOKID)) return user.getNotebookId();
            if (column.Equals(DatabaseConstants.COLUMN_ID)) return user.getId();
            if (column.Equals(DatabaseConstants.COLUMN_USERNAME)) return user.getUsername();
            //Column is invalid
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }
    }
}
