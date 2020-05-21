using System;
using TODORoutine.Shared;

namespace TODORoutine.database.authentication.dto {
    class AuthenticationDTOImplentation : AuthenticationDTO {

        private static AuthenticationDTO authDTO = null;
        private AuthenticationDAO authDAO = null;

        private AuthenticationDTOImplentation() {
            authDAO = AuthenticationDAOImplentation.getInstance();
        }

        public static AuthenticationDTO getInstance() {
            if (authDTO == null) authDTO = new AuthenticationDTOImplentation();
            return authDTO;
        }

        public bool authenticate(Authentication auth , bool isLogin = false) {
            try {
                if (isLogin) return authDAO.login(auth);
                return authDAO.register(auth);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }
    }
}
