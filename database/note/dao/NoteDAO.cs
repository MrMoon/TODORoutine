using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.models;

namespace TODORoutine.database.note.dao {
    /**
     * Main Data Access Layer for all the notes comunicatation with the database
     * Handles main notes and database operations
     **/
    public interface NoteDAO {
        Note findById(String id);
        Note findByTitle(String title);
        List<Note> findByAuthorName(String author);
        List<Note> findAllByOrderOfDateCreated();
        List<Note> findAllByOrderOfLastModified();
        bool save(Note note);
        bool update(Note note , params String[] columns);
        bool delete(Note note);
    }
}
