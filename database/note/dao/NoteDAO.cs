using System;
using System.Collections.Generic;
using TODORoutine.database.general.dao;
using TODORoutine.models;

namespace TODORoutine.database.note.dao {
    /**
     * Main Data Access Layer for all the notes comunicatation with the database
     * Handles main notes and database operations
     **/
    interface NoteDAO : DatabaseDAO<Note> {
        Note findByTitle(String title);
        List<String> findByAuthorName(String author);
        String findNoteDocument(String id);
        List<String> findAllByOrderOfDateCreated(String lastNoteId = "1");
        List<String> findAllByOrderOfLastModified(String lastNoteId = "1");
    }
}
