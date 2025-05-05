using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace cinema_project
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Basic validation
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                StatusMessage.Text = "Please fill in all fields.";
                return;
            }

            // Email format validation
            if (!IsValidEmail(email))
            {
                StatusMessage.Text = "Please enter a valid email address.";
                return;
            }

            // Password validation
            if (password.Length < 6)
            {
                StatusMessage.Text = "Password must be at least 6 characters long.";
                return;
            }

            // Confirm password validation
            if (password != confirmPassword)
            {
                StatusMessage.Text = "Passwords do not match.";
                return;
            }

            // In a real application, this would save the user to a database
            // For this example, we'll simulate a successful registration

            MessageBox.Show($"Account created successfully for {fullName}!", "Registration Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            // Return to login screen
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
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
