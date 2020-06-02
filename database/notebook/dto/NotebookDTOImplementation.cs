using System;
using System.Collections.Generic;
using TODORoutine.database.general.dao;
using TODORoutine.database.general.shared;
using TODORoutine.database.note.dao;
using TODORoutine.database.notebook.dao;
using TODORoutine.general.logging;
using TODORoutine.models;

namespace TODORoutine.database.notebook.dto {

    /**
     * Main Implementation Notebook Tranfor Layer 
     * Handles tranfor between objects
     **/
    class NotebookDTOImplementation : NotebookDTO {

        private static NotebookDTO notebookDTO = null;
        private readonly NotebookDAO notebookDAO = null;

        private NotebookDTOImplementation() {
            Logging.singlton(nameof(NotebookDTO));
            notebookDAO = NotebookDAOImplementation.getInstance();
        }

        public static NotebookDTO getInstance() {
            if (notebookDTO == null) notebookDTO = new NotebookDTOImplementation();
            return notebookDTO;
        }

        /**
         * Deleting Notebook from database
         * 
         * @id : the id of the notebook
         * 
         * return true if the delete operation wsa successfull and false otherwise
         **/
        public bool delete(String id) {
            try {
                return notebookDAO.delete(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Getting all Notebooks in a range ordered by Date of creation
         * 
         * @lastNoteId : the last notebook id that was done in the previos call
         * 
         * return a list of notebooks
         **/
        public List<Notebook> getAllByOrderOfDateCreated(String lastNoteId = "1") {
            try {
                List<Notebook> notebooks = new List<Notebook>();
                notebookDAO.findAllByOrderOfDateCreated(lastNoteId).ForEach(id => notebooks.Add(notebookDAO.findById(id)));
                return notebooks;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Notebook>();
        }

        /**
         * Getting all Notebooks in a range ordered by Last Modified
         * 
         * @lastNoteId : the last notebook id that was done in the previos call
         * 
         * return a list of notebooks
         **/
        public List<Notebook> getAllByOrderOfLastModified(String lastNoteId = "1") {
            try {
                List<Notebook> notebooks = new List<Notebook>();
                notebookDAO.findAllByOrderOfLastModified(lastNoteId).ForEach(id => notebooks.Add(notebookDAO.findById(id)));
                return notebooks;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Notebook>();
        }

        /**
         * Getting all Notebooks in a range by the author name
         * 
         * @author : the name of the author for the notebook
         * 
         * return a list of notebooks
         **/
        public List<Notebook> getByAuthorName(String author) {
            try {
                List<Notebook> notebooks = new List<Notebook>();
                notebookDAO.findByAuthorName(author).ForEach(id => notebooks.Add(notebookDAO.findById(id)));
                return notebooks;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Notebook>();
        }

        /**
         * Getting the Notebooks by it's title
         * 
         * @title : the title of the notebook
         * 
         * return a of notebook if it was found and null otherwise
         **/
        public Notebook getByTitle(String title) {
            try {
                return notebookDAO.findByTitle(title);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }

        /**
         * Getting the Notes in the Notebook 
         * 
         * @id : the id of the notebook
         * 
         * return a list of notes
         **/
        public List<Note> getNotes(String id) {
            try {
                List<Note> notes = new List<Note>();
                notebookDAO.findNotes(id).ForEach(ID => notes.Add(NoteDAOImplementation.getInsence().findById(ID)));
                return notes;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Note>();
        }

        /**
         * Getting the Notebooks by it's id
         * 
         * @id : the id of the notebook
         * 
         * return a of notebook if it was found and null otherwise
         **/
        public Notebook getById(String id) {
            try {
                return notebookDAO.findById(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }

        /**
         * Saving the notebook to the database
         * 
         * @notebook : the notebook object to save
         * 
         * return true if and only if the save operation was done successfully and false otherwise
         **/
        public bool save(Notebook notebook) {
            try {
                bool flag = notebookDAO.save(notebook);
                if(flag) {
                    notebook.setId(DatabaseDAOImplementation<Notebook>.getLastId(DatabaseConstants.TABLE_NOTEBOOK));
                    return true;
                }
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Updating the notebook in the database
         * 
         * @notebook : the notebook object to save
         * @columns : the columns to update
         * 
         * return true if and only if the update operation was done successfully and false otherwise
         **/
        public bool update(Notebook notebook , params String[] columns) {
            try {
                return notebookDAO.update(notebook , columns);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Getting all notebooks within a range from lastNotebookId to lastNotebookId + x
         * 
         * @lastNotebookId : the last notebook id that was read from the last call
         * 
         * return a list of notebook if it was found and an empty list otherwise
         **/
        public List<Notebook> getAll(String lastNotebookId = "1") {
            try {
                List<Notebook> notebooks = new List<Notebook>();
                notebookDAO.findAll(lastNotebookId).ForEach((id) => notebooks.Add(getById(id)));
                return notebooks;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Notebook>();
        }
    }
}
