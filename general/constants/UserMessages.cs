﻿using System;

namespace TODORoutine.general.constants {
    /**
     * Main User Messages Constatnts
     **/
    class UserMessages {
        public static readonly String USERNAME_PASSWORD = "Password and Username";
        public static readonly String TWO_NODE = "You must select exactly two nodes to connect";
        public static readonly String USERNAME_TAKEN = "Username Might be taken";
        public static readonly String ENABLE_EDIT = "Must Enable the Edit Option First";
        public static Func<String , String> ARE_YOU_SURE = (s) => "Are you sure you want to " + s + " ? ";
        public static Func<String , String> CONFIRMION = (s) => s + " Confirmation";
        public static Func<String , String> EMPTY_OPERATION = (s) => "There is nothing to " + s;
        public static readonly String CYCLE = "There is a cycle , therefore this cannot be sorted";
        public static readonly String DONE = "Done Successfully";
        public static readonly String FAILED = "Operation Failed";
        public static void messageStatus(bool flag) => UserMessages.messageStatus(flag);
    }
}
