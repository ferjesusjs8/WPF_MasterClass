using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void speechButton_Click(object sender, RoutedEventArgs e)
        {
            string key = string.Empty;
            string location = string.Empty;
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EvernoteClone.txt");

            using (var reader = new StreamReader(path))
            {
                var text = await reader.ReadToEndAsync();

                var @params = text.Split(";");
                key = @params[0].Split("=")[1];
                location = @params[1].Split("=")[1];
            }

            var speechConfiguration = SpeechConfig.FromSubscription(key, location);

            using (var audioConfiguration = AudioConfig.FromDefaultMicrophoneInput())
            {
                using (var recognizer = new SpeechRecognizer(speechConfiguration, audioConfiguration))
                {
                    var result = await recognizer.RecognizeOnceAsync();

                    contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
                }
            }
        }

        private void contentRichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int amountOfCharacters =
                (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd))
                    .Text
                    .Length;

            statusTextBlock.Text = $"Document lenght: {amountOfCharacters} characters";
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            bool isToggleButtonChecked = (sender as ToggleButton)?.IsChecked ?? false;

            if (isToggleButtonChecked)
                contentRichTextBox
                    .Selection
                    .ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTextBox
                    .Selection
                    .ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
        }

        private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = contentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);

            boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && selectedWeight.Equals(FontWeights.Bold);
        }
    }
}