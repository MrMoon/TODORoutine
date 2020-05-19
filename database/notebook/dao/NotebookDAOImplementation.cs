using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TODORoutine.database.general;
using TODORoutine.database.general.dao;
using TODORoutine.database.parsers;
using TODORoutine.Database;
using TODORoutine.exceptions;
using TODORoutine.models;
using TODORoutine.shared.csv;
using TODORoutine.Shared;

namespace TODORoutine.database.notebook.dao {

    /**
     * Notebook Data Access Layer Implemenation
     * Handles Data Operations for the Notebook
     **/
    class NotebookDAOImplementation : DatabaseDAOImplementation<Notebook> , NotebookDAO {

        private readonly String idColumn = DatabaseConstants.COLUMN_NOTEBOOKID;
        private readonly String tableName = DatabaseConstants.TABLE_NOTEBOOK;
        private static NotebookDAO notebookDAO = null;
        private NotebookParser parser = null;
        private DatabaseDriver driver = null;

        private NotebookDAOImplementation() {
            driver = DatabaseDriverImplementation.getInstance();
            parser = NotebookParserImplementation.getInstance();
        }

        public static NotebookDAO getInstance() {
            if (notebookDAO == null) notebookDAO = new NotebookDAOImplementation();
            return notebookDAO;
        }

        /**
         * Deleting the notebook base on the id
         * 
         * @id : the id of the notebook
         * 
         * return true if and only if the delete operation was successfull
         **/
        public override bool delete(String id) {
            //Logging
            Logging.paramenterLogging(nameof(delete) , false , new Pair(nameof(id) , id));
            //Deleting the Notebook from database
            try {
                return driver.executeQuery(parser.getDelete(tableName , idColumn , id)) != -1;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(delete) , true , new Pair(nameof(id) , id));
            //Notebook was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        /**
         * Finding all Notebooks by order of date created with a range
         * 
         * @lastNoteId : the last notebook id that was read from the prevoius call
         * 
         * return a list of notebooks ids
         **/
        public List<String> findAllByOrderOfDateCreated(String lastNoteId = "") {
            //Logging
            Logging.paramenterLogging(nameof(findAllByOrderOfDateCreated) , false , new Pair(nameof(lastNoteId) , lastNoteId));
            return findAll(parser , tableName , DatabaseConstants.COLUMN_DATECREATED , idColumn , lastNoteId);
        }

        /**
         * Finding all Notebooks by order of last modified with a range
         * 
         * @lastNoteId : the last notebook id that was read from the prevoius call
         * 
         * return a list of notebooks ids
         **/
        public List<String> findAllByOrderOfLastModified(String lastNoteId = "") {
            //Logging
            Logging.paramenterLogging(nameof(findAllByOrderOfLastModified) , false , new Pair(nameof(lastNoteId) , lastNoteId));
            return findAll(parser , tableName , DatabaseConstants.COLUMN_LASTMODIFIED , idColumn , lastNoteId);
        }

        /**
         * Finding all the notebooks for an author
         * 
         * @author : the author of the notebook name
         * 
         * return a list of ids for the notebooks
         **/
        public List<String> findByAuthorName(String author) {
            //Logging
            Logging.paramenterLogging(nameof(findByAuthorName) , false , new Pair(nameof(author) , author));
            //Finding the notebook
            List<String> notebooksIds = new List<String>();
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName
                        , DatabaseConstants.COLUMN_AUTHOR , idColumn , author));
                while (reader.Read()) notebooksIds.Add(reader[idColumn].ToString());
                reader.Close();
                return notebooksIds;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findByAuthorName) , true , new Pair(nameof(author) , author));
            //Notebook was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(author));
        }

        /**
         * Finding he notebook based on it's id
         * 
         * @id : the notebook id
         * 
         * return notebook if it was found and throw an Exception otherwise
         **/
        public override Notebook findById(String id) {
            //Logging
            Logging.paramenterLogging(nameof(findById) , false , new Pair(nameof(id) , id));
            //Finding the notebook
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName 
                                        , idColumn , DatabaseConstants.ALL , id));
                Notebook notebook = get(reader);
                reader.Close();
                return notebook;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findById) , true , new Pair(nameof(id) , id));
            //Notebook was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        /**
         * Finding he notebook based on it's title
         * 
         * @id : the notebook title
         * 
         * return notebook if it was found and null otherwise
         **/
        public Notebook findByTitle(String title) {
            //Logging
            Logging.paramenterLogging(nameof(findByTitle) , false , new Pair(nameof(title) , title));
            //Finding the notebook
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName 
                                        , DatabaseConstants.COLUMN_TITLE , DatabaseConstants.ALL , title));
                Notebook notebook = get(reader);
                reader.Close();
                return notebook;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findByTitle) , true , new Pair(nameof(title) , title));
            //Notebook was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(title));
        }

        /**
         * Getting the notebook from the SQLiteDataReader
         * 
         * @reader : the SQLiteDataReader
         * 
         * return a notebook from the reader
         **/
        public override Notebook get(SQLiteDataReader reader) {
            if(reader.Read()) {
                Notebook notebook = null;
                notebook.setAuthor(reader[DatabaseConstants.COLUMN_AUTHOR].ToString());
                notebook.setDateCreated(reader[DatabaseConstants.COLUMN_DATECREATED].ToString());
                notebook.setId(reader[idColumn].ToString());
                notebook.setLastModified(reader[DatabaseConstants.COLUMN_LASTMODIFIED].ToString());
                notebook.setNotes(CSVParser.CSV2List(reader[DatabaseConstants.COLUMN_NOTESID].ToString()));
                notebook.setTitle(reader[DatabaseConstants.COLUMN_TITLE].ToString());
                return notebook;
            }

            throw new DatabaseException(DatabaseConstants.NOT_FOUND("404"));
        }

        /**
         * Saving the Notebook to the database
         * 
         * @notebook : the notebook to save
         * 
         * return true if and only if the notebook was save successfully and false otherwise
         **/
        public override bool save(Notebook notebook) {
            //Logging
            Logging.paramenterLogging(nameof(save) , false , new Pair(nameof(notebook) , notebook.toString()));
            //Saving the notebook
            try {
                return driver.executeQuery(parser.getInsert(notebook)) != -1;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }

        /**
         * Updating the Notebook in the database
         * 
         * @notebook : the notebook to update
         * @columns : the columns to update
         * 
         * return true if and only if the update was successfull and false otherwise
         **/
        public override bool update(Notebook notebook , params String[] columns) {
            //Logging
            Logging.paramenterLogging(nameof(update) , false , new Pair(nameof(columns) , columns.ToString()) , new Pair(nameof(notebook) , notebook.toString()));
            //Updating the notebook
            try {
                return driver.executeQuery(parser.getUpdate(tableName 
                                        , idColumn , notebook.getId() , notebook , columns)) != -1;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(update) , true , new Pair(nameof(columns) , columns.ToString()) , new Pair(nameof(notebook) , notebook.toString()));
            //Notebook was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(notebook.toString()));
        }

        /**
         * Finding Notes Id for the notebook
         * 
         * @id : the id for the notebook
         * 
         * return a list of notes id
         **/
        public List<String> findNotes(String id) {
            //Logging
            Logging.paramenterLogging(nameof(findNotes) , false , new Pair(nameof(id) , id));
            //Finding
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName 
                                            , idColumn , DatabaseConstants.COLUMN_NOTESID , id));
                if(reader.Read()) {
                    List<String> notesIds = CSVParser.CSV2List(reader[DatabaseConstants.COLUMN_NOTESID].ToString());
                    reader.Close();
                    return notesIds;
                }
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findNotes) , true , new Pair(nameof(id) , id));
            //Notebook was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }
    }
}
