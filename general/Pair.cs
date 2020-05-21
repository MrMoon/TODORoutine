using System;
using System.Text;

namespace TODORoutine.Shared {
    /**
     * Pair Class that handle strings that are connected 
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
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{ ");
            stringBuilder.Append(first);
            stringBuilder.Append(" , ");
            stringBuilder.Append(second);
            stringBuilder.Append(" }");
            return stringBuilder.ToString();
        }
    }
}
