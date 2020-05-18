using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TODORoutine.shared.csv {
    class CSVParser {
        public static String CSV2String(List<String> list) {
            if (list.Count() == 0) return "";
            StringBuilder sb = new StringBuilder();
            foreach (String documentId in list) {
                sb.Append(documentId);
                if (documentId != list[list.Count() - 1]) sb.Append(",");
            }
            return sb.ToString();
        }

        public static List<String> CSV2List(String list) {
            if (String.IsNullOrEmpty(list)) return new List<String>();
            return list.Split(',').ToList<String>();
        }
    }
}
