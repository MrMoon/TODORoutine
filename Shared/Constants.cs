using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.Shared {
    class Constants {
        public static String DATABASE_NAME = "TODORoutine.sqlite";
        public static String CREATE_TABLE = @"CREATE TABLE TODORoutine (ID INTEGER  PRIMARY KEY AUTOINCREMENT,Name TEXT,NotesID TEXT);";

        public static String getConnectionString() { return "Data Source = TODORoutine.sqlite; Version = 3;"; }
    }
}
