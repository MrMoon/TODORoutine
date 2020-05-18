using System;
using System.Linq;
using System.Text;
using TODORoutine.database.parsers;
using TODORoutine.Database.Shared;
using TODORoutine.exceptions;
using TODORoutine.models;
using TODORoutine.shared.csv;
using TODORoutine.Shared;

namespace TODORoutine.database.notebook {
    class NotebookParserImplementation : DatabaseParserImplementation<Notebook> , NotebookParser {

        private static NotebookParser notebookParser = null;

        private NotebookParserImplementation() { }

        public static NotebookParser getInstance() {
            if (notebookParser == null) notebookParser = new NotebookParserImplementation();
            return notebookParser;
        }

        /**
       * Column name in the database into a notebook filed
       * 
       * @column : the column in the database
       * @notebook : the notebook to return the field from
       * 
       * return a notebook field String value based on the database column
       **/
        public override string getFieldFromColumn(string column , Notebook notebook) {
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , false
                    , new Pair(nameof(column) , column) , new Pair(nameof(notebook) , notebook.toString()));
            //Getting notebook filed
            if (column.Equals(DatabaseConstants.COLUMN_NOTEBOOKID)) return notebook.getId();
            if (column.Equals(DatabaseConstants.COLUMN_NOTESID)) return CSVParser.CSV2String(notebook.getNotes());
            if (column.Equals(DatabaseConstants.COLUMN_TITLE)) return notebook.getTitle();
            if (column.Equals(DatabaseConstants.COLUMN_AUTHOR)) return notebook.getAuthor();
            if (column.Equals(DatabaseConstants.COLUMN_LASTMODIFIED)) return notebook.getLastModified();
            if (column.Equals(DatabaseConstants.COLUMN_DATECREATED)) return notebook.getDateCreated();
            //Column is invalid
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }

        /**
        * This method is a generic SQL Note Insert Query statment
        * 
        * @notebook : the notebook object to be inserted in the database
        * 
        * It Throws and Exception when one of the parameters are invalid
        * 
        * return an SQL Notebook Insert Statment
        **/
        public override string getInsert(Notebook notebook) {
            //Validation
            if (!DatabaseValidator.isValidNotebook((notebook)))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(notebook) , notebook.toString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(notebook) , notebook.toString()));
            //Building the SQL Statment 
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(DatabaseConstants.TABLE_TODOROUTINE);
            query.Append(" ( ");
            query.Append(DatabaseConstants.COLUMN_AUTHOR);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_TITLE);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_DATECREATED);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_LASTMODIFIED);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_NOTESID);
            query.Append(") VALUES ('");
            query.Append(notebook.getAuthor());
            query.Append("','");
            query.Append(notebook.getTitle());
            query.Append("','");
            query.Append(DateTime.Now);
            query.Append("','");
            query.Append(notebook.getLastModified());
            query.Append("','");
            query.Append(notebook.getNotes());
            query.Append("');");
            return query.ToString();
        }

        /**
         * This method is a generic SQL Note Insert Query statment
         * 
         * @notebook : the notebook object to be inserted in the database
         * 
         * It Throws and Exception when one of the parameters are invalid
         * 
         * return an SQL Note Insert Statment
         **/
        public override string getUpdate(string tableName , string filter , string condition , Notebook notebook , params string[] columns) {
            //Validation
            if (columns.Count() == 0) throw new ArgumentException(DatabaseConstants.INVALID(DatabaseConstants.EMPTY_UPDATE) + Logging.paramenterLogging(nameof(getUpdate) , true
                , new Pair(nameof(columns) , columns.ToString())));

            if (!DatabaseValidator.isValidParameters(tableName , filter , condition)
                || !DatabaseValidator.isValidNotebook(notebook))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getUpdate) , true
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(notebook) , notebook.toString())
                                            , new Pair(nameof(condition) , condition)));
            //Logging
            Logging.paramenterLogging(nameof(getUpdate) , false
                                            , new Pair(nameof(tableName) , tableName)
                                            , new Pair(nameof(filter) , filter) , new Pair(nameof(notebook) , notebook.toString())
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
                    val = getFieldFromColumn(columnName , notebook);
                } catch (DatabaseException e) {
                    Logging.logInfo(true , e.Data.ToString());
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
    }
}
