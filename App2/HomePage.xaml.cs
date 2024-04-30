using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            ViewModel = new HomeViewModel();
            DataContext = ViewModel;
            LoadBlogPosts(); // Load blog posts when the page is loaded


        }
        public HomeViewModel ViewModel { get; set; }

        private void LoadBlogPosts()
        {
            // Simulate loading blog posts from a database or API
            ObservableCollection<BlogPost> blogPosts = new ObservableCollection<BlogPost>
            {
                new BlogPost { Title = "Introduction to Blogging", Author = "John Doe", Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio." },
                new BlogPost { Title = "Tips for Successful Blogging", Author = "Jane Smith", Content = "Pellentesque cursus mauris sit amet sapien fermentum, vitae convallis tortor ultricies." },
                new BlogPost { Title = "The Future of Blogging", Author = "Alex Johnson", Content = "Vivamus blandit odio non fermentum suscipit. Donec tristique, purus nec vestibulum dapibus." }
            };

            ViewModel.BlogPosts = blogPosts;
        }

        private void BlogPost_Click(object sender, ItemClickEventArgs e)
        {
            Console.WriteLine("helloo");

            // Get the clicked blog post
            BlogPost clickedPost = e.ClickedItem as BlogPost;

            // Navigate to the blog post page and pass the clicked blog post as a parameter
            Frame.Navigate(typeof(BlogPostPage), clickedPost);
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

    public class HomeViewModel
    {
        public ObservableCollection<BlogPost> BlogPosts { get; set; }
    }

    public class BlogPost
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
}