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
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

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
            Logging.logInfo(false , "Inititating Database");
            if (!File.Exists(DatabaseConstants.DATABASE_NAME)) {
                Logging.logInfo(false , "Database File Doesn't Exsist , Creart a new One");
                SQLiteConnection.CreateFile(DatabaseConstants.DATABASE_NAME);
                createTable();
            } else setupDatabaseConnection();
        }

        /**
         * Setting up database connection
         **/
        public void setupDatabaseConnection() {
            Logging.logInfo(false , "Setting up Database Connection");
            command = new SQLiteCommand();
            connection = new SQLiteConnection(DatabaseConstants.CONNECTION_STRING);
            connection.Open();
            command.Connection = connection;
        }

        /**
         * Creating Main Database Table
         **/
        public void createTable(String query = "") {
            Logging.logInfo(false , "Creating Table " , query);
            setupDatabaseConnection();
            command.CommandText = query.Equals("") ? DatabaseConstants.CREATE_TODOROUTINE_TABLE : query;
            Console.WriteLine(query);
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
            Logging.logInfo(false , "Executing Query " , query);
            int n = 0;
            setupDatabaseConnection();
            command.CommandText = query;
            try {
                command.ExecuteNonQuery();
            } catch (System.Data.SQLite.SQLiteException e) {
                if (e.Message.Contains("constraint failed UNIQUE constraint failed")) return -1;
            } finally {
                connection.Close();
                Logging.logInfo(false , "Number of Effected Recorders is " , n.ToString());
            }
            return n;
        }

    }
}
