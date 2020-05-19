using System;
using TODORoutine.Database.user.DTO;
using TODORoutine.Shared;

namespace TODORoutine.authentication {
    class Login {
    
        private UserDTO dto;

        public Login(UserDTO userDTO) { this.dto = userDTO; }

        /**
         * Login in for the username
         * 
         * @username : the username of the user to log in
         * 
         * return true if the login was successfull and false otherwise
         **/
        public bool authenticate(String id) {
            //Logging
            Logging.paramenterLogging(nameof(authenticate) , false , new Pair(nameof(id) , id));
            //Checking
            return dto.isAuthenticated(id) ;
        }
    }
}
