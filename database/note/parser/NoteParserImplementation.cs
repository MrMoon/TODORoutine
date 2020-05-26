using System;
using System.Text;
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
            if (note == null)
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(note) , note.ToString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(note) , note.ToString()));
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
                    , new Pair(nameof(column) , column) , new Pair(nameof(note) , note.ToString()));
            //Finding note filed
            if (column.Equals(DatabaseConstants.COLUMN_AUTHOR)) return note.getAuthor();
            if (column.Equals(DatabaseConstants.COLUMN_NOTEID)) return note.getId();
            if (column.Equals(DatabaseConstants.COLUMN_TITLE)) return note.getTitle();
            if (column.Equals(DatabaseConstants.COLUMN_LASTMODIFIED)) return note.getLastModified().ToString();
            if (column.Equals(DatabaseConstants.COLUMN_DATECREATED)) return note.getDateCreated().ToString();
            if (column.Equals(DatabaseConstants.COLUMN_DOCUMENTID)) return note.getDocumentId();
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , true
                    , new Pair(nameof(column) , column) , new Pair(nameof(note) , note.ToString()));
            //Column is invalid
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }
    }
}
