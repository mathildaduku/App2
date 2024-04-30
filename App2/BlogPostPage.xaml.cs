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
    public sealed partial class BlogPostPage : Page
    {
        public BlogPostPage()
        {
            this.InitializeComponent();
        }

        // Initialize the page with the blog post passed as parameter
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null && e.Parameter is BlogPost)
            {
                BlogPost post = e.Parameter as BlogPost;
                DataContext = post; // Set the DataContext to the blog post to bind it to UI elements
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
    }
}
