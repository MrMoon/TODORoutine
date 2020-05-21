namespace TODORoutine.database.authentication.dto {
    interface AuthenticationDTO {
        bool authenticate(Authentication auth , bool isLogin = false);
    }
}
