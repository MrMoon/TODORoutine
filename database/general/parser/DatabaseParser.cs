﻿using System;

namespace TODORoutine.database.parsers {
    /**
     * Database Parser Interface that has all the methods for the database parser 
     * Handle String Parsing for the main SQL statments SELECT , INSERT , UPDATE , and DELETE
     **/
    public interface DatabaseParser<T> {
        String getWhere(String filter , String condition);
        String getSelect(String tableName , String filter = "" , String column = "*" , String condition = "" , int from = 0 , int to = 0);
        String getDelete(String tableName , String filter , String condition);
        String getLastAddedRecored(String tableName);
        String getInsert(T t);
        String getUpdate(String tableName , String filter , String condition , T t , params String[] columns);
        String getFieldFromColumn(string column , T t);
    }
}