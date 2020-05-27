using System;
using System.Collections.Generic;
using TODORoutine.database.general.dto;
using TODORoutine.models;

namespace TODORoutine.database.note.dto {
    /**
     * Main Transfor Layer between the Data Access Layer and the Application Layer
     * Handles the transfor operations between the Application and the database
     **/
    interface NoteDTO : DatabaseDTO<Note> {

        Note getByTitle(String title);
        Document getNoteDocument(String noteId);
        List<Note> getByAuthorName(String author);
        List<Note> getAllByOrderOfDateCreated(String lastNoteId = "1");
        List<Note> getAllByOrderOfLastModified(String lastNoteId = "1");
        List<Note> getAll(String lastNoteId = "1");
    }
}
