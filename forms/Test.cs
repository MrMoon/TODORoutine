using NLog.Fluent;
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
using TODORoutine.Database.user.DAO;
using TODORoutine.Database.user.DTO;
using TODORoutine.Models;

namespace TODORoutine.forms {
    public partial class Test : Form {

        private UserDTO dto;
        private Authenticate auth;
        private User user;
        private int ans = 0;
        private static int idx = 0 , jdx = 0;
        private static String file = @"E:\MyDocs\Programming\Projects\Data Sets\CSV\idusername.csv";

        public Test() {
            InitializeComponent();
        }

        private void Test_Load(object sender , EventArgs e) {
            dto = UserDTOImplementation.getInstance();
            auth = new Authenticate(dto);
        }


        private void btnId_Click(object sender , EventArgs e) {
            user = dto.getUserById(txtID.Text);
            if(user != null) MessageBox.Show(user.toString());
            else MessageBox.Show("NULL");
        }

        private void txtID_TextChanged(object sender , EventArgs e) {

        }

        private void textBox1_TextChanged(object sender , EventArgs e) {

        }

        private void btnUsername_Click(object sender , EventArgs e) {
            user = dto.getUserByUsername(txtUsername.Text);
            if (user != null) MessageBox.Show(user.toString());

        }

        private void btnUpdate_Click(object sender , EventArgs e) {
            String fullName = txtID.Text , username = txtUsername.Text;
            user.setFullName(fullName);
            user.setUsername(username);
            Console.WriteLine(user.toString());
            if (dto.updateUser(user , DatabaseConstants.COLUMN_FULLNAME , DatabaseConstants.COLUMN_USERNAME)) Console.WriteLine("Update Successfull");
            else Console.WriteLine("Update Failed");
        }

        private void btnDelete_Click(object sender , EventArgs e) {
            if(dto.deleteUser(user)) Console.WriteLine("Delete Successfull");
            else Console.WriteLine("Delete Failed");
        }

        private void btnRegister_Click(object sender , EventArgs e) {
            User u = new User();
            u.setFullName(txtID.Text);
            u.setUsername(txtUsername.Text);
            u.setIsAuthenticated(1);
            int x = auth.authentication(u);
            Console.WriteLine(x);
            if (x == -1) MessageBox.Show("Already Exsits");
            else if (x == 0) MessageBox.Show("Failed");
            else MessageBox.Show("DONE");
        }

        private void btnLogin_Click(object sender , EventArgs e) {
            User u = new User();
            u.setUsername(txtUsername.Text);
            int x = auth.authentication(u , true);
            if (x == -1 || x == 0) MessageBox.Show("Failed");
            else MessageBox.Show("WELCOME");
        }

        private void btnReadInsert_Click(object sender , EventArgs e) {
            StreamReader streamReader = new StreamReader(file);
            while (!streamReader.EndOfStream) {
                String line = streamReader.ReadLine();
                String[] values = line.Split(',');
                User user = new User();
                user.setFullName(values[1]);
                user.setUsername(values[0]);
                if (dto.saveUser(user)) ++ans;
            }
            MessageBox.Show(ans.ToString() , "Number of saved users");
        }
    }
}
