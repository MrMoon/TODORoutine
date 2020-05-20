using System;
using System.Data.SQLite;
using System.IO;
using TODORoutine.Shared;
using TODORoutine.database.parsers;
using TODORoutine.database.general;
using TODORoutine.exceptions;
using System.Data;

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
            try {
                command.CommandText = query.Equals("") ? DatabaseConstants.CREATE_USER_TABLE : query;
                command.ExecuteNonQuery();
                connection.Close();
            } catch(Exception e) {
                if (e.Message.Contains("already exists")) Console.WriteLine("Table Exists");
                Logging.logInfo(true , e.Message);
            }
        }
        /**
         * Return a reader to the DAO Class to read Database Records
         * 
         * @query : the query for the reader
         * 
         * return SQLiteDataReader
         **/
        public SQLiteDataReader getReader(String query) {
            try {
                setupDatabaseConnection();
                command.CommandText = query;
                reader = command.ExecuteReader();
                return reader;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }   
            throw new DatabaseException(DatabaseConstants.INVALID("404"));
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
            try {
                command.CommandText = query;
                n = command.ExecuteNonQuery();
            } catch (System.Data.SQLite.SQLiteException e) {
                if (e.Message.Contains("constraint failed UNIQUE constraint failed")) {
                    Console.WriteLine("Record Exsists");
                    return -1;
                }
            } finally {
                connection.Close();
                Logging.logInfo(false , "Number of Effected Recorders is " , n.ToString());
            }
            return n == -1 ? 0 : n;
        }

        public SQLiteCommand getBLOBCommand(SQLiteConnection connection , String query , String parameter , byte[] file) {
            try {
                command.Connection = connection;
                connection.Open();
                command.CommandText = query;
                command.Parameters.Add(parameter , DbType.Binary , 20).Value = file;
                return command;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }

            throw new DatabaseException("Something went Wrong");
        }
    }
}
