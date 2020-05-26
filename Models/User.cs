﻿using System;
using System.Text;

namespace TODORoutine.Models {
    /**
     * Main User Genral Class that has fields related to the user
     **/
    public class User {

        private String username, notesId, fullName , userId;
        private int isAuthenticated;

        public User() { }

        public String getId() => userId; 

        public String getUsername() => username; 

        public String getNotesId() => notesId; 

        public String getFullName() => fullName; 

        public int getIsAuthenticated() => isAuthenticated; 
        public void setUsername(String username) => this.username = username; 
        public void setNotesId(String notesId) => this.notesId = notesId; 
        public void setFullName(String fullName) => this.fullName = fullName; 
        public void setId(String userId) => this.userId = userId; 
        public void setIsAuthenticated(int isAuthenticated) => this.isAuthenticated = isAuthenticated; 

        public override String ToString() {
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
