﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.database.general {

    /**
     * Database Driver Interface that has all the methods for the database drivers
     * Handles the main database operations from creating the tables to executing SQL statments
     **/
    interface DatabaseDriver {
        void initDatabase();
        void setupDatabaseConnection();
        void createTable(String query = "");
        SQLiteDataReader getReader(String query);
        int executeQuery(String query);
    }
}
