using System;
using System.Text;

namespace TODORoutine.models {
    public class Note {

        private String id , title , author , documentId;
        private DateTime dateCreated, lastModified;

        public Note() {
            dateCreated = DateTime.Now ;
        }

        public String getId() { return id; }
        public String getTitle() { return title; }
        public String getAuthor() { return author; }
        public DateTime getDateCreated() { return dateCreated; }
        public DateTime getLastModified() { return lastModified; }
        public String getDocumentId() { return documentId; }
        public void setId(String id) { this.id = id; }
        public void setTitle(String title) { this.title = title; }
        public void setAuthor(String author) { this.author = author; }
        public void setLastModified(DateTime lastModified) { this.lastModified = lastModified; }
        public void setDateCreated(DateTime dateCreated) { this.dateCreated = dateCreated; }
        public void setDocumentId(String documentId) { this.documentId = documentId; }

        public override String ToString() {
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
            sb.Append(lastModified);
            sb.Append(" , Document ID : ");
            sb.Append(documentId);
            sb.Append(" }\n");
            return sb.ToString();
        }
    }
}
