using System;
using System.Drawing;
using System.Windows.Forms;

namespace TODORoutine.general.validation {

    /**
     * Main Genral Validtor that handle basic validation for Strings 
     **/
    public class DataValidator {

        /**
         * Simple Validtor for Strings that checks of Empty String and extra white spaces
         * 
         * @args : the Strings to check
         * 
         * return true if and only if none of the Strings don't have extra white spaces, not Empty and not NULL
         **/
        public static bool isValidParameters(params String[] args) {
            foreach (String s in args) 
                if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s)) return false;
            return true;
        }

        /**
         * Validating Controls and changing the background color if it's invalid
         * 
         * @controlers : the controls to validate
         * 
         * return true if and only if all of the controlers are valid and false otherwise
         **/
        public static bool isValidTexts(params Control[] controlers) {
            bool flag = true;
            foreach(Control control in controlers) {
                if (String.IsNullOrEmpty(control.Text)) {
                    flag = false;
                    control.BackColor = Color.Red;
                }
            }
            return flag;
        }
    }
}
