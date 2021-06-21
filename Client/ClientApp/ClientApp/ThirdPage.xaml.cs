using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.AudioRecorder;
using Newtonsoft.Json.Linq;
using Lottie.Forms;
using Xamarin.Forms;

namespace ClientApp
{

    public partial class ThirdPage : ContentPage
    {

        private readonly AudioPlayer audioPlayer;

        private String audioFilePath;
        private String sentence;

        public ThirdPage(String audioFilePath, String sentence)
        {
            audioPlayer = new AudioPlayer();
            this.audioFilePath = audioFilePath;
            this.sentence = sentence;
            InitializeComponent();
        }

        async void PlayRecording(object sender, EventArgs e)
        {
            try
            {
                audioPlayer.Play(audioFilePath);
            }
            catch (Exception)
            {
                await Navigation.PushAsync(new ErrorPage("Error loading audiofile"));
                Navigation.RemovePage(this);
            }
            
        }

        //TODO: use Navigation.RemovePage on every change of pages!!!
        private async void SecondPage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SecondPage());
            Navigation.RemovePage(this);
        }

        //TODO:
        //  - got to errorpage is statuscode else than 200 (OK)
        //  - Add Proper Exceptionhandling
        //  - Open next Page at the end of this Method, forward the Result of the "levelOfDrunkenness" via constructor
        //  - Show Loading animation while waiting for the Response (will take more time if full backend is developed and deployed)

        //Lottie:
        //Placeholder-File = Mercury_navigation_refresh.json TODO: replace by actual animation, wait for design
        //  - Android: Add to Assets-folder, set Build action to AndroidAsset in Properties
        //  - Apple: Add to to root clientapp.ios-folder, set Build action to BundleResource
        private async void PostToBackend_OnClicked(object sender, EventArgs e)
        {
            // Loads loadingPage at beginning of the Method
            ContentPage loadingPage = new LoadingPage();
            await Navigation.PushAsync(loadingPage);

            // defines the content of the payload that is forwarded to Backend
            MultipartFormDataContent content = new MultipartFormDataContent();
            String url = "https://tipsy-tongues.herokuapp.com/recognition/audio";

            try
            {
                // Reads Data from the audioFile into a bytearray
                // Adds the ByteArrayContent to the payload
                byte[] fileByteArray = File.ReadAllBytes(audioFilePath);
                var fileByteArrayContent = new ByteArrayContent(fileByteArray);
                content.Add(fileByteArrayContent, "audioFile", audioFilePath);
            }
            catch (ArgumentNullException ex)
            {
                // If microphone didn´t record a sound, the fileByteArray can´t be build
                // Therefore the user is forwarded to the errorpage
                Console.WriteLine(ex.Source);
                await Navigation.PushAsync(new ErrorPage("You did not record a message, therefore your Level of Drunkenness cannot be recognized."));
                Navigation.RemovePage(this);
            }

            // Builds StringContent from given sentence
            // Adds it to the Payload
            StringContent sentenceContent = new StringContent(sentence);
            content.Add(sentenceContent, "sentence");

            // Builds HTTPClient
            // Forwards the PostRequest with content-payload to backend
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            // writes content of the HTTPResponse to console
            // TODO: add guard here, which loads errorpage, if statuscode not OK
            Console.WriteLine(response.StatusCode.ToString());
            
            // Parse response to json-format
            // Extract and forward levelOfDrunkenness to the next Page
            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(responseBody);
            var levelOfDrunkenness = jsonObject.Value<int>("levelOfDrunkenness");

            // Forwards the returned levelOfDrunkenness to the Next Page
            // Closes this Page and the loadingpage
            await Navigation.PushAsync(new FourthPage(levelOfDrunkenness));
            Navigation.RemovePage(this);
            Navigation.RemovePage(loadingPage);
        }

    }

}