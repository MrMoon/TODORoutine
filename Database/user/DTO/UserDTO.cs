using System;
using System.Collections.Generic;
using TODORoutine.database.general.dto;
using TODORoutine.models;

namespace TODORoutine.database.user.dto {
    /**
     * Main Transfor Layer between the Data Access Layer and the Application Layer
     * Handles the User Data transfor between the Transfor Layer and the Data Layer
     **/
    interface UserDTO : DatabaseDTO<User> {
        User getByUsername(String username);
        String getId(String username);
        String getUsername(String id);
        String getNotesId(String id);
        bool isAuthenticated(String id);
        List<User> getAll();
    }
}
