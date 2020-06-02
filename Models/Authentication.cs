using System;

namespace TODORoutine.models {

    /**
     * Authentication Model for the user 
     * To seperate user info from the password
     **/
    class Authentication {

        private String username, password;

        public Authentication() { }
        public Authentication(String username , String password) {
            this.username = username;
            this.password = password;
        }
        public String getUsername() => username; 
        public String getPassword() => password; 
        public void setUsername(String username) => this.username = username; 
        public void setPassword(String password) => this.password = password; 
        public override String ToString() =>  "{ Username : " + username + " , Password : " + password + " }";
    }
}
