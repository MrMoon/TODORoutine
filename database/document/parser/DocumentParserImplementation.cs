using System;
using System.Text;
using TODORoutine.database.parsers;
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
                    , new Pair(nameof(column) , column) , new Pair(nameof(document) , document.ToString()));
            //Finding document filed
            if (column.Equals(DatabaseConstants.COLUMN_DOCUMENTID)) return document.getId();
            if (column.Equals(DatabaseConstants.COLUMN_OWENER)) return document.getOwner();
            //Column is invalid
            Logging.paramenterLogging(nameof(getFieldFromColumn) , true
                    , new Pair(nameof(column) , column) , new Pair(nameof(document) , document.ToString()));
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
            if (document == null)
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(document) , document.ToString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(document) , document.ToString()));
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
    }
}
