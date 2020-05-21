﻿using System;
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
        public override String getFieldFromColumn(String column , Notebook notebook) {
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , false
                    , new Pair(nameof(column) , column) , new Pair(nameof(notebook) , notebook.ToString()));
            //Finding notebook filed
            if (column.Equals(DatabaseConstants.COLUMN_NOTEBOOKID)) return notebook.getId();
            if (column.Equals(DatabaseConstants.COLUMN_NOTESID)) return CSVParser.CSV2String(notebook.getNotes().ToList());
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
        public override String getInsert(Notebook notebook) {
            //Validation
            if (!DatabaseValidator.isValid<Notebook>((notebook)))
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(notebook) , notebook.ToString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(notebook) , notebook.ToString()));
            //Building the SQL Statment 
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(DatabaseConstants.TABLE_NOTEBOOK);
            query.Append(" ( ");
            query.Append(DatabaseConstants.COLUMN_AUTHOR);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_TITLE);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_DATECREATED);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_LASTMODIFIED);
            query.Append(") VALUES ('");
            query.Append(notebook.getAuthor());
            query.Append("','");
            query.Append(notebook.getTitle());
            query.Append("','");
            query.Append(DateTime.Now);
            query.Append("','");
            query.Append(notebook.getLastModified());
            query.Append("');");
            return query.ToString();
        }
    }
}