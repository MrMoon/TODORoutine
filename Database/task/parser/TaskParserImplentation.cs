using System;
using System.Text;
using TODORoutine.database.parsers;
using TODORoutine.exceptions;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.task.parser {

    /**
     * Main Task Parser for SQL Statment Parsing 
     * Select , Update , Insert , Update
     **/
    class TaskParserImplentation : DatabaseParserImplementation<TaskNote> , TaskParser {

        private static TaskParser taskParser = null;
        
        private TaskParserImplentation() { }

        public static TaskParser getInstance() {
            if (taskParser == null) taskParser = new TaskParserImplentation();
            return taskParser;
        }

        /**
        * Column name in the database into a task filed
        * 
        * @column : the column in the database
        * @task : the task to return the field from
        * 
        * return a task field String value based on the database column
        **/
        public override String getFieldFromColumn(String column , TaskNote task) {
            //Logging
            Logging.paramenterLogging(nameof(getFieldFromColumn) , false , new Pair(nameof(column) , column) , new Pair(nameof(task) , task.ToString()));
            //Getting field from column
            if (column.Equals(DatabaseConstants.COLUMN_NOTEBOOKID)) return task.noteId;
            if (column.Equals(DatabaseConstants.COLUMN_STATUS)) return task.status.ToString();
            if (column.Equals(DatabaseConstants.COLUMN_PRIORITY)) return task.priority.ToString();
            if (column.Equals(DatabaseConstants.COLUMN_DUEDATE)) return task.dueDate.ToString();
            //Invalid Column
            Logging.logInfo(true , DatabaseConstants.INVALID(column));
            throw new DatabaseException(DatabaseConstants.INVALID(column));
        }

        /**
        * This method is a generic SQL task Insert Query statment
        * 
        * @task : the task object to be inserted in the database
        * 
        * It Throws and Exception when one of the parameters are invalid
        * 
        * return an SQL task Insert Statment
        **/
        public override String getInsert(TaskNote task) {
             //Validation
            if (task == null)
                throw new ArgumentException(Logging.paramenterLogging(nameof(getInsert) , true ,
                    new Pair(nameof(task) , task.ToString())));

            //Logging
            Logging.paramenterLogging(nameof(getInsert) , false , new Pair(nameof(task) , task.ToString()));
            //Building the SQL Statment 
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(DatabaseConstants.TABLE_TASK);
            query.Append(" ( ");
            query.Append(DatabaseConstants.COLUMN_NOTEID);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_STATUS);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_PRIORITY);
            query.Append(" , ");
            query.Append(DatabaseConstants.COLUMN_DUEDATE);
            query.Append(") VALUES ('");
            query.Append(task.noteId);
            query.Append("','");
            query.Append(task.status);
            query.Append("','");
            query.Append(task.priority);
            query.Append("','");
            query.Append(task.dueDate.ToString());
            query.Append("');");
            return query.ToString();
        }
    }
}
