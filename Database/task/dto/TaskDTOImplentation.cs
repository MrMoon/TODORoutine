using System;
using System.Collections.Generic;
using TODORoutine.database.general.dao;
using TODORoutine.database.note.dto;
using TODORoutine.database.parsers;
using TODORoutine.database.task.dao;
using TODORoutine.general;
using TODORoutine.general.enums;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.task.dto {
    class TaskDTOImplentation : TaskDTO {

        private static TaskDTO taskDTO = null;
        private readonly TaskDAO taskDAO = null;

        private TaskDTOImplentation() => taskDAO = TaskDAOImplementation.getInstance();

        public static TaskDTO getInstance() {
            if (taskDTO == null) taskDTO = new TaskDTOImplentation();
            return taskDTO;
        }

        /**
         * Delete TaskNote based on the id
         * 
         * @id : the id of the task that will be deleted
         * 
         * return true if and only if the delete operation was successfull
         **/
        public bool delete(String id) {
            try {
                return taskDAO.delete(id);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Getting the task based on the id
         * 
         * @id : the id of the task that will be searched for
         * 
         * return a task if it was found and null otherwise
         **/
        public TaskNote getById(String id) {
            try {
                return taskDAO.findById(id);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }

        /**
         * Saving a task in the databse
         * 
         * @task : the task that will be saved
         * 
         * return true if and only if the task was successfu;ly saved
         **/
        public bool save(TaskNote task) {
            try {
                bool flag = taskDAO.save(task);
                if (flag) {
                    task.id = DatabaseDAOImplementation<TaskNote>.getLastId(DatabaseConstants.TABLE_TASK);
                    return true;
                }
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Updating the task in the database
         * 
         * @task : the task that will get updated
         * @columns : the database columns that will be updated
         * 
         * return true if and only if the update operation was done successfully
         **/
        public bool update(TaskNote task , params String[] columns) {
            try {
                return taskDAO.update(task , columns);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        public List<TaskNote> getAllByPriority(Priority priority) {
            try {
                List<TaskNote> notes = new List<TaskNote>();
                taskDAO.findAllByPriority(priority).ForEach(id => notes.Add(getById(id)));
                return notes;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<TaskNote>();
        }

        public List<TaskNote> getAllByStatus(Status status) {
            try {
                List<TaskNote> notes = new List<TaskNote>();
                taskDAO.findAllByStatus(status).ForEach(id => notes.Add(getById(id)));
                return notes;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<TaskNote>();
        }

        public Note getNote(String taskId) {
            try {
                return NoteDTOImplementation.getInstance().getById(taskDAO.findNote(taskId));
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return new Note();
            }
        }

        public List<TaskNote> getAllTasksOrderByDueDate(String lastTaskId = "1") {
            try {
                List<TaskNote> taskNotes = new List<TaskNote>();
                taskDAO.findAllByOrderOfDueDate(lastTaskId).ForEach(id => taskNotes.Add(getById(id)));
                return taskNotes;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return new List<TaskNote>();
            }
        }

        public List<TaskNote> getAllTasks(String lastTaskId = "1") {
            try {
                List<TaskNote> taskNotes = new List<TaskNote>();
                taskDAO.findAll(lastTaskId).ForEach(id => taskNotes.Add(getById(id)));
                return taskNotes;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
                return new List<TaskNote>();
            }
        }
    }
}
