using System;
using System.Text;

namespace TODORoutine.Shared {
    /**
     * Pair Class that handle Strings that are connected 
     **/
    public class Pair {
        public Pair() { }

        public Pair(String first , String second) {
            this.first = first;
            this.second = second;
        }

        public String first { get; set; }
        public String second { get; set; }

        public override String ToString() {
            StringBuilder StringBuilder = new StringBuilder();
            StringBuilder.Append("{ ");
            StringBuilder.Append(first);
            StringBuilder.Append(" , ");
            StringBuilder.Append(second);
            StringBuilder.Append(" }");
            return StringBuilder.ToString();
        }
    }
}
