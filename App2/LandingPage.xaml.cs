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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LandingPage : Page
    {
        public LandingPage()
        {
            this.InitializeComponent();
        }

        private void ExploreButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the home page to explore blogs
            Frame.Navigate(typeof(HomePage));
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the sign-up page
            Frame.Navigate(typeof(RegisterPage));
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the sign-in page
            Frame.Navigate(typeof(MainPage));
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the user profile page
            Frame.Navigate(typeof(UserProfilePage));
        }
    }
}
