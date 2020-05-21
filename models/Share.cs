using System;
using System.Collections.Generic;
using System.Text;

namespace TODORoutine.models {
    class Share {
 
        public String userId { get; set; }
        public HashSet<String> documentsIds { get; set; }

        public Share() {
            documentsIds = new HashSet<String>();
        }

        public override String ToString() {
            String prefix = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("{ UserId : ");
            sb.Append(userId);
            sb.Append(" , \nDocuments Ids : { ");
            foreach(String id in documentsIds) {
                sb.Append(prefix);
                prefix = ",";
                sb.Append(id);
            }
            sb.Append("}\n}");
            return sb.ToString();
        }
    }
}
