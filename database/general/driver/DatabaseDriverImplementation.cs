using System;
using System.Data.SQLite;
using System.IO;
using System.Data;
using TODORoutine.database.general.shared;
using TODORoutine.general.logging;
using TODORoutine.models;
using TODORoutine.database.general.exception;

namespace TODORoutine.database.general.driver {

    /**
     * Main Database Driver Class that will take care of database initilizing 
     *      , creating tables and handling querys and connections
     **/
    class DatabaseDriverImplementation : DatabaseDriver {

        private static DatabaseDriverImplementation driver = null;
        private static SQLiteConnection connection = null;
        private static SQLiteCommand command = null;
        private static SQLiteDataReader reader = null;
        
        private DatabaseDriverImplementation() => initDatabase();

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
         * 
         * @query : the create table query
         **/
        public void createTable(String query) {
            Logging.logInfo(false , "Creating Table " , query);
            setupDatabaseConnection();
            try {
                executeQuery(query);
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
         * return SQLiteDataReader with it's query
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
         * A Method to Execute all SQL statments
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
                    System.Windows.Forms.MessageBox.Show("This must be unique");
                    return -11;
                } else if(e.Message.Contains("already exists")) {
                    Console.WriteLine("Table Exsists");
                }
            } finally {
                connection.Close();
                Logging.logInfo(false , "Number of Effected Recorders is " , n.ToString());
            }
            return n == -1 ? 0 : n;
        }

        /**
         * Getting SQLiteCommand for the BLOB
         * 
         * @connection : the sql connection
         * @query : the insert query
         * @parameter : the blob parameter
         * @file : the bytes to insert into the database
         * 
         * return a SQLiteCommand with all of it's configuration
         **/
        public SQLiteCommand getBLOBCommand(SQLiteConnection connection , String query , String parameter , byte[] file) {
            //Logging
            Logging.paramenterLogging(nameof(getBLOBCommand) , false , new Pair(nameof(connection) , connection.ToString())
                            , new Pair(nameof(query) , query) , new Pair(nameof(parameter) , parameter)
                            , new Pair(nameof(file) , file.ToString()));
            //Setting up the command
            try {
                command.Connection = connection;
                connection.Open();
                command.CommandText = query;
                command.Parameters.Add(parameter , DbType.Binary , 20).Value = file;
                return command;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(getBLOBCommand) , false , new Pair(nameof(connection) , connection.ToString())
                            , new Pair(nameof(query) , query) , new Pair(nameof(parameter) , parameter)
                            , new Pair(nameof(file) , file.ToString()));
            //Inserting went wrong
            throw new DatabaseException("Something went Wrong");
        }
    }
}
