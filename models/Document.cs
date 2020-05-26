using System;
using System.Text;

namespace TODORoutine.models {
    public class Document {

        private String id;
        private String ownerId;
        private Byte[] document;

        public Document() { }

        public String getId() => id; 
        public Byte[] getDocument() => document; 
        public String getOwner() => ownerId; 
        public String getDocumentContent() => System.Text.Encoding.Default.GetString(document);
        public void setId(String id) => this.id = id; 
        public void setOwner(String owner) => this.ownerId = owner; 
        public void setDocument(Byte[] data) => this.document = data; 
        public void setDocument(String document) => this.document = Encoding.Default.GetBytes(document); 

        public override String ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ID : ");
            sb.Append(id);
            sb.Append(" , Owner : ");
            sb.Append(ownerId);
            sb.Append(" , Document : ");
            sb.Append(getDocumentContent());
            sb.Append(" } ");
            return sb.ToString();
        }
    }
}
