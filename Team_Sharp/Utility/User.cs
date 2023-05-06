using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Sharp.Utility
{
    public class User
    {
        public string _username { get; set; }
        public string _password { get; set; }
        public string _email { get; set; }
        public string _name { get; set; }
        public string _dob { get; set; }
        public int _sessionID { get; set; }

        public User(string username, string password, int sessionID, string email, string name, string dob)
        {
            this._username = username;
            this._password = password;
            this._sessionID = sessionID;
            this._email = email;
            this._name = name;
            this._dob = dob;
        }
    }
}
