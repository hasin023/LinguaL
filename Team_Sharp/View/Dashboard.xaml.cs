using Team_Sharp.Handlers;
using Team_Sharp.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace Team_Sharp.View
{
    public partial class Dashboard : UserControl
    {
        private readonly User loggedInUser;
        private FileReaderHandler fileReaderHandler;

        public Dashboard(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            fileReaderHandler = new FileReaderHandler();

            LoadProgressBar();
            fileReaderHandler.LoadMotivation(QuoteText);
        }

        private void LoadProgressBar()
        {
            string progress = $@"../../../DataBase/Language/{loggedInUser.Language}/Progress/{loggedInUser.Username}.txt";

            if (!fileReaderHandler.UserFileExists(progress))
            {
                MessageBox.Show("No records found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            fileReaderHandler.ReadProgress(progress, loggedInUser);


            progressBar.Value = loggedInUser.Experience / 10;
            progressTextPer.Text = (loggedInUser.Experience / 10).ToString("0.00") + "%";

            progressLevel.Text = loggedInUser.UserProgressLevel.ToString();
            progressProff.Text = loggedInUser.UserProgressProficiency.ToString();
            totalExp.Text = loggedInUser.Experience.ToString();
        }


        private void MyCalendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = Calendar.SelectedDate.Value;

            List<Activity> activities = fileReaderHandler.ReadActivitiesFromFile(loggedInUser);

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



    }   
}
