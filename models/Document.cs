using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TODORoutine.models {
    public class Document {

        private String id;
        private String ownerId;
        private Byte[] document;

        public Document() { }

        public String getId() { return id; }
        public Byte[] getDocument() { return document; }
        public String getOwner() { return ownerId; }
        public void setId(String id) { this.id = id; }
        public void setOwner(String owner) { this.ownerId = owner; }
        public void setDocument(Byte[] data) { this.document = data; }

        public String toString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ID : ");
            sb.Append(id);
            sb.Append(" , Owner ");
            sb.Append(ownerId);
            sb.Append(" } ");
            return sb.ToString();
        }
    }
}
