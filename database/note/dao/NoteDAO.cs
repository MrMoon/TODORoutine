using System;
using System.Collections.Generic;
using TODORoutine.database.general.dao;
using TODORoutine.models;

namespace TODORoutine.database.note.dao {
    /**
     * Main Data Access Layer for all the notes comunicatation with the database
     * Handles main notes and database operations
     **/
    public interface NoteDAO : DatabaseDAO<Note> {
        Note findByTitle(String title);
        List<String> findByAuthorName(String author);
        List<String> findAllByOrderOfDateCreated();
        List<String> findAllByOrderOfLastModified();

    }
}
