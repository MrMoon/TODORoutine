using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Models;

namespace TODORoutine.Database.user.DAO {
    interface UserDAO {
        User findById(String id);

        User findByUsername(String username);

        void save(User user);

        void update(User user);

        void delete(User user); 
    }
}
