using System;

namespace TODORoutine.database.parsers {
    /**
     * Main Database Constants Class that has all the important strings so it would be easier to edit and scale
     **/
    public abstract class DatabaseConstants {
        //Trivaial Strings
        public readonly static String ALL = "*";
        public readonly static String RANGE = (20).ToString();
        //Messages
        public static Func<String , String> INVALID = (s) => "Invalid " + s;
        public static Func<String , String> NOT_FOUND = (s) => s + " was not found";
        public static Func<String , String> FOUND = (s) => s + " was found successfully";
        public static readonly String EMPTY_UPDATE = "There is Nothing to Update\n";
        //Main Database Strings
        public readonly static String DATABASE_NAME = "TODORoutine.sqlite";
        public readonly static String CONNECTION_STRING = "Data Source = TODORoutine.sqlite; Version = 3;";
        //Table TODORoutine Strings
        public readonly static String TABLE_TODOROUTINE = "TODORoutine";
        public readonly static String COLUMN_USERID = "USERID";
        public readonly static String COLUMN_USERNAME = "USERNAME";
        public readonly static String COLUMN_NOTESID = "NOTESID";
        public readonly static String COLUMN_FULLNAME = "FULLNAME";
        public readonly static String COLUMN_AUTH = "AUTH";
        public readonly static String CREATE_TODOROUTINE_TABLE = @"CREATE TABLE " + TABLE_TODOROUTINE + " ( "
	                                                    + COLUMN_USERID + " INTEGER PRIMARY KEY AUTOINCREMENT,"
	                                                    + COLUMN_USERNAME + " TEXT UNIQUE NOT NULL,"
	                                                    + COLUMN_NOTESID +  " INTEGER UNIQUE,"
	                                                    + COLUMN_FULLNAME + " TEXT NOT NULL,"
                                                        + COLUMN_AUTH + " INTEGER);";
        //Table Note Strings
        public readonly static String TABLE_NOTE = "Note";
        public readonly static String COLUMN_NOTEID = "NOTEID";
        public readonly static String COLUMN_TITLE = "TITLE";
        public readonly static String COLUMN_AUTHOR = "AUTHOR";
        public readonly static String COLUMN_DATECREATED = "DATECREATED";
        public readonly static String COLUMN_LASTMODIFIED = "LASTMODIFIED";
        public readonly static String COLUMN_DOCUMENTID = "DOCUMNETID";
        public readonly static String CREATE_NOTE_TABLE = @"CREATE TABLE " + TABLE_NOTE + " ( "
                                                            + COLUMN_NOTEID + " INTEGER PRIMARY KEY AUTOINCREMENT,"
                                                            + COLUMN_TITLE + " TEXT UNIQUE,"
                                                            + COLUMN_AUTHOR + " TEXT NOT NULL,"
                                                            + COLUMN_DATECREATED + " TEXT NOT NULL,"
                                                            + COLUMN_LASTMODIFIED + " TEXT,"
                                                            + COLUMN_DOCUMENTID + " TEXT);";
        //Table Document Strings
        public readonly static String DOCUMENT_PARAMETER = ":Documents";
        public readonly static String TABLE_DOCUMENT = "Document";
        public readonly static String COLUMN_DOCUMENT = "DOCUMNET";
        public readonly static String COLUMN_OWENER = "OWENER";
        public readonly static String COLUMN_SHARED = "SHARED";
        public readonly static String CREATE_DOCUMENT_TABLE = @"CREATE TABLE " + TABLE_DOCUMENT + " ( "
                                                            + COLUMN_DOCUMENTID + " INTEGER PRIMARY KEY AUTOINCREMENT,"
                                                            + COLUMN_OWENER + " TEXT NOT NULL,"
                                                            + COLUMN_SHARED + " TEXT NOT NULL,"
                                                            + COLUMN_DOCUMENT + " BLOB);";
        //Table Notebook Strings
        public readonly static String TABLE_NOTEBOOK = "Notebook";
        public readonly static String COLUMN_NOTEBOOKID = "NOTEBOOKID";
        public readonly static String CREATE_NOTEBOOK_TABLE = @"CREATE TABLE " + TABLE_NOTEBOOK + " ( "
                                                            + COLUMN_NOTEBOOKID + " INTEGER PRIMARY KEY AUTOINCREMENT,"
                                                            + COLUMN_TITLE + " TEXT NOT NULL,"
                                                            + COLUMN_AUTHOR + " TEXT NOT NULL,"
                                                            + COLUMN_DATECREATED + " TEXT NOT NULL,"
                                                            + COLUMN_LASTMODIFIED + " TEXT,"
                                                            + COLUMN_NOTESID + " TEXT);";
    }
}
