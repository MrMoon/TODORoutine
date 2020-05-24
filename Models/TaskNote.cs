using System;
using System.Text;
using TODORoutine.general;
using TODORoutine.general.enums;

namespace TODORoutine.models {
    class TaskNote {
        public String noteId { get; set; }
        public String id { get; set; }
        public Status status { get; set; }
        public Priority priority { get; set; }
        public DateTime dueDate { get; set; }

        public override String ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ Task id : ");
            sb.Append(id);
            sb.Append(" , Task Note id : ");
            sb.Append(noteId);
            sb.Append(" , Status : ");
            sb.Append(status.ToString());
            sb.Append(" , Priority : ");
            sb.Append(priority.ToString());
            sb.Append(" , Due Date : ");
            sb.Append(dueDate.ToString());
            return sb.ToString();
        }

    }
}
