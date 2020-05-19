using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TODORoutine.database.parsers;
using TODORoutine.Database;
using TODORoutine.Shared;

namespace TODORoutine.database.general.dao {
    abstract class DatabaseDAOImplementation<T> : DatabaseDAO<T>{

        public List<String> findAll(DatabaseParser<T> parser , String tableName , String orderbyColumnName , String idColumnName , String lastTId) {
            //Logging
            Logging.paramenterLogging(nameof(findAll) , false 
                , new Pair(nameof(orderbyColumnName) , orderbyColumnName) , new Pair(nameof(tableName) , tableName)
                , new Pair(nameof(idColumnName) , idColumnName) , new Pair(nameof(lastTId) , lastTId));
            //Finding T
            List<String> notebooksIds = new List<String>();
            try {
                int lastId = int.Parse(lastTId), range = int.Parse(DatabaseConstants.RANGE);
                SQLiteDataReader reader = DatabaseDriverImplementation.getInstance().getReader(parser.getSelect(tableName , ""
                                            , DatabaseConstants.COLUMN_NOTEBOOKID , ""
                                            , lastId == 0 ? 0 : lastId - 1 , lastId == 0 ? lastId : lastId + range
                                            , orderbyColumnName != "" , orderbyColumnName != "" ? orderbyColumnName : ""));
                while (reader.Read()) notebooksIds.Add(reader[idColumnName].ToString());
                reader.Close();
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return notebooksIds;
        }

        public abstract T findById(String id);
        public abstract T get(SQLiteDataReader reader);
        public abstract bool save(T t);
        public abstract bool update(T t , params String[] columns);
        public abstract bool delete(String id);
    }
}
