using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Shared;

namespace TODORoutine.Database.user.DTO {
    class UserDTOImplementation : UserDTO {

        private static String tableName = Constants.TABEL_TODOROUTINE;
        private static UserDTO userDTO = null;
        private static DatabaseDriver databaseDriver = null;

        private UserDTOImplementation() {
            databaseDriver = DatabaseDriver.getInstance();
        }

        public UserDTO getInstance() {
            if (userDTO == null) userDTO = new UserDTOImplementation();
            return userDTO;
        }

        public String getIdQuery(String username) => Constants.getSelect(tableName , Constants.COLUMN_USERNAME , Constants.COLUMN_USERID , username);

        public String getNotesIdQuery(String id) => Constants.getSelect(tableName , Constants.COLUMN_USERID , Constants.COLUMN_NOTESID , id);

        public String getUsernameQuery(String id) => Constants.getSelect(tableName , Constants.COLUMN_USERID , Constants.COLUMN_USERNAME , id);

    }
}
