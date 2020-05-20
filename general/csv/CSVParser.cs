using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TODORoutine.shared.csv {
    class CSVParser {
        public static String CSV2String(List<String> list) {
            StringBuilder sb = new StringBuilder();
            String prefix = "";
            foreach (String documentId in list) {
                sb.Append(prefix);
                prefix = ",";
                sb.Append(documentId);
            }
            return sb.ToString();
        }

        public static List<String> CSV2List(String list) {
            if (String.IsNullOrEmpty(list)) return new List<String>();
            return list.Split(',').ToList<String>();
        }
    }
}
