using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.database.user.exceptions {
    /**
     * Main User Strings that has all the important strings so it would be easier to edit and scale
     **/
    class UserConstants {
        public static String USER_NOT_FOUND = "User was Not Found!!";
        public static String USER_FOUND = "User was Found";
        public static String USER_INVLAID = "Invalid User";
        public static Func<String , String> INVALID = (s) => "Invalid " + s;
        public static Func<String , String> NOT_FOUND = (s) => s + " was not found";
    }
}
