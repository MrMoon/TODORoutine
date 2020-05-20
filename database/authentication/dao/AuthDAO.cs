using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.authentication;
using TODORoutine.database.general.dao;

namespace TODORoutine.database.authentication.dao {
    /**
     * Main Data Layer for authentication
     **/
    interface AuthDAO {
        bool login(String username , String password);
        bool register(String username , String password);
    }
}
