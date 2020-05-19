﻿using System;
using System.Text;
using TODORoutine.database.parsers;
using TODORoutine.Database.Shared;

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
        public String getSelect(String tableName , String filter = "" , String column = "*" , String condition = "" , int from = 0 , int to = 0 , bool isOrder = false , String orderColumn = "") {
            //Validation
            if (from < 0 || to < 0 || (isOrder && orderColumn == null) || !DatabaseValidator.isValidParameters(tableName))
                throw new ArgumentException("Invalid Parameters in getSelect\n" + Logging.paramenterLogging(nameof(getSelect)  , true 
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(condition) , condition)
                                            , new Pair(nameof(column) , column)
                                            , new Pair(nameof(from) , from.ToString())));
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
            if (from > 0) {
                query.Append(" Limit ");
                query.Append(from);
                query.Append(" , ");
                query.Append(to);
            }
            if(isOrder) {
                query.Append("ORDER BY ");
                query.Append(orderColumn);
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

        public String getLastAddedRecored(String tableName) { return "SELECT * FROM " + tableName + " ORDER BY column DESC LIMIT 1;"; }

        public abstract String getInsert(T t);
        public abstract String getUpdate(String tableName , String filter , String condition , T t , params String[] columns);
        public abstract String getFieldFromColumn(String column , T t);
    }
}

