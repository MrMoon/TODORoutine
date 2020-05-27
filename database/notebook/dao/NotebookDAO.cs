using System;
using System.Collections.Generic;
using TODORoutine.database.general.dao;
using TODORoutine.models;

namespace TODORoutine.database.notebook.dao {

    /**
     * Notebook Data Access Layer
     * Handles Main Data Operations for the Notebook
     **/
    interface NotebookDAO : DatabaseDAO<Notebook> {
        Notebook findByTitle(String title);
        List<String> findNotes(String id);
        List<String> findByAuthorName(String author);
        List<String> findAllByOrderOfDateCreated(String lastNoteId = "1");
        List<String> findAllByOrderOfLastModified(String lastNoteId = "1");
        List<String> findAll(String lastNotebookId = "1");
    }
}
