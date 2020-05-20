using System;
using System.Collections.Generic;
using TODORoutine.database.general.dto;
using TODORoutine.models;

namespace TODORoutine.database.notebook.dto {

    /**
     * Notebook Tranfor Layer 
     * Handles tranfor between objects
     **/
    interface NotebookDTO : DatabaseDTO<Notebook> {
        Notebook getByTitle(String title);
        List<Note> getNotes(String id);
        List<Notebook> getByAuthorName(String author);
        List<Notebook> getAllByOrderOfDateCreated(String lastNoteId = "1");
        List<Notebook> getAllByOrderOfLastModified(String lastNoteId = "1");

    }
}
