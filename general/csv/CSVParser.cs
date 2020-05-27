using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TODORoutine.Models;

namespace TODORoutine.shared.csv {
    class CSVParser {

        public static readonly String FILE = "CSV files (*.csv)|*.csv";

        public static String CSV2String(List<String> list) {
            StringBuilder csv = new StringBuilder();
            String prefix = "";
            foreach (String txt in list) {
                csv.Append(prefix);
                prefix = ",";
                csv.Append(txt);
            }
            return csv.ToString();
        }

        public static List<String> CSV2List(String list) {
            if (String.IsNullOrEmpty(list)) return new List<String>();
            return list.Split(',').ToList<String>();
        }

        public static User getUser(String text) {
            if (String.IsNullOrEmpty(text)) return new User();
            String[] line = text.Split(',');
            User user = new User();
            user.setFullName(line[0]);
            user.setUsername(line[1]);
            return user;
        }
    }
}
