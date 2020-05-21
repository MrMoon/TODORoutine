using System;
using System.Collections.Generic;
using System.Text;

namespace TODORoutine.models {
    public class Notebook {

        private String id, title, author , lastModified;
        private readonly String dateCreated;
        private HashSet<String> notes;

        public Notebook() {
            notes = new HashSet<String>();
            dateCreated = DateTime.Now.ToString();
        }

        public String getId() { return id; }
        public String getTitle() { return title; }
        public String getAuthor() { return author; }
        public String getDateCreated() { return dateCreated; }
        public String getLastModified() { return lastModified; }
        public HashSet<String> getNotes() { return notes; }
        public void setId(String id) { this.id = id; }
        public void setTitle(String title) { this.title = title; }
        public void setAuthor(String author) { this.author = author; }
        public void setLastModified(String lastModified) { this.lastModified = lastModified; }
        public void setNotes(HashSet<String> notes) { this.notes = notes; }
        public void addNote(String noteId) { this.notes.Add(noteId); }
        public void removeNote(String noteId) { this.notes.Remove(noteId); }
        public override String ToString() {
            String prefix = "";
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
                sb.Append(prefix);
                prefix = ",";
                sb.Append(note);
            }
            sb.Append(" } ");
            sb.Append(" }\n");
            return sb.ToString();
        }
    }
}
