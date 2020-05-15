using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Models;

namespace TODORoutine.Database.user.DAO {
    /**
     * Main Interface for all the user Data Access Object to comunicate with the database
     **/
    interface UserDAO {
        User findById(String id);
        User findByUsername(String username);
        bool save(User user);
        int update(User oldUser , User newUser);
        int delete(User user);
    }
}
