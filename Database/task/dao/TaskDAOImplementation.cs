using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TODORoutine.database.general.dao;
using TODORoutine.database.general.driver;
using TODORoutine.database.general.exception;
using TODORoutine.database.general.shared;
using TODORoutine.database.task.parser;
using TODORoutine.general.enums;
using TODORoutine.general.logging;
using TODORoutine.models;

namespace TODORoutine.database.task.dao {

    /**
     * Task Data Access Layer Implemenation
     * Handles Data Operations for the Task
     **/
    class TaskDAOImplementation : DatabaseDAOImplementation<TaskNote> , TaskDAO {

        private readonly String idColumn = DatabaseConstants.COLUMN_ID;
        private readonly String tableName = DatabaseConstants.TABLE_TASK;
        private static TaskDAO taskDAO = null;
        private DatabaseDriver driver = null;
        private TaskParser parser = null;

        private TaskDAOImplementation() {
            Logging.singlton(nameof(TaskDAO));
            parser = TaskParserImplementation.getInstance();
            driver = DatabaseDriverImplementation.getInstance();
            driver.createTable(DatabaseConstants.CREATE_TASK_TABLE);
        }

        public static TaskDAO getInstance() {
            if (taskDAO == null) taskDAO = new TaskDAOImplementation();
            return taskDAO;
        }

        /**
         * Getting the task from the database based on the id
         * 
         * @id : the task id to search for
         * 
         * return task if found and throw an Exception otherwise
         **/
        public override TaskNote findById(String id) {
            //Logging
            Logging.paramenterLogging(nameof(findById) , false , new Pair(nameof(id) , id));
            //Finding the task
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , idColumn , "*" , id));
                //Reading the the Record from the database
                TaskNote task = find(reader);
                Logging.logInfo(false , nameof(findById) , DatabaseConstants.FOUND(id) , task.ToString());
                reader.Close();
                return task;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findById) , true , new Pair(nameof(id) , id));
            //TaskNote not found in the database
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        /**
         * Getting the Task from the reader
         * 
         * @reader : the data base reader
         * 
         * return a task if it was found and throw an exception otherwise
         **/
        public override TaskNote find(SQLiteDataReader reader) {
            if(reader.Read()) {
                TaskNote task = new TaskNote();
                task.dueDate = DateTime.Parse(reader[DatabaseConstants.COLUMN_DUEDATE].ToString());
                task.id = reader[idColumn].ToString();
                String temp = reader[DatabaseConstants.COLUMN_PRIORITY].ToString();
                if(Enum.TryParse(temp , true , out Priority priority)) task.priority = priority;
                temp = reader[DatabaseConstants.COLUMN_STATUS].ToString();
                if (Enum.TryParse(temp , true , out Status status)) task.status = status;
                task.noteId = reader[DatabaseConstants.COLUMN_NOTEID].ToString();
                return task;
            }

            throw new DatabaseException(DatabaseConstants.NOT_FOUND("404"));
        }

        /**
         * Inserting the task into the SQL Database
         * 
         * @task : the task that will get inserted
         * 
         * return ture if and only if the task was saved successfully
         **/
        public override bool save(TaskNote task) {
            //Logging
            Logging.paramenterLogging(nameof(save) , false
                , new Pair(nameof(task) , task.ToString()));
            //Inserting User into the Database
            try {
                return driver.executeQuery(parser.getInsert(task)) != -1;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }

        /**
         * updatting the task in the SQL Database
         * 
         * @task : the task that will get updated
         * @columns : the column in the database that will be updated
         * 
         * return true if and only if the updating operation was successfull
         **/
        public override bool update(TaskNote task , params String[] columns) {
            //Logging
            Logging.paramenterLogging(nameof(update) , false , new Pair(nameof(task) , task.ToString()));
            //Updating
            try {
                return driver.executeQuery(parser.getUpdate(tableName ,
                    DatabaseConstants.COLUMN_ID , task.id , task , columns)) != -1;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(update) , true , new Pair(nameof(task) , task.ToString()));
            //TaskNote was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(task.ToString()));
        }

        /**
        * deleting the task from the Database
        * 
        * @task : the task that will get deleted
        * 
        * return true if and only if the delete operation was done successfully
        **/
        public override bool delete(String id) {
            //Logging
            Logging.paramenterLogging(nameof(delete) , false
                , new Pair(nameof(id) , id));
            //Deleting task from database
            try {
                driver.executeQuery(parser.getDelete(tableName , idColumn , id));
                return true;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(delete) , true
                , new Pair(nameof(id) , id));
            //TaskNote not found in the database
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        /**
        * Getting all the task by it's priority
        * 
        * @priority : the priority to search for in the database
        * 
        * return a list of tasknotes ids with a priority if it was found and throw an exception otherwise
        **/
        public List<String> findAllByPriority(Priority priority) {
            //Logging
            Logging.paramenterLogging(nameof(findAllByPriority) , false , new Pair(nameof(priority) , priority.ToString()));
            //Finding
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , DatabaseConstants.COLUMN_PRIORITY , idColumn , priority.ToString()));
                List<String> notesids = new List<String>();
                while (reader.Read()) notesids.Add(reader[idColumn].ToString());
                return notesids;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findAllByPriority) , true , new Pair(nameof(priority) , priority.ToString()));
            //NoN were found or something went wrong
            throw new DatabaseException(DatabaseConstants.INVALID(priority.ToString()));
        }

        /**
        * Getting all the task by it's status
        * 
        * @status : the status to search for in the database
        * 
        * return a list of tasknotes ids with a status if it was found and throw an exception otherwise
        **/
        public List<String> findAllByStatus(Status status) {
            //Logging
            Logging.paramenterLogging(nameof(findAllByPriority) , false , new Pair(nameof(status) , status.ToString()));
            //Finding
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , DatabaseConstants.COLUMN_STATUS , idColumn , status.ToString()));
                List<String> notesids = new List<String>();
                while (reader.Read()) notesids.Add(reader[idColumn].ToString());
                return notesids;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findAllByPriority) , true , new Pair(nameof(status) , status.ToString()));
            //Status was not found or something went wrong
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(status.ToString()));
        }

        /**
         * Getting the Note id from the task id
         * 
         * @taskId : the task id to search for
         * 
         * return a note id if it was found and throw an exception otherwise
         **/
        public String findNote(String taskId) {
            //Logging
            Logging.paramenterLogging(nameof(findNote) , false , new Pair(nameof(taskId) , taskId));
            //Finding note id
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , idColumn , DatabaseConstants.COLUMN_NOTEID , taskId));
                if(reader.Read()) return reader[DatabaseConstants.COLUMN_NOTEID].ToString();
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findNote) , false , new Pair(nameof(taskId) , taskId));
            //Task id was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(taskId));
        }

        /**
        * Getting all the task with in a range of  lastTaskId to lastTaskId + x ordered by it's duedate
        * 
        * @lastTaskId : the lastTaskId to start from in the database
        * 
        * return a list of tasknotes ids if it was found and throw an exception otherwise
        **/
        public List<String> findAllByOrderOfDueDate(String lastTaskId = "1") {
            //Logging
            Logging.paramenterLogging(nameof(findAllByOrderOfDueDate) , false , new Pair(nameof(lastTaskId) , lastTaskId));
            return findAll(parser , tableName , DatabaseConstants.COLUMN_DUEDATE , lastTaskId);
        }

        /**
        * Getting all the task with in a range of  lastTaskId to lastTaskId + x
        * 
        * @lastTaskId : the lastTaskId to start from in the database
        * 
        * return a list of tasknotes ids if it was found and throw an exception otherwise
        **/
        public List<String> findAll(String lastTaskId = "1") {
            //Logging
            Logging.paramenterLogging(nameof(findAll) , false , new Pair(nameof(lastTaskId) , lastTaskId));
            return findAll(parser , tableName , "-1" , lastTaskId);
        }
    }
}
