namespace TODORoutine.database.authentication {
    interface AuthenticationDAO {

        bool login(Authentication auth);
        bool register(Authentication auth);
    }
}
