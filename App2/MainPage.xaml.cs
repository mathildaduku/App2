using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private LoginViewModel _viewModel;

        public MainPage()
        {
            // calling the method below creates the xaml page with the ui visuals.
            // it is what creates the connection between the code and xaml
            this.InitializeComponent();
            ViewModel = new LoginViewModel();
            DataContext = ViewModel;
           
        }

        public LoginViewModel ViewModel
        {
            get { return _viewModel; }
            set { 
                _viewModel = value;
                NotifyPropertyChanged();

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

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
           /* this.Frame.Navigate(typeof(RegisterPage));*/

            // Access VieModel.Email and ViewModel.Password here
            string email = ViewModel.Email;
            string password = ViewModel.Password;

            if (!IsValidEmail(email))
            {
                ViewModel.ErrorMessage = "Please enter a valid email address.";
                return;
            }

            if (!IsValidPassword(password))
            {
                ViewModel.ErrorMessage = "Password must be at least 8 characters long.";
                return;
            }

          //  ViewModel.ErrorMessage = "pppppp"; // Clear any previous error medssage

            // Send login request to the backend API
            bool loginSuccess = await LoginAsync(email, password);
            if (loginSuccess)
            {
               // ViewModel.ErrorMessage = ""; // Clear any previous error medssage
                ShowMessageBox("Login", "Login successful!");
               /* // Navigate to page after successful login
                this.Frame.Navigate(typeof(homPage));*/
            }
            else
            {
                ViewModel.ErrorMessage = "Invalid email or password. Please try again.";
            }
        }

        private async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                // Construct the JSON payload
                var payload = new { email, password };
                string jsonPayload = JsonSerializer.Serialize(payload);

                // Send POST request to the login endpoint
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync("https://crispygatewayservice.azurewebsites.net/api/auth/login", new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
                    
                    // Check if request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read response content
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Parse response JSON
                        var result = JsonSerializer.Deserialize<LoginResponse>(responseBody);
                        return result.Success;
                    }
                    else
                    {
                        // Handle unsuccessful HTTP response
                        ViewModel.ErrorMessage = $"Failed to authenticate. Status code: {response.StatusCode}";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                ViewModel.ErrorMessage = $"An error occured: {ex.Message}";
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

        private bool IsValidEmail(string email)
        {
            // basic email format validation
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");
        }

        private bool IsValidPassword(string password)
        {
            // Basic password lenght validation
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 8;
        }
       
    }

    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private string _errorMessage;

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

    public class LoginResponse
    {
        public bool Success { get; set; }
    }
}
