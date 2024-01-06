using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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


        public void ReadProgress(string filePath, User user)
        {
            try
            {
                string[] proggLines = File.ReadAllLines(filePath);

                string exp = null;
                string level = null;
                string proficiency = null;

                foreach (string line in proggLines)
                {
                    if (line.StartsWith("EXP:"))
                    {
                        exp = line.Substring("EXP:".Length).Trim();
                    }
                    else if (line.StartsWith("Level:"))
                    {
                        level = line.Substring("Level:".Length).Trim();
                    }
                    else if (line.StartsWith("Proficiency:"))
                    {
                        proficiency = line.Substring("Proficiency:".Length).Trim();
                    }
                }

                user.Experience = int.Parse(exp);
                user.UserProgressLevel = int.Parse(level);
                user.UserProgressProficiency = proficiency;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading progress file: {ex.Message}");
            }
        }


        public void SetUserImage(string gender, ImageBrush userImage)
        {
            string imagePath = GetImagePathByGender(gender);
            userImage.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
        }

        private string GetImagePathByGender(string gender)
        {
            switch (gender)
            {
                case "Male":
                    return @"../../../Team_Sharp/Assets/dudeIcon.png";
                case "Female":
                    return @"../../../Team_Sharp/Assets/girlIcon.png";
                case "Other":
                    return @"../../../Team_Sharp/Assets/otherGenIcon.png";
                default:
                    return string.Empty;
            }
        }


        public void LoadMotivation(TextBlock quoteText)
        {
            string filePath = $@"../../../DataBase/Quotes/Quotes.txt";
            string randomQuote = GetRandomQuote(filePath);

            if (!string.IsNullOrEmpty(randomQuote))
            {
                string[] parts = randomQuote.Split('_');
                string randomText = parts[0].Trim();
                string person = parts[1].Trim();

                DisplayQuote(quoteText, randomText, person);
            }
        }

        private string GetRandomQuote(string filePath)
        {
            try
            {
                string[] quotes = File.ReadAllLines(filePath);
                Random random = new Random();
                int index = random.Next(0, quotes.Length);
                return quotes[index];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading quotes file: {ex.Message}");
                return string.Empty;
            }
        }

        private void DisplayQuote(TextBlock quoteText, string randomText, string person)
        {
            quoteText.Text = $"{randomText}\n      - {person}";
        }



    }

}
