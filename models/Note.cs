using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TODORoutine.models {
    public class Note {

        private String id , title , author , dateCreated , lastModified;
        List<String> documents;

        public Note() {
            documents = new List<String>();
        }

        public String getId() { return id; }
        public String getTitle() { return title; }
        public String getAuthor() { return author; }
        public String getDateCreated() { return dateCreated; }
        public String getLastModified() { return lastModified; }
        public List<String> getDocuments() { return documents; }
        public void setId(String id) { this.id = id; }
        public void setTitle(String title) { this.title = title; }
        public void setAuthor(String author) { this.author = author; }
        public void setDateCreated(String dateCreated) { this.dateCreated = dateCreated; }
        public void setLastModified(String lastModified) { this.lastModified = lastModified; }
        public void setDocuments(List<String> documents) { this.documents = documents; }
        public void addDocument(String documentId) { this.documents.Add(documentId); }

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
            sb.Append(" \nDocument { IDs : ");
            foreach(String id in documents) {
                sb.Append(id);
                if (id != documents[documents.Count() - 1]) sb.Append(" , ");
            }
            sb.Append("}\n");
            sb.Append(" }\n");
            return sb.ToString();
        }
    }
}
