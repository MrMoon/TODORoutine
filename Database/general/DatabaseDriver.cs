using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Shared;

namespace TODORoutine.Database {
    class DatabaseDriver {

        private static DatabaseDriver driver = null;
        private static SQLiteConnection connection = null;
        private static SQLiteCommand command = null;
        private static SQLiteDataReader reader = null;
        
        private DatabaseDriver() { initDatabase(); }

        public static DatabaseDriver getInstance() {
            if (driver == null) driver = new DatabaseDriver();
            return driver;
        }

        private static void initDatabase() {
            if (!File.Exists(Constants.DATABASE_NAME)) {
                SQLiteConnection.CreateFile(Constants.DATABASE_NAME);
                createMainTable();
            } else setupDatabaseConnection();
        }

        private static void setupDatabaseConnection() {
            command = new SQLiteCommand();
            connection = new SQLiteConnection(Constants.CONNECTION_STRING);
            connection.Open();
            command.Connection = connection;
        }

        private static void createMainTable() {
            setupDatabaseConnection();
            command.CommandText = Constants.CREATE_TABLE;
            command.ExecuteNonQuery();
            connection.Close();
        }
        public int executeQuery(String query) {
            int n;
            setupDatabaseConnection();
            command.CommandText = query;
            n = command.ExecuteNonQuery();
            connection.Close();
            return n;
        }
    }
}
