using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Plugin.AudioRecorder;
using Plugin.Permissions;

namespace ClientApp
{
    public partial class SecondPage : ContentPage
    {
        private readonly AudioRecorderService AudioRecorderService;

        private String[] sentences = { "te", "qui", "la" };
        private String sentence;
        public String Sentence
        {
            get { return sentence; }
            set
            {
                sentence = value;
                OnPropertyChanged("Sentence");
            }
        }

        public SecondPage()
        {
            AudioRecorderService = new AudioRecorderService();
            RandomizeSentence();
            BindingContext = this;
            InitializeComponent();
        }

       

        async void OnButtonPressed (System.Object sender, System.EventArgs e)
        {
            await AudioRecorderService.StartRecording();
        }

        

        private async void OnButtonReleased (object sender, EventArgs e)
        {
            await AudioRecorderService.StopRecording();
            String audioFilePath = AudioRecorderService.GetAudioFilePath();

            await Navigation.PushAsync(new ThirdPage(audioFilePath, sentence));
            Navigation.RemovePage(this);
        }


        private void RandomizeSentence()
        {
            Random rand = new Random();
            int index = rand.Next(sentences.Length);
            Sentence = sentences[index];
        }

        void Shuffle_OnClicked(object sender, EventArgs e)
        {
            RandomizeSentence();
        }

    }

}
