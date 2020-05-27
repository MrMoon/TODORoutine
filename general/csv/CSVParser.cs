using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.shared.csv {
    /**
     * Main Comma Seprated Values Parser
     **/
    class CSVParser {

        public static readonly String FILE = "CSV files (*.csv)|*.csv";

        /**
         * Parsing a list to CSV
         * 
         * @list: the list to parse
         * 
         * return a csv string
         **/
        public static String CSV2String(List<String> list) {
            //Logging
            Logging.paramenterLogging(nameof(CSV2String) , false , new Pair(nameof(list) , list.ToString()));
            //Parsing
            StringBuilder csv = new StringBuilder();
            String prefix = "";
            foreach (String txt in list) {
                csv.Append(prefix);
                prefix = ",";
                csv.Append(txt);
            }
            return csv.ToString();
        }

        /**
         * Parsing a csv string to a list
         * 
         * @list: the list to parse
         * 
         * return a list of string from the list
         **/
        public static List<String> CSV2List(String list) {
            //Logging
            Logging.paramenterLogging(nameof(CSV2List) , false , new Pair(nameof(list) , list));
            //Parsing
            if (String.IsNullOrEmpty(list)) return new List<String>();
            return list.Split(',').ToList<String>();
        }

        /**
         * User parsing from csv
         * 
         * @text : the csv text
         * 
         * return a user object from the csv
         **/
        public static User getUser(String text) {
            if (String.IsNullOrEmpty(text)) return new User();
            //Logging
            Logging.paramenterLogging(nameof(getUser) , false , new Pair(nameof(text) , text));
            //Parsing
            String[] line = text.Split(',');
            User user = new User();
            user.setFullName(line[0]);
            user.setUsername(line[1]);
            return user;
        }

        /**
        * User parsing to csv
        * 
        * @user : the user to parse into a csv
        * 
        * return a csv user value
        **/
        public static String setUser(User user) {
            if (user == null) return "";
            //Logging
            Logging.paramenterLogging(nameof(setUser) , false , new Pair(nameof(user) , user.ToString()));
            //Parsing
            StringBuilder sb = new StringBuilder();
            sb.Append(user.getId());
            sb.Append(",");
            sb.Append(user.getUsername());
            sb.Append(",");
            sb.Append(user.getFullName());
            sb.Append(",\n");
            return sb.ToString();
        }
    }
}
