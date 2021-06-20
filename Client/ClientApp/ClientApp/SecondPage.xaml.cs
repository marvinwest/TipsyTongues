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

        private String[] sentences = { "Wrap rage, also called package rage, is the common name for heightened levels of anger and frustration resulting from the inability to open packaging, particularly some heat-sealed plastic blister packs and clamshells.", "You also took the fine jewelry I gave you, the jewelry made of my gold and silver, and you made for yourself male idols and engaged in prostitutionwith them.", "Bonsai Kitten was a hoax website that claimed to provide instructions on how to raise a kitten in a jar, so as to mold the bones of the kitten into the shape of the jar as the cat grows, like how a bonsai plant is shaped." };
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
