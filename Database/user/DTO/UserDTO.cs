using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Models;

namespace TODORoutine.Database.user.DTO {
    /**
     * Main Interface for all the user Data Transfor Object to comunicate with other classes
     **/
    interface UserDTO {
        ArrayList compare(User oldUser , User newUser);
        String getIdQuery(String username);
        String getUsernameQuery(String id);
        String getNotesIdQuery(String id);
    }
}
