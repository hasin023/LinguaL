using System;
using System.Diagnostics;
using System.IO;
using Team_Sharp.Model;
using Team_Sharp.Utility;

namespace Team_Sharp.Handlers
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


        public void WriteProgress(string fileName, User loggedInUser)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine($"EXP:{loggedInUser.Progress.UserExperience}");
                writer.WriteLine($"Level:{loggedInUser.Progress.UserProgressLevel}");
                writer.WriteLine($"Proficiency:{loggedInUser.Progress.UserProgressProficiency}");
            }
        }


        public void ReplaceLineInFile(string filePath, string oldLine, string newLine)
        {
            string[] lines = File.ReadAllLines(filePath);

            int index = Array.IndexOf(lines, oldLine);

            if (index >= 0)
            {
                lines[index] = newLine;

                File.WriteAllLines(filePath, lines);
            }
        }


        public void AppendTextToFile(string filePath, string textToAppend)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(textToAppend);
            }
        }

    }
}
