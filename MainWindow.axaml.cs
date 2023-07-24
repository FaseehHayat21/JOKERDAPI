using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls.Primitives;
using System;

namespace JOKERDAPI
{
    public partial class MainWindow : Window
    {
        public static TextBox Tofind;
        public static RandomUser real;
        public static RandomJoke areal;
        public static WordDetails wreal;
        public MainWindow()
        {
            int reaction = 0;
            string examples = "Examples";
            Tofind = new TextBox();
            real = new RandomUser();
            areal = new RandomJoke();
            wreal = new WordDetails();
            InitializeComponent();
        }
        public async void loginbtn(object sender, RoutedEventArgs e)
        {
            displayprofile.IsVisible = false;
            displayprofile.IsEnabled = false;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync("https://randomuser.me/api/");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response and extract the user's name
                        var randomUser = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
                        real.FirstName = randomUser.results[0].name.first;
                        real.LastName = randomUser.results[0].name.last;
                        real.Gender = randomUser.results[0].gender;
                        real.Country = randomUser.results[0].location.country;
                        real.Email = randomUser.results[0].email;
                        real.DobDate = randomUser.results[0].dob.date;
                        real.DobAge = randomUser.results[0].dob.age;
                        real.Phone = randomUser.results[0].phone;
                        usernamebox.Text = real.FirstName + "  " + real.LastName;

                        gender.Content = real.Gender;
                        email.Content = real.Email;
                        username.Content = real.FirstName + real.LastName;
                        login.IsVisible = false;
                        login.IsEnabled = false;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                login.IsVisible = true;
                login.IsEnabled = true;
            }
        }
        public async void generatebtn(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync("https://official-joke-api.appspot.com/random_joke");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var randomJoke = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
                        areal.setup = randomJoke.setup;
                        areal.punchline = randomJoke.punchline;
                        jokebox.Text = areal.setup + "\n" + areal.punchline;

                    }
                }

            }
            catch (HttpRequestException ex)
            {
                login.IsVisible = true;
                login.IsEnabled = true;
            }
        }

        public async void searchbtn(object sender, RoutedEventArgs e)
        {

            string wasif = wordbox.Text;

            if (searchbox.Text.Contains(' '))
            { searchbox.Text = "Dont include Space in it okkkkkkk"; }
            else
            {
                string apiKey = "Gowkurw5YaGw2xkQARxM3A==lhtiOh6Bw3BoaSFp";
                string apis = "https://api.api-ninjas.com/v1/thesaurus?word=" + searchbox.Text;
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
                        HttpResponseMessage response = await httpClient.GetAsync(apis);
                        if (response.IsSuccessStatusCode)
                        {
                            
                            string json = await response.Content.ReadAsStringAsync();


                            var randomUser = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

                                        foreach (string synonym in randomUser.synonyms)
                            {
                                wordbox.Text = (wordbox.Text + "\n" + synonym);
                            }
                       


                        }
                        else
                        {
                            wordbox.Text = "Network Error! ";
                            // UserNameTextBlock.Text = "Failed to fetch data.";
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    wordbox.Text = "Network Error! ";
                    // UserNameTextBlock.Text = $"Error fetching data: {ex.Message}";
                }
            }

  
        }


        public void logoutbtn(object sender, RoutedEventArgs e)
        {
            login.IsVisible = true;
            login.IsEnabled = true;
        }
        public void profilebtn(object sender, RoutedEventArgs e)
        {
            login.IsVisible = false;
            login.IsEnabled = false;
            displayprofile.IsVisible = true;
            displayprofile.IsEnabled = true;
        }
        public void backbtn(object sender, RoutedEventArgs e)
        {
            displayprofile.IsVisible = false;
            displayprofile.IsEnabled = false;
        }

    }
    public class RandomUser
    {
        public string Gender { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Postcode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimezoneOffset { get; set; }
        public string TimezoneDescription { get; set; }
        public string Email { get; set; }
        public string Uuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Md5 { get; set; }
        public string Sha1 { get; set; }
        public string Sha256 { get; set; }
        public DateTime DobDate { get; set; }
        public int DobAge { get; set; }
        public DateTime RegisteredDate { get; set; }
        public int RegisteredAge { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string IdName { get; set; }
        public string IdValue { get; set; }
        public string PictureLarge { get; set; }
        public string PictureMedium { get; set; }
        public string PictureThumbnail { get; set; }
        public string Nat { get; set; }
    }
    public class RandomJoke
    {
        public string setup { get; set; }
        public string punchline { get; set; }
    }
    public class WordDetails
    {
        public string Word { get; set; }
        public string Pronunciation { get; set; }
        public string[] Definitions { get; set; }
        public string[] Synonyms { get; set; }
        public string[] Antonyms { get; set; }
    }
}