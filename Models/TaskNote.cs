using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using TODORoutine.general;
using TODORoutine.general.enums;

namespace TODORoutine.models {
    public class TaskNote {
        public String noteId { get; set; }
        public String document { get; set; }
        public String id { get; set; }
        public Status status { get; set; }
        public Priority priority { get; set; }
        public DateTime dueDate { get; set; }

        public TaskNote() { }

        public TaskNote(String id , DateTime dueDate , Priority priority , Status status , String docuemnt) {
            this.id = id;
            this.dueDate = dueDate;
            this.priority = priority;
            this.status = status;
            this.document = document;
        }

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
