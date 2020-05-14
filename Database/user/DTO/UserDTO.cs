using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.Database.user.DTO {
    interface UserDTO {

        String getIdQuery(String username);

        String getUsernameQuery(String id);

        String getNotesIdQuery(String id);
    }
}
