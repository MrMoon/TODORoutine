using System;
using System.Security.Cryptography;
using System.Text;
using TODORoutine.database.authentication.dao;
using TODORoutine.Database.user.DTO;
using TODORoutine.Models;

namespace TODORoutine.authentication {
    /**
     * Main class that handle authentication operations (Login and Register)
     **/
    class Authenticate {

        private readonly static Random random = new Random();
        private readonly static int KEY = random.Next(10);
        public String username { get; set; }
        public String password { get; set; }
        private Login login;
        private Register register;

        public Authenticate(String username , String password) {
            this.username = username;
            this.password = password;
        }

        /**
         * Authentication Method 
         * 
         * @user : the user to authenticate
         * @isLogin : flag the authentication operation (Register or Login)
         * 
         * return 1 if the authentication was successfull , 0 if not and -1 if a database error occured
         **/
        public bool authentication(bool isLogin = false) {
            AuthDAO dao = AuthDAOImplentation.getInstance();
            if (isLogin) return dao.login(username , password);
            return dao.register(username , password);
        }

        public static String encrypt(String text , int key = 16) {
            String output = "";
            foreach (char c in text) output += cipher(c , key);
            return output;
        }

        public static String derypt(String text) {
            return encrypt(text , 26 - KEY);
        }

        public static char cipher(char ch , int key) {
            if (!char.IsLetter(ch)) return ch;
            char c = char.IsUpper(ch) ? 'A' : 'a';
            return (char) ((((ch + key) - c) % 26) + c);
        }

        public String toString() {
            return "{ Username : " + username + " , Password : " + password + "}\n";
        }

    }
}
