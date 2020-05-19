using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TODORoutine.authentication;
using TODORoutine.database.parsers;
using TODORoutine.Database.user.DTO;
using TODORoutine.Models;

namespace TODORoutine.forms {
    public partial class Test : Form {

        private User user;
        private UserDTO dto;
        private Authenticate auth;
        private String file = @"E:\MyDocs\Programming\Projects\Data Sets\CSV\idusername.csv";

        public Test() {
            InitializeComponent();
        }

        private void Test_Load(object sender , EventArgs e) {
            dto = UserDTOImplementation.getInstance();
            auth = new Authenticate(dto);
        }

        private void btnInsert_Click(object sender , EventArgs e) {
            int counter = 0;
            StreamReader reader = new StreamReader(file);
            while(!reader.EndOfStream) {
                String line = reader.ReadLine();
                String[] val = line.Split(',');
                User user = new User();
                user.setFullName(val[1]);
                user.setUsername(val[0]);
                user.setIsAuthenticated(0);
                if (dto.save(user)) ++counter;
            }
            Console.WriteLine(counter);
        }

        private void btnDelete_Click(object sender , EventArgs e) {
            Console.WriteLine((dto.delete(user.getId())));  
        }

        private void btnUpdate_Click(object sender , EventArgs e) {
            user.setFullName(txtName.Text);
            user.setUsername(txtUsername.Text);
            Console.WriteLine(dto.update(user , DatabaseConstants.COLUMN_FULLNAME , DatabaseConstants.COLUMN_USERNAME));
            Console.WriteLine(user.toString());
        }

        private void btnSelect_Click(object sender , EventArgs e) {
            user = dto.getByUsername(txtUsername.Text);
            Console.WriteLine(user.toString());
        }

        private void btnLogin_Click(object sender , EventArgs e) {
            Console.WriteLine(user.toString());
            Console.WriteLine(auth.authentication(user , true));
        }

        private void btnRegister_Click(object sender , EventArgs e) {
            user = new User();
            user.setFullName(txtName.Text);
            user.setUsername(txtUsername.Text);
            user.setIsAuthenticated(1);
            Console.WriteLine(auth.authentication(user , false));
            Console.WriteLine(user.toString());
        }
    }
}
