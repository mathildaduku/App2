using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238





namespace App2
{
    public sealed partial class RegisterPage : Page, INotifyPropertyChanged
    {
        private RegisterViewModel _viewModel;

        public RegisterPage()
        {
            InitializeComponent();
            ViewModel = new RegisterViewModel();
            DataContext = ViewModel;
        }

        public RegisterViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                NotifyPropertyChanged();
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // You can access ViewModel.FirstName, ViewModel.LastName, ViewModel.Email, and ViewModel.Password here
            string firstName = ViewModel.FirstName;
            string lastName = ViewModel.LastName;
            string email = ViewModel.Email;
            string password = ViewModel.Password;

            // Validate input fields (for example, check if they are not empty)
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewModel.ErrorMessage = "Please fill in all fields.";
                return;
            }

            // Send registration request to the backend API
            bool registrationSuccess = await RegisterAsync(firstName, lastName, email, password);
            if (registrationSuccess)
            {
                ViewModel.ErrorMessage = ""; // Clear any previous error message
                ShowMessageBox("Registration", "Registration successful!");
            }
            else
            {
                ViewModel.ErrorMessage = "Failed to register. Please try again.";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the previous page
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async Task<bool> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            try
            {
                // Construct the JSON payload
                var payload = new
                {
                    firstName = firstName,
                    lastName = lastName,
                    email = email,
                    password = password
                };

                string jsonPayload = JsonSerializer.Serialize(payload);

                // Send POST request to the registration endpoint
                using (var client = new HttpClient())
                {
                     var response = await client.PostAsync("https://crispygatewayservice.azurewebsites.net/api/auth/register", new StringContent(jsonPayload, Encoding.UTF8, "application/json"));


                    // Check if request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read response content
                        string responseBody = await response.Content.ReadAsStringAsync();
                        // Parse response JSON
                        var result = JsonSerializer.Deserialize<RegistrationResponse>(responseBody);
                        return true;
                    }
                    else
                    {
                        // Handle unsuccessful HTTP response
                        ViewModel.ErrorMessage = $"Failed to register. Status code: {response.StatusCode}";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                ViewModel.ErrorMessage = $"An error occurred: {ex.Message}";
                return false;
            }
        }

        private async void ShowMessageBox(string title, string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK"
            };

            await dialog.ShowAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _errorMessage;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RegistrationResponse
    {
        public bool Success { get; set; }
    }
}

