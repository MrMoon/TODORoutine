using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TODORoutine.models {
    public class Notebook {

        private String id, title, author, dateCreated, lastModified;
        private List<String> notes;

        public Notebook() {
            notes = new List<String>();
        }

        public String getId() { return id; }
        public String getTitle() { return title; }
        public String getAuthor() { return author; }
        public String getDateCreated() { return dateCreated; }
        public String getLastModified() { return lastModified; }
        public List<String> getNotes() { return notes; }
        public void setId(String id) { this.id = id; }
        public void setTitle(String title) { this.title = title; }
        public void setAuthor(String author) { this.author = author; }
        public void setDateCreated(String dateCreated) { this.dateCreated = dateCreated; }
        public void setLastModified(String lastModified) { this.lastModified = lastModified; }
        public void setNotes(List<String> notes) { this.notes = notes; }
        public void addNote(String noteId) { this.notes.Add(noteId); }
        public void removeNote(String noteId) { this.notes.Remove(noteId); }
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
            sb.Append(", Notes IDs : { ");
            foreach(String note in notes) {
                sb.Append(note);
                if (note != notes[notes.Count() - 1]) sb.Append(" , ");
            }
            sb.Append(" } ");
            sb.Append(" }\n");
            return sb.ToString();
        }
    }
}
