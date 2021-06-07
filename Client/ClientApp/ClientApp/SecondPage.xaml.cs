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
        private readonly AudioRecorderService AudioRecorderService = new AudioRecorderService();

       
        private String sentence;
        private String[] sentences = { "te", "qui", "la" };
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
             RandomizeSentence();
             BindingContext = this;
             InitializeComponent();
        }

        //mocked for now
        //TODO: add outputtext to represent sentence in SecondPage.xaml,
        //  Let textfield be generated via Method on loading of page and on new Sentence Button.
        //  method should take one sentence randomly out of an array of sentences.


        async void OnButtonPressed (System.Object sender, System.EventArgs e)
        {
            if (AudioRecorderService.IsRecording)
            {
                await AudioRecorderService.StopRecording();
            }
            else
            {
                await AudioRecorderService.StartRecording();
            }
        }

        

        private async void OnButtonReleased (object sender, EventArgs e)
        {
            String audioFilePath = AudioRecorderService.GetAudioFilePath();
            
            //delete this mocked sentence when outputtext is implemented, see TODO above
            this.sentence = "I am a mocked Sentence for now";
            await Navigation.PushAsync(new ThirdPage(audioFilePath, sentence));
        }


        private void RandomizeSentence()

        {
            
            Random rand = new Random();
            int index = rand.Next(sentences.Length);

           
            Sentence = sentences[index];

            

        }

        void Shuffle_Clicked(System.Object sender, System.EventArgs e)
        {
            RandomizeSentence();
        }
        

    


    

    }

}
