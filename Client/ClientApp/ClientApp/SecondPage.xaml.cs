using System;
using System.Timers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.IO;
using System.Diagnostics;
using Plugin.AudioRecorder;
using Plugin.Permissions;

//{ "Wrap rage, also called package rage, is the common name for heightened levels of anger and frustration resulting from the inability to open packaging", "You also took the fine jewelry I gave you, the jewelry made of my gold and silver, and you made for yourself ", "Bonsai Kitten was a hoax website that claimed to provide instructions on how to raise a kitten in a jar, so as to mold the bones of the kitten into the shape of the jar " };
namespace ClientApp
{
    public partial class SecondPage : ContentPage
    {

        private static Timer recordingTimer;
        private AudioRecorderService audioRecorderService;

        private String[] hardModeSentences = {
            "Mister Tongue Twister tried to train his tongue to twist and turn and twit and twat to learn the letter T",
            "I am not the fig plucker nor the fig pluckers son but I will pluck figs till the fig plucker comes"};
        private String[] softModeSentences = {
            "A toast to those who wish me well and all the rest can go to hell",
            "Here is to doing and drinking not sitting and thinking",
            "Drinks are on the house so someone get a ladder",
            "Nothing uses up alcohol faster than political argument",
            "Cheap booze is a false economy.",
            "There are worse ways to die than warm and drunk."};
        private String[] sentences;
        private String sentence;

        private String modeButtonText;

        private bool isHardMode;

        private ElementSizeService elementSizeService;

        private Double frameHeight;
        private Double frameWidth;

        private Double secondRowHeight;
        private Double modeButtonWidth;
        private Double shuffleButtonWidth;

        private Double recordingButtonHeight;

        public SecondPage()
        {
            audioRecorderService = new AudioRecorderService();

            elementSizeService = new ElementSizeService();
            Sentences = softModeSentences;
            modeButtonText = "Hard";
            isHardMode = false;
            RandomizeSentence();

            FrameHeight = elementSizeService.calculateElementHeight(0.5);
            FrameWidth = elementSizeService.calculateElementWidth(0.9);

            secondRowHeight = elementSizeService.calculateElementHeight(0.075);
            ModeButtonWidth = elementSizeService.calculateElementWidth(0.2);

            RecordingButtonHeight = elementSizeService.calculateElementHeight(0.2);
            BindingContext = this;
            InitializeComponent();
  
            NavigationPage.SetHasNavigationBar(this, false);
        }

       

        async void OnButtonPressed (Object sender, EventArgs e)
        {
            recordingTimer = new Timer(14999);
            recordingTimer.Elapsed += new ElapsedEventHandler(OnRecordingTimeOut);
            recordingTimer.Enabled = true;
            await audioRecorderService.StartRecording();
        }

        private async void OnRecordingTimeOut(object sender, ElapsedEventArgs e)
        {
            await Task.Run(() => Device.BeginInvokeOnMainThread(() => OnButtonReleased(sender, e)));
        }

        private async void OnButtonReleased (object sender, EventArgs e)
        {
            await audioRecorderService.StopRecording();

            String audioFilePath = audioRecorderService.GetAudioFilePath();
            AudioStreamDetails audioStreamDetails = audioRecorderService.AudioStreamDetails;

            await Navigation.PushAsync(new ThirdPage(audioFilePath, sentence, audioStreamDetails));
            Navigation.RemovePage(this);
        }

        void Shuffle_OnClicked(object sender, EventArgs e)
        {
            RandomizeSentence();
        }

        void ChangeMode_OnClicked(object sender, EventArgs e)
        {
            if (isHardMode)
            {
                isHardMode = false;
                Sentences = softModeSentences;
                ModeButtonText = "Hard";
                RandomizeSentence();
            }
            else
            {
                isHardMode = true;
                Sentences = hardModeSentences;
                ModeButtonText = "Soft";
                RandomizeSentence();
            }
        }

        private void RandomizeSentence()
        {
            Random rand = new Random();
            int index = rand.Next(sentences.Length);
            Sentence = sentences[index];
        }


        public String Sentence
        {
            get { return sentence; }
            set
            {
                sentence = value;
                OnPropertyChanged("Sentence");
            }
        }

        public String[] Sentences
        {
            get { return sentences; }
            set
            {
                sentences = value;
                OnPropertyChanged("Sentences");
            }
        }

        public String ModeButtonText
        {
            get { return modeButtonText; }
            set 
            {
                modeButtonText = value;
                OnPropertyChanged("ModeButtonText");
            }
        }

        public Double FrameHeight
        {
            get { return frameHeight; }
            set { frameHeight = value; }
        }

        public Double FrameWidth
        {
            get { return frameWidth; }
            set { frameWidth = value; }
        }

        public Double SecondRowHeight
        {
            get { return secondRowHeight; }
            set { secondRowHeight = value; }
        }

        public Double ModeButtonWidth
        {
            get { return modeButtonWidth; }
            set { modeButtonWidth = value; }
        }

        public Double ShuffleButtonWidth
        {
            get { return shuffleButtonWidth; }
            set { shuffleButtonWidth = value; }
        }

        public Double RecordingButtonHeight
        {
            get { return recordingButtonHeight; }
            set { recordingButtonHeight = value; }
        }

    }

}