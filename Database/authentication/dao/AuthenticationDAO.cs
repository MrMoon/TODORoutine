namespace TODORoutine.database.authentication {

    /**
     * Main Authentication Data  Layer
     * Handles basic authentication data operations
     **/
    interface AuthenticationDAO {

        bool login(Authentication auth);
        bool register(Authentication auth);
    }
}
