using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace cinema_project
{
    public partial class PasswordRecoveryWindow : Window
    {
        public PasswordRecoveryWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;

            // Basic validation
            if (string.IsNullOrWhiteSpace(email))
            {
                StatusMessage.Text = "Please enter your email address.";
                StatusMessage.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            // Email format validation
            if (!IsValidEmail(email))
            {
                StatusMessage.Text = "Please enter a valid email address.";
                StatusMessage.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            // In a real application, this would send an email with recovery instructions
            StatusMessage.Text = "Recovery email sent. Please check your inbox.";
            StatusMessage.Foreground = System.Windows.Media.Brushes.Green;

            // Disable the submit button temporarily
            SubmitButton.IsEnabled = false;
        }

        private bool IsValidEmail(string email)
        {
            // Simple regex for email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private void BackToLoginLink_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
