using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
        private const string BackendUrl = "https://crispygatewayservice.azurewebsites.net/api/post";

        public HomeViewModel ViewModel { get; set; }

        public HomePage()
        {
            this.InitializeComponent();
            ViewModel = new HomeViewModel();
            DataContext = ViewModel;
            /* LoadBlogPostsAsync(); // Load blog posts when the page is loaded*/



        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           await LoadBlogPostsAsync();

        }

        private async Task LoadBlogPostsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(BackendUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var blogPostResponse = JsonConvert.DeserializeObject<BlogPostResponse>(json);

                        // Populate the ObservableCollection with the deserialized blog posts
                        ObservableCollection<BlogPost> blogPosts = new ObservableCollection<BlogPost>(blogPostResponse.Results);
                        // ViewModel.BlogPosts = blogPosts;
                        ListViewJsonData.ItemsSource = blogPosts;
                        //ViewModel.BlogPosts = JsonConvert.DeserializeObject<ObservableCollection<BlogPost>>(json);
                        Console.WriteLine("Blog posts loaded successfully.");
                    }
                    else
                    {
                        // Handle error response
                        Console.WriteLine($"Failed to load blog posts. Status code: {response.StatusCode}");

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error: {ex.Message}");
            }
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

    public class BlogPostResponse
    {
        [JsonProperty("results")]
        public List<BlogPost> Results { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }

    public class HomeViewModel
    {

        public ObservableCollection<BlogPost> BlogPosts { get; set; }
    }


    public class BlogPost
    {
        public Guid Id { get; set; }
        public Guid Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public User User { get; set; }
        public int LikeCount { get; set; }
        public bool UserLiked { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
    }
}

