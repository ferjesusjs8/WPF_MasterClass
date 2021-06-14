using LandmarkAI.Classes;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void selectImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png *.jpg)|*.png;*.jpg;*.jpeg;|All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;

                await MakePredictionAsync(fileName);
                selectedImage.Source = new BitmapImage(new Uri(fileName));
            }
        }

        private async Task MakePredictionAsync(string fileName)
        {
            string url = string.Empty;
            string prediction_key = string.Empty;
            string content_type = string.Empty;
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LandmarkAI.txt");

            using (var reader = new StreamReader(path))
            {
                var text = await reader.ReadToEndAsync();

                var @params = text.Split(";");
                url = @params[0];
                prediction_key = @params[1];
                content_type = @params[2];
            }

            var file = File.ReadAllBytes(fileName);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Prediction-Key", prediction_key);

                using (var content = new ByteArrayContent(file))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(content_type);
                    var response = await httpClient.PostAsync(url, content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    List<Prediction> predictions = JsonConvert.DeserializeObject<CustomVision>(responseString).Predictions;

                    resultsListView.ItemsSource = predictions;
                }
            }
        }

        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }
    }
}