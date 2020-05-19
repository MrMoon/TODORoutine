using System;
using System.Text;

namespace TODORoutine.models {
    public class Note {

        private String id , title , author , dateCreated , lastModified , documentId;

        public Note() { }

        public String getId() { return id; }
        public String getTitle() { return title; }
        public String getAuthor() { return author; }
        public String getDateCreated() { return dateCreated; }
        public String getLastModified() { return lastModified; }
        public String getDocumentId() { return documentId; }
        public void setId(String id) { this.id = id; }
        public void setTitle(String title) { this.title = title; }
        public void setAuthor(String author) { this.author = author; }
        public void setDateCreated(String dateCreated) { this.dateCreated = dateCreated; }
        public void setLastModified(String lastModified) { this.lastModified = lastModified; }
        public void setDocumentId(String documentId) { this.documentId = documentId; }

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
            sb.Append(" , Document ID : ");
            sb.Append(documentId);
            sb.Append(" }\n");
            return sb.ToString();
        }
    }
}
