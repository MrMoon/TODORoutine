using System;
using System.Linq;
using System.Text;
using TODORoutine.database.parsers;
using TODORoutine.Database.Shared;
using TODORoutine.exceptions;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.document {

    /**
     * Main Document Parser Implementation that handles inserts of the Documnets and Updating 
     **/
    class DocumentParserImplementation : DatabaseParserImplementation<Document> , DocumentParser {

        private static DocumentParser documentParser = null;

        public static DocumentParser getInstance() {
            if (documentParser == null) documentParser = new DocumentParserImplementation();
            return documentParser;
        }

        /**
        * Column name in the database into a document filed
        * 
        * @column : the column in the database
        * @document : the document to return the field from
        * 
        * return a document field String value based on the database column
        **/
        public override String getFieldFromColumn(String column , Document document) {
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , false
                    , new Pair(nameof(column) , column) , new Pair(nameof(document) , document.toString()));
            //Finding document filed
            if (column.Equals(DatabaseConstants.COLUMN_DOCUMENTID)) return document.getId();
            if (column.Equals(DatabaseConstants.COLUMN_OWENER)) return document.getOwner();
            //Column is invalid
            Logging.paramenterLogging(nameof(getFieldFromColumn) , true
                    , new Pair(nameof(column) , column) , new Pair(nameof(document) , document.toString()));
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }

        /**
        * This method is a generic SQL Insert Query statment
        *
        * @tableName : The Table Name in the Database
        * @document : the document object to be inserted in the database
        * It Throws and Exception when one of the parameters are invalid
        *
        * return an SQL Document Insert Statment
        **/
        public override String getInsert(Document document) {
            //Validation
            if (!DatabaseValidator.isValid<Document>(document))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(document) , document.toString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(document) , document.toString()));
            //Building the SQL Statment 
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(DatabaseConstants.TABLE_DOCUMENT);
            query.Append(" ( ");
            query.Append(DatabaseConstants.COLUMN_OWENER);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_SHARED);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_DOCUMENT);
            query.Append(") VALUES ('");
            query.Append(document.getOwner());
            query.Append("' , '");
            query.Append(false.ToString());
            query.Append("' , ");
            query.Append(DatabaseConstants.DOCUMENT_PARAMETER);
            query.Append(");");
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
        * It Throws and Exception when one of the parameters are invalid
        *
        * return an SQL Document Update Statment
        **/
        public override String getUpdate(String tableName , String filter , String condition , Document document , params String[] columns) {
            //Validation
            if (columns.Count() == 0) throw new ArgumentException(DatabaseConstants.EMPTY_UPDATE + Logging.paramenterLogging(nameof(getUpdate) , true
                , new Pair(nameof(columns) , columns.ToString())));

            if (!DatabaseValidator.isValidParameters(tableName , filter , condition)
                || !DatabaseValidator.isValid<Document>(document))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getUpdate) , true
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(document) , document.toString())
                                            , new Pair(nameof(condition) , condition)));
            //Logging
            Logging.paramenterLogging(nameof(getUpdate) , false
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(document) , document.toString())
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
                    val = getFieldFromColumn(columnName , document);
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
