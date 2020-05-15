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
     **/
    interface DatabaseParser {
        String getWhere(String filter , String condition);
        String getSelect(String tableName , String filter = "" , String column = "*" , String condition = "");
        String getDelete(String tableName , String filter , String condition);
        String getInsert(User user);
        String getUpdate(String tableName , String filter , String condition , ArrayList columns , User user);
    }
}
