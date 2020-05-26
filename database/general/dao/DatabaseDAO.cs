using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TODORoutine.database.parsers;

namespace TODORoutine.database.general.dao {

    /**
     * Main Database Genric Access Layer
     * Handle basic data operations 
     **/
    interface DatabaseDAO<T> {
        T findById(String id);
        bool save(T t);
        bool update(T t , params String[] columns);
        bool delete(String id);
        T find(SQLiteDataReader reader);
        List<String> findAll(DatabaseParser<T> parser , String tableName , String orderbyColumnName = "" , String lastId = "1");
    }
}
