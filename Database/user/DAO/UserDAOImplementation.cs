using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Models;

namespace TODORoutine.Database.user.DAO {
    class UserDAOImplementation : UserDAO {

        private UserDAO userDAO = null;
        private DatabaseDriver databaseDriver = null;

        private UserDAOImplementation() {
            databaseDriver = DatabaseDriver.getInstance();
        }

        public UserDAO getInstance() {
            if (userDAO == null) userDAO = new UserDAOImplementation();
            return userDAO;
        }

        public void delete(User user) {
            
        }

        public User findById(String id) {
            throw new NotImplementedException();
        }

        public User findByUsername(String username) {
            throw new NotImplementedException();
        }

        public UserDAO getInctence() {
            if (userDAO == null) userDAO = new UserDAOImplementation();
            return userDAO;
        }

        public void save(User user) {
            throw new NotImplementedException();
        }

        public void update(User user) {
            throw new NotImplementedException();
        }
    }
}
