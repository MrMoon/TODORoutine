using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Shared;
using TODORoutine.database.parsers;
using TODORoutine.database.general;

namespace TODORoutine.Database {
    /**
     * Main Database Driver Class that will take care of database initilizing 
     *      , creating tables and handling querys and connections
     **/
    class DatabaseDriverImplementation : DatabaseDriver {

        private static DatabaseDriverImplementation driver = null;
        private static SQLiteConnection connection = null;
        private static SQLiteCommand command = null;
        private static SQLiteDataReader reader = null;
        
        private DatabaseDriverImplementation() { initDatabase();  }

        public static DatabaseDriverImplementation getInstance() {
            if (driver == null) driver = new DatabaseDriverImplementation();
            return driver;
        }
        /**
         * Initinating Database Files
         **/
        public void initDatabase() {
            Logging.logInfo("Inititating Database");
            if (!File.Exists(DatabaseConstants.DATABASE_NAME)) {
                Logging.logInfo("Database File Doesn't Exsist , Creart a new One");
                SQLiteConnection.CreateFile(DatabaseConstants.DATABASE_NAME);
                createTable();
            } else setupDatabaseConnection();
        }

        /**
         * Setting up database connection
         **/
        public void setupDatabaseConnection() {
            Logging.logInfo("Setting up Database Connection");
            if(command == null) command = new SQLiteCommand();
            if(connection == null) connection = new SQLiteConnection(DatabaseConstants.CONNECTION_STRING);
            connection.Open();
            command.Connection = connection;
        }

        /**
         * Creating Main Database Table
         **/
        public void createTable(String query = "") {
            Logging.logInfo("Creating Table " , query);
            setupDatabaseConnection();
            command.CommandText = query.Equals("") ? DatabaseConstants.CREATE_TABLE : query;
            command.ExecuteNonQuery();
            connection.Close();
        }
        /**
         * Return a reader to the DAO Class to read Database Records
         * 
         * @query : the query for the reader
         * 
         * return SQLiteDataReader
         **/
        public SQLiteDataReader getReader(String query) {
            setupDatabaseConnection();
            command.CommandText = query;
            reader = command.ExecuteReader();
            return reader;
        }

        /**
         * A Method to Execute SQL statments
         * 
         * @query : the SQL Statment
         * 
         * return the number of effected Recorders
         **/
        public int executeQuery(String query) {
            Logging.logInfo("Executing Query " , query);
            int n;
            setupDatabaseConnection();
            command.CommandText = query;
            n = command.ExecuteNonQuery();
            connection.Close();
            Logging.logInfo("Number of Effected Recorders is " , n.ToString());
            return n;
        }
    }
}
