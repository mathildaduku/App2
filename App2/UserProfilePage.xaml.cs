using System;
using System.Collections.Generic;
using System.ComponentModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserProfilePage : Page
    {
        public UserProfilePage()
        {
            this.InitializeComponent();
            DataContext = new UserProfileViewModel(); // Set DataContext to ViewModel
        }

        private void ViewFollowers_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the page showing the list of followers
            Frame.Navigate(typeof(FollowersPage));
        }

        private void ViewFollowing_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the page showing the list of users being followed
            Frame.Navigate(typeof(FollowingPage));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the previous page
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }

    public class UserProfileViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private string _email;
        private int _followersCount;
        private int _followingCount;

        // User's username
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        // User's email address
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        // Number of followers
        public int FollowersCount
        {
            get { return _followersCount; }
            set
            {
                _followersCount = value;
                OnPropertyChanged(nameof(FollowersCount));
            }
        }

        // Number of users being followed
        public int FollowingCount
        {
            get { return _followingCount; }
            set
            {
                _followingCount = value;
                OnPropertyChanged(nameof(FollowingCount));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //  populate the properties with real user data later 
        public UserProfileViewModel()
        {
            // Sample data
            UserName = "JohnDoe";
            Email = "johndoe@example.com";
            FollowersCount = 500;
            FollowingCount = 300;
        }
    }
}

