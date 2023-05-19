using Team_Sharp.Utility;
using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Globalization;

namespace Team_Sharp.View
{
    public partial class Dashboard : UserControl
    {
        private List<Activity> activities = new List<Activity>();

        public Dashboard()
        {
            InitializeComponent();

            ReadProgress();
            LoadMotivation();


        }

        private void ReadProgress()
        {
            string progress = $@"../../../DataBase/Language/{Global._language}/Progress/{Global._userName}.txt";

            if (!File.Exists(progress))
            {
                MessageBox.Show("No records found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] proggLines = File.ReadAllLines(progress);

            string exp = null;
            string level = null;
            string proficiency = null;

            foreach (string uL in proggLines)
            {
                if (uL.StartsWith("EXP:"))
                {
                    exp = uL.Remove(0, 4);
                }
                else if (uL.StartsWith("Level:"))
                {
                    level = uL.Remove(0, 6);
                }
                else if (uL.StartsWith("Proficiency:"))
                {
                    proficiency = uL.Remove(0, 12);
                }

            }

            UserProgress._exp = int.Parse(exp);
            UserProgress._userProgressLevel = int.Parse(level);
            UserProgress._userProgressProficiency = proficiency;
            progressBar.Value = UserProgress._exp / 10;
            progressTextPer.Text = (UserProgress._exp / 10).ToString("0.00") + "%";

            progressLevel.Text = UserProgress._userProgressLevel.ToString();
            progressProff.Text = UserProgress._userProgressProficiency.ToString();
            totalExp.Text = UserProgress._exp.ToString();
        }

        private void LoadMotivation()
        {

            string filePath = $@"../../../DataBase/Quotes/Quotes.txt";
            string[] quotes = File.ReadAllLines(filePath);


            Random random = new Random();
            int index = random.Next(0, quotes.Length);


            string[] parts = quotes[index].Split('_');
            string randomText = parts[0].Trim();
            string person = parts[1].Trim();


            QuoteText.Text = $"{randomText} " + "\n" +
                $"      - {person}";
        }

        private void MyCalendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = Calendar.SelectedDate.Value;

            
            List<Activity> activities = ReadActivitiesFromFile(Global._userName);


            List<Activity> activitiesForSelectedDate = new List<Activity>();

            foreach (Activity a in activities)
            {
                if (a.Date.Date == selectedDate.Date)
                {
                    activitiesForSelectedDate.Add(a);
                }
            }
            ActivityList.ItemsSource = activitiesForSelectedDate;
        }

        private List<Activity> ReadActivitiesFromFile(string str)
        {
            List<Activity> activities = new List<Activity>();

            string filePath = $@"../../../DataBase/DashBoardActivity/{Global._language}/{str}.txt";
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                DateTime date = DateTime.Parse(parts[0].Trim());
                string name = parts[1].Trim();
                activities.Add(new Activity { Date = date, Name = name });
            }

            return activities;
        }

    }   
}
