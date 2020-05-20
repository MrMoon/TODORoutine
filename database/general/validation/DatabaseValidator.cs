using System;
using System.Text.RegularExpressions;
using TODORoutine.models;
using TODORoutine.Models;

namespace TODORoutine.Database.Shared {

    /**
     * Main Genral Database Statments Validtor that handle basic validation for strings in the statment 
     **/
    class DatabaseValidator {

        /**
         * Simple Validtor for Strings that checks of Empty String and extra white spaces
         * 
         * @args : the strings to check
         * 
         * return true if and only if none of the Strings don't have extra white spaces, not Empty and not NULL
         **/
        public static bool isValidParameters(params String[] args) {
            String temp;
            foreach (String s in args) {
                if (String.IsNullOrEmpty(s)) return false;
                temp = Regex.Replace(s.ToString() , " {2,}" , " ");
                if (s.Length != temp.Length) return false;
            }
            return true;
        }

        /**
         * Simple Validtor for User Objects
         * 
         * @user : the user that will be validated 
         * 
         * return true if and only if the user is valid according to the @isValidParameters
         **/
        public static bool isValid<T>(T t) => t != null;
    }
}
