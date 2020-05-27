using System;
using System.Text;
using TODORoutine.Shared;

namespace TODORoutine.database.parsers {
    /**
     * Main Database Constants Class that has all the important Strings so it would be easier to edit and scale
     **/
    public abstract class DatabaseConstants {
        //Trivaial Strings
        public readonly static String ALL = "*";
        public readonly static String RANGE = (20).ToString();
        public readonly static String COLUMN_ID = "ID";
        public readonly static String ID = COLUMN_ID + " INTEGER PRIMARY KEY AUTOINCREMENT,";
        //Messages
        public static Func<String , String> INVALID = (s) => "Invalid " + s;
        public static Func<String , String> NOT_FOUND = (s) => s + " was not found";
        public static Func<String , String> FOUND = (s) => s + " was found successfully";
        public static readonly String EMPTY_UPDATE = "There is Nothing to Update\n";
        //Main Database Strings
        public readonly static String DATABASE_NAME = "TODORoutine.sqlite";
        public readonly static String CONNECTION_STRING = "Data Source = TODORoutine.sqlite; Version = 3;";
        //Creating tables
        private static String getTableStatment(String tableName , params Pair[] columns) {
            String prefix = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE ");
            sb.Append(tableName);
            sb.Append(" ( ");
            sb.Append(ID);
            foreach(Pair pair in columns) {
                sb.Append(prefix);
                prefix = ",";
                sb.Append(pair.first);
                sb.Append(" ");
                sb.Append(pair.second);
            }
            sb.Append(";");
            return sb.ToString();
        }
        //Table User Strings
        public readonly static String TABLE_USER = "User";
        public readonly static String COLUMN_USERNAME = "USERNAME";
        public readonly static String COLUMN_FULLNAME = "FULLNAME";
        public readonly static String COLUMN_AUTH = "AUTH";
        public readonly static String COLUMN_NOTEBOOKID = "NOTEBOOKID";
        public readonly static String CREATE_USER_TABLE = @"CREATE TABLE " + TABLE_USER + " ( "
                                                        + ID
                                                        + COLUMN_USERNAME + " TEXT NOT NULL UNIQUE,"
                                                        + COLUMN_NOTEBOOKID + " TEXT,"
                                                        + COLUMN_FULLNAME + " TEXT NOT NULL,"
                                                        + COLUMN_AUTH + " INTEGER);";
        //Table Note Strings
        public readonly static String TABLE_NOTE = "Note";
        public readonly static String COLUMN_NOTEID = "NOTEID";
        public readonly static String COLUMN_TITLE = "TITLE";
        public readonly static String COLUMN_AUTHOR = "AUTHOR";
        public readonly static String COLUMN_DATECREATED = "DATECREATED";
        public readonly static String COLUMN_LASTMODIFIED = "LASTMODIFIED";
        public readonly static String COLUMN_NOTESID = "NOTESID";
        public readonly static String COLUMN_DOCUMENTID = "DOCUMNETID";
        public readonly static String CREATE_NOTE_TABLE = @"CREATE TABLE " + TABLE_NOTE + " ( "
                                                            + ID
                                                            + COLUMN_TITLE + " TEXT NOT NULL,"
                                                            + COLUMN_AUTHOR + " TEXT NOT NULL,"
                                                            + COLUMN_DATECREATED + " TEXT NOT NULL,"
                                                            + COLUMN_LASTMODIFIED + " TEXT NOT NULL,"
                                                            + COLUMN_DOCUMENTID + " TEXT);";
        //Table Document Strings
        public readonly static String DOCUMENT_PARAMETER = "@Documents";
        public readonly static String TABLE_DOCUMENT = "Document";
        public readonly static String COLUMN_DOCUMENT = "DOCUMNET";
        public readonly static String COLUMN_OWENER = "OWENER";
        public readonly static String COLUMN_SHARED = "SHARED";
        public readonly static String CREATE_DOCUMENT_TABLE = @"CREATE TABLE " + TABLE_DOCUMENT + " ( "
                                                            + ID
                                                            + COLUMN_OWENER + " TEXT NOT NULL,"
                                                            + COLUMN_SHARED + " TEXT NOT NULL,"
                                                            + COLUMN_DOCUMENT + " BLOB);";
        //Table Notebook Strings
        public readonly static String TABLE_NOTEBOOK = "Notebook";
        public readonly static String CREATE_NOTEBOOK_TABLE = @"CREATE TABLE " + TABLE_NOTEBOOK + " ( "
                                                            + ID
                                                            + COLUMN_TITLE + " TEXT NOT NULL,"
                                                            + COLUMN_AUTHOR + " TEXT NOT NULL,"
                                                            + COLUMN_DATECREATED + " TEXT NOT NULL,"
                                                            + COLUMN_LASTMODIFIED + " TEXT,"
                                                            + COLUMN_NOTESID + " TEXT);";
        //Table DocumentShare Strings
        public readonly static String TABLE_DOCUMENT_SHARE = "DocumentShare";
        public readonly static String COLUMN_USERID = "USERID";
        public readonly static String COLUMN_DOCUMENTSIDS = "DOCUMENTSIDS";
        public readonly static String CREATE_DOCUMENT_SHARE_TABLE = @"CREATE TABLE " + TABLE_DOCUMENT_SHARE + " ( "
                                                            + COLUMN_USERID + " TEXT UNIQUE NOT NULL,"
                                                            + COLUMN_DOCUMENTSIDS + " TEXT NOT NULL);";
        //Table Authenticate Strings
        public readonly static String TABLE_AUTHENTICATE = "Authenticate";
        public readonly static String COLUMN_PASSWORD = "PASSWORD";
        public readonly static String CREATE_AUTHENTICATE_TABLE = @"CREATE TABLE " + TABLE_AUTHENTICATE + " ( "
                                                            + COLUMN_USERNAME + " TEXT UNIQUE NOT NULL,"
                                                            + COLUMN_PASSWORD + " TEXT NOT NULL);";
        //Table Task Strings
        public readonly static String TABLE_TASK = "Task";
        public readonly static String COLUMN_DUEDATE = "DUEDATE";
        public readonly static String COLUMN_STATUS = "STATUS";
        public readonly static String COLUMN_PRIORITY = "PRIORITY";
        public readonly static String CREATE_TASK_TABLE = @"CREATE TABLE " + TABLE_TASK + " ( "
                                                            + ID
                                                            + COLUMN_PRIORITY + " TEXT NOT NULL,"
                                                            + COLUMN_NOTEID + " TEXT NOT NULL,"
                                                            + COLUMN_DUEDATE + " TEXT NOT NULL,"
                                                            + COLUMN_STATUS + " TEXT NOT NULL);";
    }
}
