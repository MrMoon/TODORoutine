﻿using System;
using System.Collections.Generic;
using TODORoutine.database.general.dao;
using TODORoutine.models;

namespace TODORoutine.database.user.dao {

    /**
     * Main Data Access Layer for all the user comunicatation with the database
     * Handles the Database side of the Application for the User
     **/
    interface UserDAO : DatabaseDAO<User> {
        String findUserId(String username);
        String findUsername(String id);
        String findNotebookId(String id);
        User findByUsername(String username);
        bool isUserAuthenticated(String id);
        List<String> findAll();
    }
}
