using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.database.parsers {
    /**
     * Main Database Constants Class that has all the important strings so it would be easier to edit and scale
     **/
    public class DatabaseConstants {
        //Main Database Name
        public readonly static String DATABASE_NAME = "TODORoutine.sqlite";
        //Table TODORoutine Strings
        public readonly static String ALL = "*";
        public readonly static String TABEL_TODOROUTINE = "TODORoutine";
        public readonly static String COLUMN_USERID = "USERID";
        public readonly static String COLUMN_USERNAME = "USERNAME";
        public readonly static String COLUMN_NOTESID = "NOTESID";
        public readonly static String COLUMN_FULLNAME = "FULLNAME";
        public readonly static String CREATE_TABLE = @"CREATE TABLE" + TABEL_TODOROUTINE + "("
                                            + COLUMN_USERID + "TEXT NOT NULL UNIQUE PRIMARY KEY,"
                                            + COLUMN_USERNAME + "TEXT NOT NULL UNIQUE,"
                                            + COLUMN_NOTESID + "TEXT NOT NULL UNIQUE,"
                                            + COLUMN_FULLNAME + "TEXT NOT NULL);";
        public readonly static String CONNECTION_STRING = "Data Source = TODORoutine.sqlite; Version = 3;";
    }
}
