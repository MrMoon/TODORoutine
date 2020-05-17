using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.models;

namespace TODORoutine.database.note.dao {

    /**
     * Main Note Data Access Implementation that handle database operations
     **/

    public class NoteDAOImlementation : NoteDAO {
        public bool delete(Note note) {
            throw new NotImplementedException();
        }

        public List<Note> findAllByOrderOfDateCreated() {
            throw new NotImplementedException();
        }

        public List<Note> findAllByOrderOfLastModified() {
            throw new NotImplementedException();
        }

        public List<Note> findByAuthorName(string author) {
            throw new NotImplementedException();
        }

        public Note findById(string id) {
            throw new NotImplementedException();
        }

        public Note findByTitle(string title) {
            throw new NotImplementedException();
        }

        public bool save(Note note) {
            throw new NotImplementedException();
        }

        public bool update(Note note , params string[] columns) {
            throw new NotImplementedException();
        }
    }
}
