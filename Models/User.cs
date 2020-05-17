using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.Models {
    /**
     * Main User Genral Class that has fields related to the user
     **/
    public class User {

        private String username, notesId, fullName;
        private readonly String userId;
        private int isAuthenticated;

        public User() { }

        public User(String userId) { this.userId = userId; }

        public String getId() { return userId; }

        public String getUsername() { return username; }

        public String getNotesId() { return notesId; }

        public String getFullName() { return fullName; }

        public int getIsAuthenticated() { return isAuthenticated; }

        public void setUsername(String username) { this.username = username; }

        public void setNotesId(String notesId) { this.notesId = notesId; }

        public void setFullName(String fullName) { this.fullName = fullName; }

        public void setIsAuthenticated(int isAuthenticated) { this.isAuthenticated = isAuthenticated; }

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
            sb.Append(" , isAuthenticated : ");
            sb.Append(isAuthenticated);
            sb.Append(" }\n");
            return sb.ToString();
        }

    }
}
