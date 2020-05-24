using System;
using System.Collections.Generic;
using TODORoutine.database.general.dto;
using TODORoutine.general;
using TODORoutine.general.enums;
using TODORoutine.models;

namespace TODORoutine.database.task.dto {
    interface TaskDTO : DatabaseDTO<TaskNote> {
        List<TaskNote> getAllByPriority(Priority priority);
        List<TaskNote> getAllByStatus(Status status);
        Note getNote(String taskId);
        List<TaskNote> getTasksAllOrderByDueDate(String lastTaskId = "1");
    }
}
