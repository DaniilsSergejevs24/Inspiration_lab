using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cinema_project
{
    public partial class DashboardWindow : Window
    {
        public DashboardWindow(string email)
        {
            InitializeComponent();
            WelcomeMessage.Text = $"Logged in as: {email}";
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
