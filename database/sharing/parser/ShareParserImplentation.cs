using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.parsers;
using TODORoutine.Database.Shared;
using TODORoutine.exceptions;
using TODORoutine.models;
using TODORoutine.shared.csv;
using TODORoutine.Shared;

namespace TODORoutine.database.sharing.parser {

    /**
     * Main Share Parser Implentation that handle insert and delete statement for the share class
     **/
    class ShareParserImplentation : DatabaseParserImplementation<Share>, ShareParser {

        private static ShareParser shareParser = null;

        private ShareParserImplentation() { }

        public static ShareParser getInstance() {
            if (shareParser == null) shareParser = new ShareParserImplentation();
            return shareParser;
        }

        /**
        * Column name in the database into a note filed
        * 
        * @column : the column in the database
        * @share : the share to return the field from
        * 
        * return a share field String value based on the database column
        **/
        public override String getFieldFromColumn(String column , Share share) {
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , false
                    , new Pair(nameof(column) , column) , new Pair(nameof(share) , share.toString()));
            //Finding note filed
            if (column.Equals(DatabaseConstants.COLUMN_USERID)) return share.userId;
            if (column.Equals(DatabaseConstants.COLUMN_DOCUMENTSIDS)) return CSVParser.CSV2String(share.documentsIds.ToList());

            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , true
                    , new Pair(nameof(column) , column) , new Pair(nameof(note) , share.toString()));
            //Column is invalid
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }

        /**
        * This method is a generic SQL Note Insert Query statment
        * 
        * @share : the share object to be inserted in the database
        * 
        * It Throws and Exception when one of the parameters are invalid
        * 
        * return an SQL Share Insert Statment
        **/
        public override String getInsert(Share share) {
            //Validation
            if (!DatabaseValidator.isValid<Share>(share))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(note) , share.toString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(note) , share.toString()));
            //Building the SQL Statment 
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(DatabaseConstants.TABLE_DOCUMENT_SHARE);
            query.Append(" ( ");
            query.Append(DatabaseConstants.COLUMN_USERID);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_DOCUMENTSIDS);
            query.Append(") VALUES ('");
            query.Append(share.userId);
            query.Append("','");
            query.Append(CSVParser.CSV2String(share.documentsIds.ToList()));
            query.Append("');");
            return query.ToString();
        }

        /**
        * This method is a generic SQL Note Update Query statment
        * 
        * @tableName : The Table Name in the Database
        * @filter : the filter for the Where Statment
        * @condition : the condition for the Where statment
        * @column : the column name in the database
        * @share : the share the will be updated
        * 
        * It Throws and Exception when one of the parameters are invalid
        * 
        * return an SQL Update Statment
        **/
        public override String getUpdate(String tableName , String filter , String condition , Share share , params String[] columns) {
            //Validation
            if (columns.Count() == 0)
                throw new ArgumentException(DatabaseConstants.INVALID(DatabaseConstants.EMPTY_UPDATE) + Logging.paramenterLogging(nameof(getUpdate) , true
                , new Pair(nameof(columns) , columns.ToString())));

            if (!DatabaseValidator.isValidParameters(tableName , filter , condition)
                || !DatabaseValidator.isValid<Share>(share))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getUpdate) , true
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(note) , share.toString())
                                            , new Pair(nameof(condition) , condition)));
            //Logging
            Logging.paramenterLogging(nameof(getUpdate) , false
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(note) , share.toString())
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
                    val = getFieldFromColumn(columnName , share);
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
