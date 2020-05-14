using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.Models {
    class User {

        private String userId, username, notesId, fullName;

        User() { }

        public String getId() { return userId; }

        public String getUsername() { return username; }

        public String getNotesId() { return notesId; }

        public String getFullName() { return fullName; }

        public void setId(String id) { this.userId = id; }

        public void setName(String name) { this.username = name; }

        public void setNotesId(String notesId) { this.notesId = notesId; }

        public void setFullName(String fullName) { this.fullName = fullName; }

        public String toString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ID : ");
            sb.Append(userId);
            sb.Append(" , Username : ");
            sb.Append(username);
            sb.Append(" , Full Name : ");
            sb.Append(fullName);
            sb.Append(" , Notes Id : ");
            sb.Append(notesId);
            sb.Append(" }\n");
            return sb.ToString();
        }

    }
}
