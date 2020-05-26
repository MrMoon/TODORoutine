using System;
using System.Collections.Generic;
using TODORoutine.database.document.dto;
using TODORoutine.database.general.dao;
using TODORoutine.database.note.dao;
using TODORoutine.database.parsers;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.note.dto {

    /**
     * Main Implementation for the Note Transfor Layer
     **/
    class NoteDTOImplementation : NoteDTO {

        private readonly NoteDAO noteDAO = null;
        private static NoteDTO noteDTO = null;

        private NoteDTOImplementation() => noteDAO = NoteDAOImplentation.getInsence();

        public static NoteDTO getInstance() {
            if (noteDTO == null) noteDTO = new NoteDTOImplementation();
            return noteDTO;
        }

        /**
         * Delete Note based on the id
         * 
         * @id : the id of the note that will be deleted
         * 
         * return true if and only if the delete operation was successfull
         **/
        public bool delete(String id) {
            try {
                return noteDAO.delete(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Getting the note based on the id
         * 
         * @id : the id of the note that will be searched for
         * 
         * return a note if it was found and null otherwise
         **/
        public Note getById(String id) {
            try {
                return noteDAO.findById(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }

        /**
         * Saving a note in the databse
         * 
         * @note : the note that will be saved
         * 
         * return true if and only if the note was successfu;ly saved
         **/
        public bool save(Note note) {
            try {
                bool flag = noteDAO.save(note);
                if (flag) {
                    note.setId(DatabaseDAOImplementation<Note>.getLastId(DatabaseConstants.TABLE_NOTE));
                    return true;
                }
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Updating the note in the database
         * 
         * @note : the note that will get updated
         * @columns : the database columns that will be updated
         * 
         * return true if and only if the update operation was done successfully
         **/
        public bool update(Note note , params String[] columns) {
            try {
                return noteDAO.update(note , columns);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Getting the note by it's title
         * 
         * @tilte : the title to search for
         * 
         * return the note if it was found and null otherwise
         **/
        public Note getByTitle(String title) {
            try {
                return noteDAO.findByTitle(title);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }

        /**
         * Getting all the notes for the author
         * 
         * @author : the author of the note that will be searched for
         * 
         * return a list of notes for the author if it was found and an empty list otherwise
         **/
        public List<Note> getByAuthorName(String author) {
            try {
                List<Note> notes = new List<Note>();
                noteDAO.findByAuthorName(author).ForEach(id => notes.Add(noteDAO.findById(id)));
                return notes;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Note>();
        }

        /**
         * Getting all the notes by order of it created date
         * 
         * @lastNoteId : the last id the was read from the previous call
         * 
         * return a list of notes by order of date created it it was found and an empty list otherwise
         **/
        public List<Note> getAllByOrderOfDateCreated(String lastNoteId = "1") {
            try {
                List<String> ids = noteDAO.findAllByOrderOfDateCreated(lastNoteId);
                List<Note> notes = new List<Note>();
                foreach (String id in ids) notes.Add(getById(id));
                return notes;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Note>();
        }

        /**
         * Getting all the notes by order of it last modified date
         * 
         * @lastNoteId : the last id the was read from the previous call
         * 
         * return a list of notes by order of date created if it was found and an empty list otherwise
         **/
        public List<Note> getAllByOrderOfLastModified(String lastNoteId = "") {
            try {
                List<Note> notes = new List<Note>();
                noteDAO.findAllByOrderOfLastModified(lastNoteId).ForEach(id => notes.Add(getById(id)));
                return notes;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Note>();
        }

        /**
         * Getting the Note Document
         * 
         * @id : note's id
         * 
         * return a docuemnt if it was found and null otherwise
         **/
        public Document getNoteDocument(String noteId) {       
            try {
                return DocumentDTOImplementation.getInstance().getById(noteDAO.findNoteDocument(noteId));
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }
    }
}
