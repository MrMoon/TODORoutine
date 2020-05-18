using System;
using System.Data.SQLite;

namespace TODORoutine.database.general.dao {

    /**
     * Main Database Genric Access Layer
     * Handle basic data operations 
     **/
    public interface DatabaseDAO<T> {
        T findById(String id);
        bool save(T t);
        bool update(T t , params String[] columns);
        bool delete(T t);
        T getT(SQLiteDataReader dataReader);
    }
}
