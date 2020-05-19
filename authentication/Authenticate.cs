using TODORoutine.Database.user.DTO;
using TODORoutine.Models;

namespace TODORoutine.authentication {
    /**
     * Main class that handle authentication operations (Login and Register)
     **/
    class Authenticate {

        private Login login;
        private Register register;

        public Authenticate(UserDTO userDTO) {
            login = new Login(userDTO);
            register = new Register(userDTO);
        }

        /**
         * Authentication Method 
         * 
         * @user : the user to authenticate
         * @isLogin : flag the authentication operation (Register or Login)
         * 
         * return 1 if the authentication was successfull , 0 if not and -1 if a database error occured
         **/
        public bool authentication(User user , bool isLogin = false) {
            if (isLogin) return login.authenticate(user.getId());
            return register.register(user);
        }

    }
}
