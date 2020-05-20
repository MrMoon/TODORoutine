using System;
using System.Linq;
using System.Text;
using TODORoutine.Database.Shared;
using TODORoutine.exceptions;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.parsers.notes_parser {

    /**
     * Main Note Parser for SQL Statment Parsing 
     * Select , Update , Insert , Update
     **/
    class NoteParserImplementation : DatabaseParserImplementation<Note> , NoteParser {

        private static NoteParser noteParser = null;

        private NoteParserImplementation() {
 
        }

        public static NoteParser getInstance() {
            if (noteParser == null) noteParser = new NoteParserImplementation();
            return noteParser;
        }

        /**
       * This method is a generic SQL Note Insert Query statment
       * 
       * @note : the note object to be inserted in the database
       * 
       * It Throws and Exception when one of the parameters are invalid
       * 
       * return an SQL Note Insert Statment
       **/
        public override String getInsert(Note note) {
            //Validation
            if (!DatabaseValidator.isValidNote(note))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(note) , note.toString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(note) , note.toString()));
            //Building the SQL Statment 
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(DatabaseConstants.TABLE_NOTE);
            query.Append(" ( ");
            query.Append(DatabaseConstants.COLUMN_AUTHOR);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_TITLE);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_DATECREATED);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_LASTMODIFIED);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_DOCUMENTID);
            query.Append(") VALUES ('");
            query.Append(note.getAuthor());
            query.Append("','");
            query.Append(note.getTitle());
            query.Append("','");
            query.Append(DateTime.Now);
            query.Append("','");
            query.Append(note.getLastModified());
            query.Append("','");
            query.Append(note.getDocumentId());
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
        * 
        * It Throws and Exception when one of the parameters are invalid
        * 
        * return an SQL Update Statment
        **/
        public override String getUpdate(String tableName , String filter , String condition , Note note , params String[] columns) {
            //Validation
            if (columns.Count() == 0) 
                throw new ArgumentException(DatabaseConstants.INVALID(DatabaseConstants.EMPTY_UPDATE) + Logging.paramenterLogging(nameof(getUpdate) , true
                , new Pair(nameof(columns) , columns.ToString())));

            if (!DatabaseValidator.isValidParameters(tableName , filter , condition)
                || !DatabaseValidator.isValidNote(note))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getUpdate) , true
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(note) , note.toString())
                                            , new Pair(nameof(condition) , condition)));
            //Logging
            Logging.paramenterLogging(nameof(getUpdate) , false
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(note) , note.toString())
                                            , new Pair(nameof(condition) , condition));
            //Building SQL Statment
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append(tableName);
            query.Append(" SET ");
            String val = "";
            foreach (String columnName in columns) {
                query.Append(columnName);
                query.Append(" = '");
                try {
                    val = getFieldFromColumn(columnName , note);
                } catch (DatabaseException e) {
                    Logging.logInfo(true , e.Message);
                    return null;
                }
                query.Append(val);
                query.Append("'");
                if (columnName != columns[columns.Count() - 1]) query.Append(",");
            }
            query.Append(getWhere(filter , condition));
            query.Append(";");
            return query.ToString();
        }

        /**
        * Column name in the database into a note filed
        * 
        * @column : the column in the database
        * @note : the note to return the field from
        * 
        * return a note field String value based on the database column
        **/
        public override String getFieldFromColumn(String column , Note note) {
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , false
                    , new Pair(nameof(column) , column) , new Pair(nameof(note) , note.toString()));
            //Finding note filed
            if (column.Equals(DatabaseConstants.COLUMN_AUTHOR)) return note.getAuthor();
            if (column.Equals(DatabaseConstants.COLUMN_NOTEID)) return note.getId();
            if (column.Equals(DatabaseConstants.COLUMN_TITLE)) return note.getTitle();
            if (column.Equals(DatabaseConstants.COLUMN_LASTMODIFIED)) return note.getLastModified();
            if (column.Equals(DatabaseConstants.COLUMN_DATECREATED)) return note.getDateCreated();
            if (column.Equals(DatabaseConstants.COLUMN_DOCUMENTID)) return note.getDocumentId();
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , true
                    , new Pair(nameof(column) , column) , new Pair(nameof(note) , note.toString()));
            //Column is invalid
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }
    }
}
