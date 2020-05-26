using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TODORoutine.database.parsers;
using TODORoutine.Database;
using TODORoutine.exceptions;
using TODORoutine.Shared;

namespace TODORoutine.database.general.dao {
    abstract class DatabaseDAOImplementation<T> : DatabaseDAO<T> {

        /**
         * Getting all records in range
         * 
         * @parser : the object database parser
         * @tableName : the tableName for the records
         * @orderByColumnName : the name of the column to order the data
         * @lastTId : the last id of the previous call
         * 
         * Return a list of ids
         **/
        public List<String> findAll(DatabaseParser<T> parser , String tableName , String orderbyColumnName  = "", String lastTId = "1") {
            //Logging
            Logging.paramenterLogging(nameof(findAll) , false 
                , new Pair(nameof(orderbyColumnName) , orderbyColumnName) , new Pair(nameof(tableName) , tableName)
                , new Pair(nameof(lastTId) , lastTId));
            //Finding T
            List<String> ids = new List<String>();
            try {
                int lastId = int.Parse(lastTId), range = int.Parse(DatabaseConstants.RANGE);
                String query = "";
                if (orderbyColumnName.Equals("-1")) query = "SELECT ID FROM " + tableName + " WHERE ID BETWEEN " + lastTId + " AND " + (int.Parse(lastTId) + 20).ToString();
                else query = parser.getSelect(tableName , ""
                                            , DatabaseConstants.COLUMN_ID , "" , true
                                            , lastId , lastId + 20 , orderbyColumnName != "" , orderbyColumnName );
                SQLiteDataReader reader = DatabaseDriverImplementation.getInstance()
                            .getReader(query);
                while (reader.Read()) ids.Add(reader[DatabaseConstants.COLUMN_ID].ToString());
                reader.Close();
                return ids;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }

            throw new DatabaseException(DatabaseConstants.INVALID("EMPTY"));
        }

        /**
         * Get the last id for the inseted value
         * 
         * @tableName : the tableName for the id that was inserted in
         * 
         * return an id 
         **/
        public static String getLastId(String tableName) {
            //Logging
            Logging.paramenterLogging(nameof(getLastId) , false , new Pair(nameof(tableName) , tableName));
            try {
                SQLiteDataReader reader = DatabaseDriverImplementation.getInstance()
                    .getReader(DatabaseParserImplementation<T>.getLastAddedRecored(tableName));
                if (reader.Read()) {
                    String id = reader[DatabaseConstants.COLUMN_ID].ToString();
                    reader.Close();
                    return id;
                }
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            Logging.paramenterLogging(nameof(getLastId) , true , new Pair(nameof(tableName) , tableName));
            throw new DatabaseException(DatabaseConstants.NOT_FOUND("EMPTY"));
        }

        public abstract T findById(String id);
        public abstract T find(SQLiteDataReader reader);
        public abstract bool save(T t);
        public abstract bool update(T t , params String[] columns);
        public abstract bool delete(String id);
    }
}
