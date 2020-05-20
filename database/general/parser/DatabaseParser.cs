using System;

namespace TODORoutine.database.parsers {
    /**
     * Database Parser Interface that has all the methods for the database parser 
     * Handle String Parsing for the main SQL statments SELECT , INSERT , UPDATE , and DELETE
     **/
    interface DatabaseParser<T> {
        String getWhere(String filter , String condition);
        String getSelect(String tableName , String filter = "" , String column = "*" , String condition = "" , bool range = false , int from = 0 , int to = 21 , bool isOrder = false , String orderColumn = "");
        String getDelete(String tableName , String filter , String condition);
        String getInsert(T t);
        String getUpdate(String tableName , String filter , String condition , T t , params String[] columns);
        String getFieldFromColumn(String column , T t);
    }
}
