using System;
using TODORoutine.database.general.dao;
using TODORoutine.Models;

namespace TODORoutine.Database.user.DAO {

    /**
     * Main Data Access Layer for all the user comunicatation with the database
     * Handles the Database side of the Application for the User
     **/
    interface UserDAO : DatabaseDAO<User> {
        String findUserId(String username);
        String findUserUsername(String id);
        String findUserNotesId(String id);
        User findByUsername(String username);
        bool isUserAuthenticated(String id);
    }
}
