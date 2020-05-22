using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TODORoutine.Database.Shared {

    /**
     * Main Genral Validtor that handle basic validation for Strings 
     **/
    class Validator {

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
                if (String.IsNullOrWhiteSpace(s)) return false;
            }
            return true;
        }

        public static bool isValidTexts(params TextBox[] txts) {
            bool flag = true;
            foreach(TextBox txt in txts) {
                if (String.IsNullOrEmpty(txt.Text)) {
                    flag = false;
                    txt.BackColor = Color.Red;
                }
            }
            return flag;
        }
    }
}
