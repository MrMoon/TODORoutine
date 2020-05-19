using System;
using System.Collections.Generic;
using TODORoutine.database.document.dto;
using TODORoutine.database.note.dao;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.note.dto {

    /**
     * Main Implementation for the Note Transfor Layer
     **/
    class NoteDTOImplementation : NoteDTO {

        private NoteDAO noteDAO = null;
        private static NoteDTO noteDTO = null;

        private NoteDTOImplementation() {
            noteDAO = NoteDAOImlementation.getInsence();
        }

        public NoteDTO getInstance() {
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
                noteDAO.delete(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
            return true;
        }

        /**
         * Getting the note based on the id
         * 
         * @id : the id of the note that will be searched for
         * 
         * return a note if it was found and null otherwise
         **/
        public Note getById(String id) {
            Note note = null;
            try {
                note = noteDAO.findById(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return note;
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
                noteDAO.save(note);
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
            return true;
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
                noteDAO.update(note , columns);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
            return true;
        }

        /**
         * Getting the note by it's title
         * 
         * @tilte : the title to search for
         * 
         * return the note if it was found and null otherwise
         **/
        public Note getByTitle(String title) {
            Note note = null;
            try {
                note = noteDAO.findByTitle(title);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return note;
        }

        /**
         * Getting all the notes for the author
         * 
         * @author : the author of the note that will be searched for
         * 
         * return a list of notes for the author if it was found and an empty list otherwise
         **/
        public List<Note> getByAuthorName(String author) {
            List<Note> notes = new List<Note>();
            try {
                noteDAO.findByAuthorName(author).ForEach(id => notes.Add(noteDAO.findById(id)));
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return notes;
        }

        /**
         * Getting all the notes by order of it created date
         * 
         * @lastNoteId : the last id the was read from the previous call
         * 
         * return a list of notes by order of date created it it was found and an empty list otherwise
         **/
        public List<Note> getAllByOrderOfDateCreated(String lastNoteId = "") {
            List<Note> notes = new List<Note>();
            try {
                noteDAO.findAllByOrderOfDateCreated(lastNoteId).ForEach(id => notes.Add(noteDAO.findById(id)));
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return notes;
        }

        /**
         * Getting all the notes by order of it last modified date
         * 
         * @lastNoteId : the last id the was read from the previous call
         * 
         * return a list of notes by order of date created if it was found and an empty list otherwise
         **/
        public List<Note> getAllByOrderOfLastModified(String lastNoteId = "") {
            List<Note> notes = new List<Note>();
            try {
                noteDAO.findAllByOrderOfLastModified(lastNoteId).ForEach(id => notes.Add(noteDAO.findById(id)));
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return notes;
        }

        /**
         * Getting all the notes by order of it created date
         * 
         * return a list of notes by order of date created
         **/
        public List<Note> getAllNotes() => getAllByOrderOfLastModified();

        /**
         * Getting the Note Document
         * 
         * @id : note's id
         * 
         * return a docuemnt if it was found and null otherwise
         **/
        public Document getNoteDocument(String id) {
            Document document = null;          
            try {
                document = DocumentDTOImplementation.getInstance().getById(noteDAO.findNoteDocument(id));
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return null;
            }
            return document;
        }
    }
}
