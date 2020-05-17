using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.user.exceptions;
using TODORoutine.Database.Shared;
using TODORoutine.Database.user.DTO;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.authentication {
    public class Login {
    
        private UserDTO dto;

        public Login(UserDTO userDTO) { this.dto = userDTO; }

        /**
         * Login in for the username
         * 
         * @username : the username of the user to log in
         * 
         * return 1 if the login was successfull , 0 if not and -1 if a database error occured
         **/
        public int authenticate(String username) {
            //Logging
            Logging.paramenterLogging(nameof(authenticate) , false , new Pair(nameof(username) , username));
            //Checking
            return dto.isAuthenticated(username);
        }
    }
}
