using System.IO;

namespace Team_Sharp.Utility
{
    public class FileWriterHandler
    {

        private PasswordHasher passwordHasher = new PasswordHasher();

        public void WriteUserToFile(User user, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Username:" + user.Username);

                string hashedPassword = passwordHasher.HashPassword(user.Password);
                writer.WriteLine("Password:" + hashedPassword);

                writer.WriteLine("Name:" + user.Name);
                writer.WriteLine("Email:" + user.Email);
                writer.WriteLine("DOB:" + user.DateOfBirth);
                writer.WriteLine("Gender:" + user.Gender);
            }
        }


    }
}
