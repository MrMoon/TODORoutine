using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TODORoutine.database.general;
using TODORoutine.database.general.dao;
using TODORoutine.database.parsers;
using TODORoutine.database.parsers.notes_parser;
using TODORoutine.Database;
using TODORoutine.exceptions;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.note.dao {

    /**
     * Main Note Data Access Implementation that handle database operations
     **/
    class NoteDAOImplentation : DatabaseDAOImplementation<Note> , NoteDAO {

        private readonly String idColumn = DatabaseConstants.COLUMN_ID;
        private readonly String tableName = DatabaseConstants.TABLE_NOTE;
        private static NoteDAO noteDAO = null;
        private DatabaseDriver driver = null;
        private NoteParser parser = null;

        private NoteDAOImplentation() {
            parser = NoteParserImplementation.getInstance();
            driver = DatabaseDriverImplementation.getInstance();
            driver.createTable(DatabaseConstants.CREATE_NOTE_TABLE);
        }

        public static NoteDAO getInsence() {
            if (noteDAO == null) noteDAO = new NoteDAOImplentation();
            return noteDAO;
        }

        /**
        * deleting the note from the Database
        * 
        * @note : the note that will get deleted
        * 
        * return true if and only if the delete operation was done successfully
        **/
        public override bool delete(String id) {
            //Logging
            Logging.paramenterLogging(nameof(delete) , false
                , new Pair(nameof(id) , id));
            //Deleting note from database
            try {
                driver.executeQuery(parser.getDelete(tableName , idColumn , id));
                return true;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(delete) , true
                , new Pair(nameof(id) , id));
            //Note not found in the database
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        public List<String> findAllByOrderOfDateCreated(String lastNoteId = "1") {
            //Logging
            Logging.paramenterLogging(nameof(findAllByOrderOfDateCreated) , false , new Pair(nameof(lastNoteId) , lastNoteId));
            return findAll(parser , tableName , DatabaseConstants.COLUMN_DATECREATED , lastNoteId);
        }

        public List<String> findAllByOrderOfLastModified(String lastNoteId = "1") {
            //Logging
            Logging.paramenterLogging(nameof(findAllByOrderOfLastModified) , false , new Pair(nameof(lastNoteId) , lastNoteId));
            return findAll(parser , tableName , DatabaseConstants.COLUMN_LASTMODIFIED , lastNoteId);
        }

        public List<String> findByAuthorName(String author) {
            //Logging
            Logging.paramenterLogging(nameof(findByAuthorName) , false , new Pair(nameof(author) , author));
            //Finding notes id
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName
                                    , DatabaseConstants.COLUMN_AUTHOR , idColumn , author));
                List<String> notesIds = new List<String>();
                while (reader.Read()) notesIds.Add(reader[idColumn].ToString());
                reader.Close();
                return notesIds;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findByAuthorName) , true , new Pair(nameof(author) , author));
            //Note not found in the database
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(author));
        }

        /**
         * Getting the note from the database based on the id
         * 
         * @id : the note id to search for
         * 
         * return note if found and throw an Exception otherwise
         **/
        public override Note findById(String id) {
            //Logging
            Logging.paramenterLogging(nameof(findById) , false , new Pair(nameof(id) , id));
            //Finding the note
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , idColumn , "*" , id));
                //Reading the the Record from the database
                Note note = find(reader);
                Logging.logInfo(false , nameof(findById) , DatabaseConstants.FOUND(id) , note.ToString());
                reader.Close();
                return note;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findById) , true , new Pair(nameof(id) , id));
            //Note not found in the database
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        public Note findByTitle(String title) {
            //Logging
            Logging.paramenterLogging(nameof(findByTitle) , false , new Pair(nameof(title) , title));
            //Finding the note
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName
                                            , DatabaseConstants.COLUMN_TITLE , DatabaseConstants.ALL , title));
                Note note = find(reader);
                Logging.logInfo(false , note.ToString());
                reader.Close();
                return note;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findByTitle) , true , new Pair(nameof(title) , title));
            //Note was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(title));
        }

        public override Note find(SQLiteDataReader reader) {
            if(reader.Read()) {
                Note note = new Note();
                note.setAuthor(reader[DatabaseConstants.COLUMN_AUTHOR].ToString());
                note.setDocumentId(reader[DatabaseConstants.COLUMN_DOCUMENTID].ToString());
                note.setId(reader[idColumn].ToString());
                note.setLastModified(DateTime.Parse(reader[DatabaseConstants.COLUMN_LASTMODIFIED].ToString()));
                note.setDateCreated(DateTime.Parse(reader[DatabaseConstants.COLUMN_DATECREATED].ToString()));
                note.setTitle(reader[DatabaseConstants.COLUMN_TITLE].ToString());
                return note;
            }
            //Note was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND("404"));
        }

        /**
         * Inserting the note into the SQL Database
         * 
         * @note : the note that will get inserted
         * 
         * return ture if and only if the note was saved successfully
         **/
        public override bool save(Note note) {
            //Logging
            Logging.paramenterLogging(nameof(save) , false
                , new Pair(nameof(note) , note.ToString()));
            //Inserting User into the Database
            try {
                return driver.executeQuery(parser.getInsert(note)) != -1;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }

        /**
         * updatting the note in the SQL Database
         * 
         * @note : the note that will get updated
         * @columns : the column in the database that will be updated
         * 
         * return true if and only if the updating operation was successfull
         **/
        public override bool update(Note note , params String[] columns) {
            //Logging
            Logging.paramenterLogging(nameof(update) , false , new Pair(nameof(note) , note.ToString()));
            //Updating
            try {
                return driver.executeQuery(parser.getUpdate(tableName ,
                    DatabaseConstants.COLUMN_ID , note.getId() , note , columns)) != -1;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(update) , true , new Pair(nameof(note) , note.ToString()));
            //Note was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(note.ToString()));
        }

        /**
         * Getting Doucment Id 
         * 
         * @id : the note id the get the docuemnt from
         * 
         * return id of the document if it was found and throw an exception otherwise
         **/
        public String findNoteDocument(String id) {
            //Logging
            Logging.paramenterLogging(nameof(findByTitle) , false , new Pair(nameof(id) , id));
            //Finding the note
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName 
                                        , DatabaseConstants.COLUMN_DOCUMENTID , DatabaseConstants.COLUMN_DOCUMENTID , id));
                String documentId = reader[DatabaseConstants.COLUMN_DOCUMENTID].ToString();
                Logging.logInfo(false , documentId);
                reader.Close();
                return documentId;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findByTitle) , true , new Pair(nameof(id) , id));
            //Note was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }
    }
}
