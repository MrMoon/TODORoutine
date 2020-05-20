using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.authentication;
using TODORoutine.database.parsers;
using TODORoutine.Database.Shared;
using TODORoutine.exceptions;
using TODORoutine.Shared;

namespace TODORoutine.database.authentication.parser {

    /**
     * Main Authentication SQL Parser Implentation that handls Authentication SQL Statment 
     **/
    class AuthParserImplementation : DatabaseParserImplementation<Authenticate> , AuthParser {

        private static AuthParser authParser = null;

        private AuthParserImplementation() { }

        public static AuthParser getInstance() {
            if (authParser == null) authParser = new AuthParserImplementation();
            return authParser;
        }

        /**
         * Getting authenticate fields from the column name
         * 
         * @column : the column name in the database
         * @authenticate : the authentiaction object to get the field 
         * 
         * return a filed from the authenticate object
         **/
        public override string getFieldFromColumn(string column , Authenticate authenticate) {
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , false
                    , new Pair(nameof(column) , column) , new Pair(nameof(authenticate) , authenticate.toString()));
            //Finding authenticate filed
            if (column.Equals(DatabaseConstants.COLUMN_USERNAME)) return authenticate.username;
            if (column.Equals(DatabaseConstants.COLUMN_PASSWORD)) return authenticate.password;
            //Column is invalid
            Logging.paramenterLogging(nameof(getFieldFromColumn) , true
                    , new Pair(nameof(column) , column) , new Pair(nameof(authenticate) , authenticate.toString()));
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }

        /**
        * This method is a generic SQL Insert Query statment
        *
        * @tableName : The Table Name in the Database
        * @authenticate : the authenticate object to be inserted in the database
        * It Throws and Exception when one of the parameters are invalid
        *
        * return an SQL Document Insert Statment
        **/
        public override string getInsert(Authenticate authenticate) {
            //Validation
            if (!DatabaseValidator.isValid<Authenticate>(authenticate))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(authenticate) , authenticate.toString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(authenticate) , authenticate.toString()));
            //Building the SQL Statment 
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(DatabaseConstants.TABLE_AUTHENTICATE);
            query.Append(" ( ");
            query.Append(DatabaseConstants.COLUMN_USERNAME);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_PASSWORD);
            query.Append(") VALUES ('");
            query.Append(authenticate.username);
            query.Append("' , '");
            query.Append(authenticate.password);
            query.Append("');");
            return query.ToString();
        }

        /**
        * This method is a generic SQL Update Query statment
        *
        * @tableName : The Table Name in the Database
        * @filter : the filter for the Where Statment
        * @condition : the condition for the Where statment
        * @column : the column name in the database
        * @id : Document id that will be updated
        * 
        * It Throws and Exception when one of the parameters are invalid
        *
        * return an SQL Document Update Statment
        **/
        public override string getUpdate(string tableName , string filter , string condition , Authenticate authenticate , params string[] columns) {
            //Validation
            if (columns.Count() == 0)
                throw new ArgumentException(DatabaseConstants.INVALID(DatabaseConstants.EMPTY_UPDATE) 
                    + Logging.paramenterLogging(nameof(getUpdate) , true
                    , new Pair(nameof(columns) , columns.ToString())));

            if (!DatabaseValidator.isValidParameters(tableName , filter , condition)
                || !DatabaseValidator.isValid<Authenticate>(authenticate))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getUpdate) , true
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(note) , authenticate.toString())
                                            , new Pair(nameof(condition) , condition)));
            //Logging
            Logging.paramenterLogging(nameof(getUpdate) , false
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(note) , authenticate.toString())
                                            , new Pair(nameof(condition) , condition));
            //Building SQL Statment
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append(tableName);
            query.Append(" SET ");
            String val = "", prefix = "";
            foreach (String columnName in columns) {
                query.Append(prefix);
                prefix = ",";
                query.Append(columnName);
                query.Append(" = '");
                try {
                    val = getFieldFromColumn(columnName , authenticate);
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
    }
}
