using System;
using TODORoutine.Shared;

namespace TODORoutine.database.authentication.dto {

    /**
     * Main Authentication Transfer Layer Implenetation
     * Handles Authentication object transfermation between other classes and the data layer
     **/
    class AuthenticationDTOImplementation : AuthenticationDTO {

        private static AuthenticationDTO authDTO = null;
        private readonly AuthenticationDAO authDAO = null;

        private AuthenticationDTOImplementation() {
            Logging.singlton(nameof(AuthenticationDTO));
            authDAO = AuthenticationDAOImplementation.getInstance();
        }

        public static AuthenticationDTO getInstance() {
            if (authDTO == null) authDTO = new AuthenticationDTOImplementation();
            return authDTO;
        }

        /**
         * Authenticating user from the data
         * 
         * @auth : user auth (username and password)
         * @islogin : a flag to indicate the operation (login or register)
         * 
         * return true if and only if the authentication was done successfully and false other wise
         **/
        public bool authenticate(Authentication auth , bool isLogin = false) {
            try {
                if (isLogin) return authDAO.login(auth);
                return authDAO.register(auth);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }

        /**
         * Deletng the Authentication from the data
         * 
         * @auth : the authentication to delete
         * 
         * return true if and only if the the delete operation was done successfully
         **/
        public bool delete(String username) {
            try {
                return authDAO.delete(username);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }
    }
}
