using System;
using TODORoutine.Shared;

namespace TODORoutine.database.authentication.dto {

    /**
     * Main Authentication Transfer Layer Implenetation
     **/
    class AuthenticationDTOImplentation : AuthenticationDTO {

        private static AuthenticationDTO authDTO = null;
        private readonly AuthenticationDAO authDAO = null;

        private AuthenticationDTOImplentation() => authDAO = AuthenticationDAOImplentation.getInstance();

        public static AuthenticationDTO getInstance() {
            if (authDTO == null) authDTO = new AuthenticationDTOImplentation();
            return authDTO;
        }

        /**
         * Authenticating user 
         * 
         * @auth : user auth (username and password)
         * @islogin : a flag to indicate the operation
         * 
         * return true if and only if the operation was done successfully and false other wise
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

        public bool delete(Authentication auth) {
            try {
                return authDAO.delete(auth);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }
    }
}
