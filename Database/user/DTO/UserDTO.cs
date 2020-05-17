using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Models;

namespace TODORoutine.Database.user.DTO {
    /**
     * Main Layer for all the user Data Transfor to comunicate with other classes
     * Handles the User Data transfor between the Transfor Layer and the Data Layer
     **/
    public interface UserDTO {
        User getUserById(String id);
        User getUserByUsername(String username);
        String getUserId(String username);
        String getUserUsername(String id);
        String getUserNotesId(String id);
        bool saveUser(User user);
        bool deleteUser(User user);
        bool updateUser(User user , params String[] columns);
        int isAuthenticated(String username);
    }
}
