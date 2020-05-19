using TODORoutine.Database.user.DTO;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.authentication {
    class Register {

        private UserDTO dto;

        public Register(UserDTO userDTO) {
            this.dto = userDTO;
        }

        /**
         * Registering the user in the database
         * 
         * @user : the user to register and add to the database
         * 
         * return 1 if the registration was successfull , 0 if not , -1 if the user is already authenticated
         **/
        public bool register(User user) {
            //Logging
            Logging.paramenterLogging(nameof(register) , false , new Pair(nameof(user) , user.toString()));
            //Checking if the user already exist in the database (which means that he is already registered)
            if (dto.isAuthenticated(user.getId())) return false;
            //Registering the user
            return dto.save(user);
        }

    }
}
