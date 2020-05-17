using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.models {
    class Notebook {

        private String id, title, author, dateCreated, lastModified;
        private List<Note> notes;

        public Notebook() {
            notes = new List<Note>();
        }

        public String getId() { return id; }
        public String getTitle() { return title; }
        public String getAuthor() { return author; }
        public String getDateCreated() { return dateCreated; }
        public String getLastModified() { return lastModified; }
        public List<Note> getNotes() { return notes; }
        public void setId(String id) { this.id = id; }
        public void setTitle(String title) { this.title = title; }
        public void setAuthor(String author) { this.author = author; }
        public void setDateCreated(String dateCreated) { this.dateCreated = dateCreated; }
        public void setLastModified(String lastModified) { this.lastModified = lastModified; }
        public void setNotes(List<Note> notes) { this.notes = notes; }
        public String toString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ID : ");
            sb.Append(id);
            sb.Append(" , Author : ");
            sb.Append(author);
            sb.Append(" , Title : ");
            sb.Append(title);
            sb.Append(" , Date Created : ");
            sb.Append(dateCreated);
            sb.Append(" , Last Modified : ");
            sb.Append(lastModified);
            sb.Append(", Notes : { ");
            foreach(Note note in notes) {
                sb.Append(note.toString());
                if (note != notes[notes.Count() - 1]) sb.Append(" , ");
            }
            sb.Append(" } ");
            sb.Append(" }\n");
            return sb.ToString();
        }
    }
}
