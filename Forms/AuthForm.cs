using System.Windows.Forms;
using TODORoutine.Database.user.DTO;

namespace TODORoutine {
    public partial class StartupForm : Form {

        private UserDTO userDTO;

        public StartupForm() {
            InitializeComponent();
        }

        private void StartupForm_Load(object sender , System.EventArgs e) {
            userDTO = UserDTOImplementation.getInstance();
        }

        private void btnLogin_Click(object sender , System.EventArgs e) {
            
        }
    }
}
