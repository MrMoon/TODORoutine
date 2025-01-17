﻿using System;
using System.Data.SQLite;

namespace TODORoutine.database.general.driver {

    /**
     * Database Driver Interface that has all the methods for the database drivers
     * Handles the main database operations from creating the tables to executing SQL statments
     **/
    interface DatabaseDriver {
        void initDatabase();
        void setupDatabaseConnection();
        void createTable(String query);
        int executeQuery(String query);
        SQLiteDataReader getReader(String query);
        SQLiteCommand getBLOBCommand(SQLiteConnection connection , String query , String parameter , byte[] file);
    }
}
