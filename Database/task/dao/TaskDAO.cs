using System;
using System.Collections.Generic;
using TODORoutine.database.general.dao;
using TODORoutine.general.enums;
using TODORoutine.models;

namespace TODORoutine.database.task.dao {
    /**
     * Task Data Access Layer
     * Handles Data Operations for the Task
     **/
    interface TaskDAO : DatabaseDAO<TaskNote> {
        String findNote(String taskId);
        List<String> findAllByOrderOfDueDate(String lastTaskId = "1");
        List<String> findAllByPriority(Priority priority);
        List<String> findAllByStatus(Status status);
        List<String> findAll(String lastTaskId = "1");
    }
}
