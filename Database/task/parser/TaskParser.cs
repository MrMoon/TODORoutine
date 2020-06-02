using TODORoutine.database.general.parser;
using TODORoutine.models;

namespace TODORoutine.database.task.parser {
    /**
     * Main Task Database Parser that handle Insert Statments for the Tasks , 
     * Parsing Columns and SQL Statments for the task class
     **/
    interface TaskParser : DatabaseParser<TaskNote> {

    }
}
