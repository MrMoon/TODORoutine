using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TODORoutine.database.authentication.dto;
using TODORoutine.database.user.dto;
using TODORoutine.general.constants;
using TODORoutine.general.csv;
using TODORoutine.general.validation;
using TODORoutine.models;

namespace TODORoutine.forms {
    public partial class AuthForm : Form {

        private bool createAccount = false;
        private readonly AuthenticationDTO auth = AuthenticationDTOImplementation.getInstance();

        public AuthForm() => InitializeComponent();

        private void AuthForm_Load(object sender , System.EventArgs e) => this.ActiveControl = txtUsername;

        /**
         * Login button
         * check the validity of the username and password
         * if all is valid check the authentication
         * if the authentication is good go to the TODOForm with the user object
         **/
        private void btnLogin_Click(object sender , System.EventArgs e) {
            if(DataValidator.isValidTexts(txtUsername , txtPassword)) {
                if (auth.authenticate(new Authentication(txtUsername.Text , txtPassword.Text) , true)) {
                    this.Hide();
                    User user = new User();
                    user.setUsername(txtUsername.Text);
                    BrainstormFrom textEditor = new BrainstormFrom(user , true);
                    textEditor.Closed += (s , args) => this.Close(); //It creates a function "in place" that is called when the form2.Closed event is fired.
                    textEditor.Show();
                } else {
                    MessageBox.Show(ErrorMessages.SOMETHING_WENT_WRONG(UserMessages.USERNAME_PASSWORD));
                    txtUsername.Focus();
                }
            }
        }

        /**
         * Register button
         * check the validity of the username, password , confirm password and name
         * if all is valid check the authentication
         * if the authentication is good go to the TODOForm with the user object
         **/
        private void btnRegister_Click(object sender , System.EventArgs e) {
            if(!createAccount) {
                createAccount = true;
                txtName.Visible = true;
                txtConfirmPassword.Visible = true;
                lblConfirmPassword.Visible = true;
                lblName.Visible = true;
                this.AcceptButton = btnRegister;
            } else {
                if(DataValidator.isValidTexts(txtUsername , txtPassword , txtConfirmPassword , txtName)) {
                    if (txtPassword.Text.Length <= 6) {
                        lblPasswordMessage.Text = ErrorMessages.PASSWORD_LENGTH;
                        txtPassword.BackColor = Color.Red;
                    } else if (!txtPassword.Text.Equals(txtConfirmPassword.Text)) {
                        lblConfirmMessage.Text = ErrorMessages.PASSWORD_MATCH;
                        txtConfirmPassword.BackColor = Color.Red;
                    } else {
                        if (auth.authenticate(new Authentication(txtUsername.Text , txtPassword.Text))) {
                            this.Hide();
                            User user = new User();
                            user.setUsername(txtUsername.Text);
                            user.setFullName(txtName.Text);
                            user.setIsAuthenticated(1);
                            BrainstormFrom textEditor = new BrainstormFrom(user);
                            textEditor.Closed += (s , args) => this.Close(); //It creates a function "in place" that is called when the form2.Closed event is fired.
                            textEditor.Show();
                        } else {
                            MessageBox.Show(ErrorMessages.SOMETHING_WENT_WRONG(UserMessages.USERNAME_TAKEN));
                            txtUsername.Focus();
                        }
                    }
                }
            }
        }

        private void txtUsername_TextChanged(object sender , System.EventArgs e) {
            if (txtUsername.Focused) txtUsername.BackColor = Color.White;
        }

        private void txtPassword_TextChanged(object sender , System.EventArgs e) {
            if (txtPassword.Focused) txtPassword.BackColor = Color.White;
        }

        private void txtPassword_KeyDown(object sender , KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) btnLogin_Click(this , new EventArgs());
        } 

        private void txtConfirmPassword_TextChanged(object sender , System.EventArgs e) {
            if (txtConfirmPassword.Focused) txtConfirmPassword.BackColor = Color.White;
        }

        private void txtName_TextChanged(object sender , System.EventArgs e) {
            if (txtName.Focused) txtName.BackColor = Color.White;
        }
        private void txtName_KeyDown(object sender , KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) btnRegister_Click(this , new EventArgs());
        }

        private void picLogo_Click(object sender , EventArgs e) {
            btnReadCSV.Visible = TypesConstants.FLIP(btnReadCSV.Visible);
            btnWriteCSV.Visible = TypesConstants.FLIP(btnWriteCSV.Visible);
        }

        private void btnReadCSV_Click(object sender , EventArgs e) {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = CSVParser.FILE;
            int counter = 0;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                Stream fileStream = openFileDialog.OpenFile();
                StreamReader reader = new StreamReader(fileStream);
                while (!reader.EndOfStream) 
                    counter += UserDTOImplementation.getInstance()
                        .save(CSVParser.getUser(reader.ReadLine())) ? 1 : 0;
                reader.Close();
            }
            MessageBox.Show(counter + " was added successfully to the database");
        }

        private void btnWriteCSV_Click(object sender , EventArgs e) {
            TextWriter writer = new StreamWriter("users.csv");
            List<User> users = UserDTOImplementation.getInstance().getAll();
            foreach(User user in users) writer.WriteLine(CSVParser.setUser(user));
            writer.Close();
        }
    }
}