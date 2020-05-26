using System;
using System.Collections.Generic;
using System.Text;

namespace TODORoutine.models {
    public class Notebook {

        private String id, title, author;
        private DateTime dateCreated , lastModified;
        private HashSet<String> notes;

        public Notebook() {
            notes = new HashSet<String>();
            dateCreated = DateTime.Now;
        }

        public String getId() => id; 
        public String getTitle() => title; 
        public String getAuthor() => author; 
        public DateTime getDateCreated() => dateCreated; 
        public DateTime getLastModified() => lastModified; 
        public HashSet<String> getNotes() => notes; 
        public void setId(String id) => this.id = id; 
        public void setTitle(String title) => this.title = title; 
        public void setAuthor(String author) => this.author = author; 
        public void setLastModified(DateTime lastModified) => this.lastModified = lastModified; 
        public void setDateCreated(DateTime dateCreated) => this.dateCreated = dateCreated; 
        public void setNotes(HashSet<String> notes) => this.notes = notes; 
        public void addNote(String noteId) => this.notes.Add(noteId); 
        public void removeNote(String noteId) => this.notes.Remove(noteId); 
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
            sb.Append(dateCreated.ToString());
            sb.Append(" , Last Modified : ");
            sb.Append(lastModified.ToString());
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
