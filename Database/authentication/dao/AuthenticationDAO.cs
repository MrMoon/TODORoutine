using System;
using TODORoutine.models;

namespace TODORoutine.database.authentication.dao {

    /**
     * Main Authentication Data Layer
     * Handles basic authentication data operations
     **/
    interface AuthenticationDAO {
        bool login(Authentication auth);
        bool register(Authentication auth);
        bool delete(String username);
    }
}
