using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.AudioRecorder;
using Xamarin.Forms;

namespace ClientApp
{

    public partial class ThirdPage : ContentPage
    {

        private readonly AudioPlayer audioPlayer = new AudioPlayer();

        private String audioFilePath;
        private String sentence;


        public ThirdPage(String audioFilePath, String sentence)
        {
            InitializeComponent();
            this.audioFilePath = audioFilePath;
            this.sentence = sentence;
        }

        void PlayRecording(object sender, EventArgs e)
        {
            audioPlayer.Play(audioFilePath);
        }

        private async void SecondPage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SecondPage());
        }
    }
}