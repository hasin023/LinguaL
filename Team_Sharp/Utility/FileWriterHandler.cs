using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Sharp.Utility
{
    public class FileWriterHandler
    {

        public void WriteUserToFile(User user, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Username:" + user.Username);
                writer.WriteLine("Password:" + user.Password);
                writer.WriteLine("Name:" + user.Name);
                writer.WriteLine("Email:" + user.Email);
                writer.WriteLine("DOB:" + user.DateOfBirth);
                writer.WriteLine("Gender:" + user.Gender);
            }
        }


    }
}
