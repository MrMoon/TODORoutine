using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Models;

namespace TODORoutine.Database.user.DAO {

    /**
     * Main Data Access Layer for all the user comunicatation with the database
     * Handles the Database side of the Application for the User
     **/
     interface UserDAO {
        String findUserId(String username);
        String findUserUsername(String id);
        String findUserNotesId(String id);
        User findById(String id);
        User findByUsername(String username);
        bool save(User user);
        bool update(User user , params String[] columns);
        bool delete(User user);
        bool isUserAuthenticated(String id);
    }
}
