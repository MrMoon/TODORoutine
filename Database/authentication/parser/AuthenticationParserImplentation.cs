using System;
using System.Text;
using TODORoutine.database.general.exception;
using TODORoutine.database.general.parser;
using TODORoutine.database.general.shared;
using TODORoutine.general.logging;
using TODORoutine.models;

namespace TODORoutine.database.authentication.parser {

    /**
     * Main Authentication SQL Statments Parser Implementation
     * Handles Authentication SQL Statments and Strings
     **/
    class AuthenticationParserImplementation : DatabaseParserImplementation<Authentication> , AuthenticationParser {

        private static AuthenticationParser authenticationParser = null;

        private AuthenticationParserImplementation() => Logging.singlton(nameof(AuthenticationParser));

        public static AuthenticationParser getInstance() {
            if (authenticationParser == null) authenticationParser = new AuthenticationParserImplementation();
            return authenticationParser;
        }

        /**
        * from Column name in the database into a authentication filed
        * 
        * @column : the column in the database
        * @authentication : the authentication to return the field from
        * 
        * return a authentication field String value based on the database column
        **/
        public override String getFieldFromColumn(String  column , Authentication authentication) {
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , false
                    , new Pair(nameof(column) , column) , new Pair(nameof(note) , authentication.ToString()));
            //Finding authentication filed
            if (column.Equals(DatabaseConstants.COLUMN_USERNAME)) return authentication.getUsername();
            if (column.Equals(DatabaseConstants.COLUMN_PASSWORD)) return authentication.getPassword();
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , true
                    , new Pair(nameof(column) , column) , new Pair(nameof(note) , authentication.ToString()));
            //Column is invalid
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }

        /**
         * Insert Statment for Authentication model 
         * 
         * @authentication : the authentication object to insert to the database
         * 
         * return a SQL insert Statment for the authentication model
         **/
        public override String getInsert(Authentication authentication) {
            //Validation
            if (authentication == null)
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(note) , authentication.ToString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(note) , authentication.ToString()));
            //Building the SQL Statment 
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(DatabaseConstants.TABLE_AUTHENTICATE);
            query.Append(" ( ");
            query.Append(DatabaseConstants.COLUMN_USERNAME);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_PASSWORD);
            query.Append(") VALUES ('");
            query.Append(authentication.getUsername());
            query.Append("','");
            query.Append(authentication.getPassword());
            query.Append("');");
            return query.ToString();
        }
    }
}
