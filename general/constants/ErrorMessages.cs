using System;

namespace TODORoutine.general.constants {

    /**
     * Main Error Messages Constatnts
     **/
    abstract class ErrorMessages {
        public static readonly String PASSWORD_MATCH = "Please Enter the same Password";
        public static readonly String PASSWORD_LENGTH = "Password must be 6 or more characters";
        public static readonly Func<String , String> SOMETHING_WENT_WRONG = (s) => "Something went wrong please check " + s;
    }
}
