using System;
using TODORoutine.database.general.dto;
using TODORoutine.Models;

namespace TODORoutine.Database.user.DTO {
    /**
     * Main Transfor Layer between the Data Access Layer and the Application Layer
     * Handles the User Data transfor between the Transfor Layer and the Data Layer
     **/
    public interface UserDTO : DatabaseDTO<User> {
        User getUserByUsername(String username);
        String getUserId(String username);
        String getUserUsername(String id);
        String getUserNotesId(String id);
        int isAuthenticated(String username);
    }
}
