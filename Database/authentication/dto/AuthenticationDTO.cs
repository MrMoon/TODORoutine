using System;
using TODORoutine.models;

namespace TODORoutine.database.authentication.dto {
    /**
     * Main Authentication Transfer Layer
     * Handles basic authentication operations
     **/
    interface AuthenticationDTO {
        bool authenticate(Authentication auth , bool isLogin = false);
        bool delete(String username);
    }
}
