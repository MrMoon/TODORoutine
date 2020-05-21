using System;
using System.Linq;
using System.Text;
using TODORoutine.database.parsers;
using TODORoutine.Database.Shared;
using TODORoutine.exceptions;

namespace TODORoutine.Shared {

    /**
     * Main Database Parser Implementation
     * Database Statment Parser Class that handle SQL Statments
     **/
    abstract class DatabaseParserImplementation<T> : DatabaseParser<T> {

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
            StringBuilder query = new StringBuilder();
            query.Append(" WHERE ");
            query.Append(filter);
            query.Append(" = '");
            query.Append(condition);
            query.Append("'");
            return query.ToString();
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
        public String getSelect(String tableName , String filter = "" , String column = "*" , String condition = "" , bool range = false , int from = 1 , int to = 20 , bool isOrder = false , String orderColumn = "") {
            //Validation
            if ((range && (from <= 0 || to <= 0)) || (isOrder && orderColumn == null) || !DatabaseValidator.isValidParameters(tableName))
                throw new ArgumentException("Invalid Parameters in getSelect\n" + Logging.paramenterLogging(nameof(getSelect)  , true 
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition)
                                            , new Pair(nameof(column) , column)
                                            , new Pair(nameof(from) , from.ToString())
                                            , new Pair(nameof(to) , to.ToString())
                                            , new Pair(nameof(range) , range.ToString())));
            //Logging
            Logging.paramenterLogging(nameof(getSelect) , false , new Pair(nameof(tableName) , tableName)
                                        , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition)
                                        , new Pair(nameof(column) , column));
            //Building the SQL Statment
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(column);
            query.Append(" FROM ");
            query.Append(tableName);
            if (filter != "") query.Append(getWhere(filter , condition));
            if (isOrder) {
                query.Append(" ORDER BY ");
                query.Append(orderColumn);
            }
            if (range) {
                query.Append(" Limit ");
                query.Append(from - 1);
                query.Append(" , ");
                query.Append(to + 1);
            }
            query.Append(";");
            return query.ToString();
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
            //Validation
            if (!DatabaseValidator.isValidParameters(tableName , filter , condition))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getDelete) , true , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition)));
            //Logging
            Logging.paramenterLogging(nameof(getDelete) , false , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition));
            //Building the SQL Statment
            StringBuilder query = new StringBuilder();
            query.Append("DELETE FROM ");
            query.Append(tableName);
            query.Append(getWhere(filter , condition));
            query.Append(";");
            return query.ToString();
        }

        /**
        * This method is a generic SQL Note Update Query statment
        * 
        * @tableName : The Table Name in the Database
        * @filter : the filter for the Where Statment
        * @condition : the condition for the Where statment
        * @column : the column name in the database
        * @t : the object that will be updated
        * 
        * It Throws and Exception when one of the parameters are invalid
        * 
        * return an SQL Update Statment
        **/
        public String getUpdate(String tableName , String filter , String condition , T t , params String[] columns) {
            //Validation
            if (columns.Count() == 0)
                throw new ArgumentException(DatabaseConstants.INVALID(DatabaseConstants.EMPTY_UPDATE) + Logging.paramenterLogging(nameof(getUpdate) , true
                , new Pair(nameof(columns) , columns.ToString())));

            if (!DatabaseValidator.isValidParameters(tableName , filter , condition)
                || !DatabaseValidator.isValid<T>(t))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getUpdate) , true
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(t) , t.ToString())
                                            , new Pair(nameof(condition) , condition)));
            //Logging
            Logging.paramenterLogging(nameof(getUpdate) , false
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(t) , t.ToString())
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
                    val = getFieldFromColumn(columnName , t);
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

        public static String getLastAddedRecored(String tableName) { return "SELECT * FROM " + tableName + " ORDER BY " + DatabaseConstants.COLUMN_ID + " DESC LIMIT 1;"; }
        public abstract String getInsert(T t);
        public abstract String getFieldFromColumn(String column , T t);
    }
}

