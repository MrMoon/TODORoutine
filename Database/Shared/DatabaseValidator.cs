using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TODORoutine.Models;

namespace TODORoutine.Database.Shared {
    class DatabaseValidator {

        /**
         * Simple Validtor for Strings that checks of Empty String and extra white spaces
         * @args : the strings to check
         * return true if and only if none of the Strings don't have extra white spaces, not Empty and not NULL
         **/
        public static bool isValidParameters<T>( params T[] args) {
            string temp;
            foreach (T s in args) {
                if (String.IsNullOrEmpty(s.ToString())) return false;
                temp = Regex.Replace(s.ToString() , " {2,}" , " ");
                if (s.ToString().Length != temp.Length) return false;
            }
            return true;
        }

        public static bool isValidUser(User user) { return isValidParameters(user.getUsername() , user.getId() , user.getNotesId()); }

    }
}
