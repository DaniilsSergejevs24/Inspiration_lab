using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace cinema_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string UserDataFile = "userdata.xml";
        private UserData userData;

        public MainWindow()
        {
            InitializeComponent();
            LoadSavedCredentials();
        }

        private void LoadSavedCredentials()
        {
            userData = UserDataManager.LoadUserData();

            if (userData != null && userData.RememberMe)
            {
                EmailTextBox.Text = userData.Email;
                RememberMeCheckBox.IsChecked = true;
                // Note: For security reasons, we don't auto-fill the password
                // Focus on password field for better UX
                PasswordBox.Focus();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            bool rememberMe = RememberMeCheckBox.IsChecked ?? false;

            // Basic validation
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                StatusMessage.Text = "Please enter both email and password.";
                return;
            }

            // Email format validation
            if (!IsValidEmail(email))
            {
                StatusMessage.Text = "Please enter a valid email address.";
                return;
            }

            // In a real app, you would authenticate against a database or service
            // For this example, we'll simulate a successful login
            if (AuthenticateUser(email, password))
            {
                // Save user data if "Remember me" is checked
                if (rememberMe)
                {
                    SaveUserCredentials(email, rememberMe);
                }
                else
                {
                    // Clear saved credentials if "Remember me" is unchecked
                    UserDataManager.ClearUserData();
                }

                // Navigate to the main application window
                DashboardWindow dashboard = new DashboardWindow(email);
                dashboard.Show();
                this.Close();
            }
            else
            {
                StatusMessage.Text = "Invalid email or password.";
            }
        }

        private bool IsValidEmail(string email)
        {
            // Simple regex for email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool AuthenticateUser(string email, string password)
        {
            // In a real application, this would verify against a database
            // For this example, we'll accept a demo account
            return (email == "demo@example.com" && password == "password") ||
                   (email == "test@example.com" && password == "test123");
        }

        private void SaveUserCredentials(string email, bool rememberMe)
        {
            UserData newUserData = new UserData
            {
                Email = email,
                RememberMe = rememberMe
            };

            UserDataManager.SaveUserData(newUserData);
        }

        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to registration window
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        private void ForgotPasswordLink_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to password recovery window
            PasswordRecoveryWindow recoveryWindow = new PasswordRecoveryWindow();
            recoveryWindow.Show();
            this.Close();
        }
    }

    public class UserData
    {
        public string Email { get; set; }
        public bool RememberMe { get; set; }
    }

    public static class UserDataManager
    {
        private const string UserDataFile = "userdata.xml";

        public static UserData LoadUserData()
        {
            try
            {
                if (File.Exists(UserDataFile))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(UserData));
                    using (FileStream fs = new FileStream(UserDataFile, FileMode.Open))
                    {
                        return (UserData)serializer.Deserialize(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }

        public static void SaveUserData(UserData userData)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UserData));
                using (FileStream fs = new FileStream(UserDataFile, FileMode.Create))
                {
                    serializer.Serialize(fs, userData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving user data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void ClearUserData()
        {
            try
            {
                if (File.Exists(UserDataFile))
                {
                    File.Delete(UserDataFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing user data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}