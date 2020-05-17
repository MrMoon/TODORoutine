using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Models;

namespace TODORoutine.database.parsers {
    /**
     * Database Parser Interface that has all the methods for the database parser 
     * Handle String Parsing for the main SQL statments SELECT , INSERT , UPDATE , and DELETE
     **/
    interface DatabaseParser<T> {
        String getWhere(String filter , String condition);
        String getSelect(String tableName , String filter = "" , String column = "*" , String condition = "");
        String getDelete(String tableName , String filter , String condition);
        String getInsert(T user);
        String getUpdate(String tableName , String filter , String condition , T user , params String[] values);
    }
}
