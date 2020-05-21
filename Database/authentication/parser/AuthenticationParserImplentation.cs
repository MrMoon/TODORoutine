using System;
using System.Text;
using TODORoutine.database.parsers;
using TODORoutine.exceptions;
using TODORoutine.Shared;

namespace TODORoutine.database.authentication.parser {
    class AuthenticationParserImplentation : DatabaseParserImplementation<Authentication> , AuthenticationParser {

        private static AuthenticationParser authenticationParser = null;

        private AuthenticationParserImplentation() { }

        public static AuthenticationParser getInstance() {
            if (authenticationParser == null) authenticationParser = new AuthenticationParserImplentation();
            return authenticationParser;
        }

        /**
        * Column name in the database into a note filed
        * 
        * @column : the column in the database
        * @note : the note to return the field from
        * 
        * return a note field String value based on the database column
        **/
        public override String  getFieldFromColumn(String  column , Authentication authentication) {
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , false
                    , new Pair(nameof(column) , column) , new Pair(nameof(note) , authentication.ToString()));
            //Finding note filed
            if (column.Equals(DatabaseConstants.COLUMN_USERNAME)) return authentication.getUsername();
            if (column.Equals(DatabaseConstants.COLUMN_PASSWORD)) return authentication.getPassword();
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , true
                    , new Pair(nameof(column) , column) , new Pair(nameof(note) , authentication.ToString()));
            //Column is invalid
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }

        public override String  getInsert(Authentication authentication) {
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
