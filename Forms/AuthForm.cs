using MainTextEditor;
using System.Drawing;
using System.Windows.Forms;
using TODORoutine.database;
using TODORoutine.database.authentication.dto;
using TODORoutine.Database.Shared;
using TODORoutine.Database.user.DTO;
using TODORoutine.general;
using TODORoutine.Models;

namespace TODORoutine {
    public partial class AuthForm : Form {

        private bool createAccount = false;
        private AuthenticationDTO auth;

        public AuthForm() {
            InitializeComponent();
        }

        private void StartupForm_Load(object sender , System.EventArgs e) {
            auth = AuthenticationDTOImplentation.getInstance();
        }

        /**
         * Login button
         * check the validity of the username and password
         * if all is valid check the authentication
         * if the authentication is good go to the TODOForm with the user object
         **/
        private void btnLogin_Click(object sender , System.EventArgs e) {
            if(Validator.isValidTexts(txtUsername , txtPassword)) {
                if(auth.authenticate(new Authentication(txtUsername.Text , txtPassword.Text) , true)) {
                    this.Hide();
                    User user = new User();
                    user.setUsername(txtUsername.Text);
                    TextEditor textEditor = new TextEditor(user , true);
                    textEditor.Closed += (s , args) => this.Close(); //It creates a function "in place" that is called when the form2.Closed event is fired.
                    textEditor.Show();
                } else MessageBox.Show(ErrorMessages.SOMETHING_WENT_WRONG("Password and Username"));
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
            } else {
                if(Validator.isValidTexts(txtUsername , txtPassword , txtConfirmPassword , txtName)) {
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
                            TextEditor textEditor = new TextEditor(user);
                            textEditor.Closed += (s , args) => this.Close(); //It creates a function "in place" that is called when the form2.Closed event is fired.
                            textEditor.Show();
                        } else MessageBox.Show(ErrorMessages.SOMETHING_WENT_WRONG("Username Might be taken"));
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

        private void txtConfirmPassword_TextChanged(object sender , System.EventArgs e) {
            if (txtConfirmPassword.Focused) txtConfirmPassword.BackColor = Color.White;
        }

        private void txtName_TextChanged(object sender , System.EventArgs e) {
            if (txtName.Focused) txtName.BackColor = Color.White;
        }
    }
}
