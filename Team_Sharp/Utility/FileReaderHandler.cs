using System;
using System.IO;

namespace Team_Sharp.Utility
{
    public class FileReaderHandler
    {
        public bool UserFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public User ReadUserFromFile(string filePath)
        {
            try
            {
                string[] userLines = File.ReadAllLines(filePath);

                User user = new User();

                foreach (string line in userLines)
                {
                    if (line.StartsWith("Username:"))
                    {
                        user.Username = line.Substring("Username:".Length).Trim();
                    }
                    else if (line.StartsWith("Password:"))
                    {
                        user.Password = line.Substring("Password:".Length).Trim();
                    }
                    else if (line.StartsWith("Gender:"))
                    {
                        user.Gender = line.Substring("Gender:".Length).Trim();
                    }
                    else if (line.StartsWith("Name:"))
                    {
                        user.Name = line.Substring("Name:".Length).Trim();
                    }
                    else if (line.StartsWith("Email:"))
                    {
                        user.Email = line.Substring("Email:".Length).Trim();
                    }
                    else if (line.StartsWith("DOB:"))
                    {
                        user.DateOfBirth = line.Substring("DOB:".Length).Trim();
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading user file: {ex.Message}");
                return null;
            }
        }
    }

}
