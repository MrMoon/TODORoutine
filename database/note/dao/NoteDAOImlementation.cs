using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TODORoutine.database.general;
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

    public class NoteDAOImlementation : NoteDAO {

        private readonly String tableName = DatabaseConstants.TABLE_NOTE;
        private static NoteDAO noteDAO = null;
        private DatabaseDriver driver = null;
        private NoteParser parser = null;

        private NoteDAOImlementation() {
            parser = NoteParserImplementation.getInstance();
            driver = DatabaseDriverImplementation.getInstance();
        }

        public static NoteDAO getInsence() {
            if (noteDAO == null) noteDAO = new NoteDAOImlementation();
            return noteDAO;
        }

        /**
        * deleting the note from the Database
        * 
        * @note : the note that will get deleted
        * 
        * return true if and only if the delete operation was done successfully
        **/
        public bool delete(Note note) {
            //Logging
            Logging.paramenterLogging(nameof(delete) , false
                , new Pair(nameof(note) , note.toString()));
            //Deleting note from database
            try {
                driver.executeQuery(parser.getDelete(tableName , DatabaseConstants.COLUMN_USERID , note.getId()));
            } catch (SQLiteException e) {
                Logging.logInfo(true , e.Data.ToString());
                return false;
            }
            return true;
        }

        public List<string> findAllByOrderOfDateCreated() {
            throw new NotImplementedException();
        }

        public List<string> findAllByOrderOfLastModified() {
            throw new NotImplementedException();
        }

        public List<string> findByAuthorName(string author) {
            throw new NotImplementedException();
        }

        /**
         * Getting the note from the database based on the id
         * 
         * @id : the note id to search for
         * 
         * return note if found and throw an Exception otherwise
         **/
        public Note findById(string id) {
            //Logging
            Logging.paramenterLogging(nameof(findById) , false , new Pair(nameof(id) , id));
            //Getting the note
            SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName ,
                                            DatabaseConstants.COLUMN_USERID , DatabaseConstants.ALL , id));
            //Reading the the Record from the database
            while (reader.Read()) {
                Note note = getT(reader);
                Logging.logInfo(false , nameof(findById) , DatabaseConstants.FOUND(id) , note.toString());
                reader.Close();
                return note;
            }
            reader.Close();
            //Logging
            Logging.paramenterLogging(nameof(findById) , true , new Pair(nameof(id) , id));
            //Note not found in the database
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        public Note findByTitle(string title) {
            throw new NotImplementedException();
        }

        public Note getT(SQLiteDataReader dataReader) {
            throw new NotImplementedException();
        }

        /**
         * Inserting the note into the SQL Database
         * 
         * @note : the note that will get inserted
         * 
         * return ture if and only if the note was saved successfully
         **/
        public bool save(Note note) {
            //Logging
            Logging.paramenterLogging(nameof(save) , false
                , new Pair(nameof(note) , note.toString()));
            //Inserting User into the Database
            try {
                driver.executeQuery(parser.getInsert(note));
            } catch (SQLiteException e) {
                Logging.logInfo(true , e.Data.ToString());
                return false;
            }
            return true;
        }

        /**
         * updatting the note in the SQL Database
         * 
         * @note : the note that will get updated
         * @columns : the column in the database that will be updated
         * 
         * return true if and only if the updating operation was successfull
         **/
        public bool update(Note note , params string[] columns) {
            //Logging
            Logging.paramenterLogging(nameof(update) , false , new Pair(nameof(note) , note.toString()));
            //Updating
            try {
                driver.executeQuery(parser.getUpdate(tableName , DatabaseConstants.COLUMN_USERID , note.getId() , note , columns));
            } catch (SQLiteException e) {
                Logging.logInfo(true , e.Data.ToString());
                return false;
            }
            return true;
        }
    }
}
