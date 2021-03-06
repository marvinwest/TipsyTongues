using System;
using System.IO;
using System.Net.Http;

using Plugin.AudioRecorder;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ClientApp
{

    /**
     * ListeningPage:
     * The user can listen to his recording by clicking the button in the middle of the page.
     * By click on "Try again" the RecordingPage is loaded
     * By click on "Go" languageCode, authorization, sentence, audiofile and the audiofiledetails are forwarded to the server.
     **/
    public partial class ListeningPage : ContentPage
    {
        private static String LANGUAGE_CODE = "en-US";
        private static String AUTHORIZATION = "12345678";

        private readonly AudioPlayer audioPlayer;
        private AudioStreamDetails audioStreamDetails;

        private String audioFilePath;
        private String sentence;

        private ElementSizeService elementSizeService;

        private double playButtonHeight;
        private double playButtonWidth;

        private double menuButtonHeight;
        private double menuButtonWidth;

        /**
         * On Initilization:
         * Forwarded elements audioFilePath, sentence and audioStreamDetails are set.
         * AudioPlayer and ElementSizeService are loaded.
         * Width and Height of the shown elements are calculated.
         **/
        public ListeningPage(String audioFilePath, String sentence, AudioStreamDetails audioStreamDetails)
        {
            this.audioFilePath = audioFilePath;
            this.sentence = sentence;
            this.audioStreamDetails = audioStreamDetails;

            audioPlayer = new AudioPlayer();
            elementSizeService = new ElementSizeService();

            playButtonHeight = elementSizeService.calculateElementHeight(0.4);
            playButtonWidth = elementSizeService.calculateElementWidth(0.4);
            menuButtonHeight = elementSizeService.calculateElementHeight(0.1);
            menuButtonWidth = elementSizeService.calculateElementWidth(0.4);

            BindingContext = this;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /**
         * On click the recorded audiofile is played.
         **/
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

        /**
         * On click playing of the audiofile is stopped and the RecordingPage is loaded.
         **/
        private async void RecordingPage_OnClicked(object sender, EventArgs e)
        {
            audioPlayer.Pause();
            await Navigation.PushAsync(new RecordingPage());
            Navigation.RemovePage(this);
        }

        /**
         * On click:
         * Playing of the audiofile is stopped.
         * The loadingPage is shown.
         * MultipartFormDataContent for the request is build.
         * If the Data can be set correctly the Request is forwarded to the backend-server.
         * 
         * On response:
         * If the Statuscode of the response is successful (200), the levelOfDrunkenness is extracted from it.
         * LoadingPage and RecordingPage are closed.
         * Resultpage is loaded, levelOfDrunkenness is forwarded.
         * If an error occurs or the statuscode is not successful the ErrorPage is loaded.
         **/
        private async void PostToBackend_OnClicked(object sender, EventArgs e)
        {
            audioPlayer.Pause();

            MultipartFormDataContent content = new MultipartFormDataContent();
            try
            {
                // Reads Data from the audioFile into a bytearray
                // Adds the ByteArrayContent to the payload
                byte[] fileByteArray = File.ReadAllBytes(audioFilePath);
                var fileByteArrayContent = new ByteArrayContent(fileByteArray);
                content.Add(fileByteArrayContent, "audioFile", audioFilePath);
            }
            catch (ArgumentNullException)
            {
                // If microphone didn´t record a sound, the fileByteArray can´t be build
                // Therefore the user is forwarded to the errorpage
                await Navigation.PushAsync(new ErrorPage("You did not record a message, therefore your Level of Drunkenness cannot be recognized."));
                Navigation.RemovePage(this);
            }

            // Loads loadingPage at beginning of the Method
            ContentPage loadingPage = new LoadingPage();
            await Navigation.PushAsync(loadingPage);

            // defines the content of the payload that is forwarded to Backend
            String url = "https://tipsy-tongues.herokuapp.com/recognition/audio";

            // Builds content from given sentence and languageCode
            // Adds it to the Payload
            StringContent sentenceContent = new StringContent(sentence);
            content.Add(sentenceContent, "sentence");
            StringContent languageCodeContent = new StringContent(LANGUAGE_CODE);
            content.Add(languageCodeContent, "languageCode");

            // Build content from given AudioStreamDetails
            // Adds it to the payload
            StringContent audioChannelCountContent = new StringContent(audioStreamDetails.ChannelCount.ToString());
            content.Add(audioChannelCountContent, "audioChannelCount");
            int bytesPerSample = audioStreamDetails.BitsPerSample / 8;
            StringContent bytesPerSampleContent = new StringContent(bytesPerSample.ToString());
            content.Add(bytesPerSampleContent, "audioBytesPerSample");
            StringContent sampleRateContent = new StringContent(audioStreamDetails.SampleRate.ToString());
            content.Add(sampleRateContent, "audioSampleRate");

            // Builds HTTPClient
            // Forwards the PostRequest with content-payload to backend
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("authorization", AUTHORIZATION);
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            // writes content of the HTTPResponse to console
            // TODO: add guard here, which loads errorpage, if statuscode not OK
            if (response.IsSuccessStatusCode)
            {
                // Parse response to json-format
                // Extract and forward levelOfDrunkenness to the next Page
                var responseBody = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseBody);
                var levelOfDrunkenness = jsonObject.Value<int>("levelOfDrunkenness");

                // Forwards the returned levelOfDrunkenness to the Next Page
                // Closes this Page and the loadingpage
                await Navigation.PushAsync(new ResultPage(levelOfDrunkenness));
                Navigation.RemovePage(loadingPage);
                Navigation.RemovePage(this);
            }
            else
            {
                await Navigation.PushAsync(new ErrorPage("Service currently unavailable"));
                Navigation.RemovePage(loadingPage);
                Navigation.RemovePage(this);
            }
            
        }

        // Following are the properties for valuebinding in the XAML.

        public Double PlayButtonWidth
        {
            get { return playButtonWidth; }
            set { playButtonWidth = value; }
        }

        public Double PlayButtonHeight
        {
            get { return playButtonHeight; }
            set { playButtonHeight = value; }
        }

        public Double MenuButtonWidth
        {
            get { return menuButtonWidth; }
            set { menuButtonWidth = value; }
        }

        public Double MenuButtonHeight
        {
            get { return menuButtonHeight; }
            set { menuButtonHeight = value; }
        }

    }

}