using System;
using System.Collections.Generic;
using TODORoutine.database.note.dao;
using TODORoutine.database.notebook.dao;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.notebook.dto {

    /**
     * Main Implementation Notebook Tranfor Layer 
     * Handles tranfor between objects
     **/
    class NotebookDTOImplementation : NotebookDTO {

        private static NotebookDTO notebookDTO = null;
        private NotebookDAO notebookDAO = null;

        private NotebookDTOImplementation() {
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
                notebookDAO.delete(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
            return true;
        }

        /**
         * Getting all Notebooks in a range ordered by Date of creation
         * 
         * @lastNoteId : the last notebook id that was done in the previos call
         * 
         * return a list of notebooks
         **/
        public List<Notebook> getAllByOrderOfDateCreated(String lastNoteId = "") {
            List<Notebook> notebooks = new List<Notebook>();
            try {
                notebookDAO.findAllByOrderOfDateCreated(lastNoteId).ForEach(id => notebooks.Add(notebookDAO.findById(id)));
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return notebooks;
        }

        /**
         * Getting all Notebooks in a range ordered by Last Modified
         * 
         * @lastNoteId : the last notebook id that was done in the previos call
         * 
         * return a list of notebooks
         **/
        public List<Notebook> getAllByOrderOfLastModified(String lastNoteId = "") {
            List<Notebook> notebooks = new List<Notebook>();
            try {
                notebookDAO.findAllByOrderOfLastModified(lastNoteId).ForEach(id => notebooks.Add(notebookDAO.findById(id)));
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return notebooks;
        }

        /**
         * Getting all Notebooks in a range by the author name
         * 
         * @author : the name of the author for the notebook
         * 
         * return a list of notebooks
         **/
        public List<Notebook> getByAuthorName(String author) {
            List<Notebook> notebooks = new List<Notebook>();
            try {
                notebookDAO.findByAuthorName(author).ForEach(id => notebooks.Add(notebookDAO.findById(id)));
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return notebooks;
        }

        /**
         * Getting the Notebooks by it's title
         * 
         * @title : the title of the notebook
         * 
         * return a of notebook if it was found and null otherwise
         **/
        public Notebook getByTitle(String title) {
            Notebook notebook = null;
            try {
                notebook = notebookDAO.findByTitle(title);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return notebook;
        }

        /**
         * Getting the Notes in the Notebook 
         * 
         * @id : the id of the notebook
         * 
         * return a list of notes
         **/
        public List<Note> getNotes(String id) {
            List<Note> notes = new List<Note>();
            try {
                notebookDAO.findNotes(id).ForEach(ID => notes.Add(NoteDAOImlementation.getInsence().findById(ID)));
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return notes;
        }

        /**
         * Getting the Notebooks by it's id
         * 
         * @id : the id of the notebook
         * 
         * return a of notebook if it was found and null otherwise
         **/
        public Notebook getById(String id) {
            Notebook notebook = null;
            try {
                notebook = notebookDAO.findById(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return notebook;
        }

        /**
         * Saving the notebook to the database
         * 
         * @t : the notebook object to save
         * 
         * return true if and only if the save operation was done successfully and false otherwise
         **/
        public bool save(Notebook t) {
            try {
                notebookDAO.save(t);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
            return true;
        }

        /**
         * Updating the notebook in the database
         * 
         * @t : the notebook object to save
         * @columns : the columns to update
         * 
         * return true if and only if the update operation was done successfully and false otherwise
         **/
        public bool update(Notebook t , params String[] columns) {
            try {
                notebookDAO.update(t);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
            return true;
        }
    }
}
