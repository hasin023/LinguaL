using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Team_Sharp.Utility
{
    public class PasswordHasher
    {

        private const string Salt = "55VoicesInMyHeadAndGottaListenToAllOfThem";

        public string HashPassword(string password)
        {
            string combinedString = password + Salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedString));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }



    }
}
