using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Plugin.AudioRecorder;

namespace ClientApp
{
    public partial class SecondPage : ContentPage
    {
        private readonly AudioRecorderService AudioRecorderService = new AudioRecorderService();

        public SecondPage()
        {
            InitializeComponent();

            
        }

        void OnButtonPressed (System.Object sender, System.EventArgs e)
        {
            if (AudioRecorderService.IsRecording)
            {
                AudioRecorderService.StopRecording();     
            }
            else
            {
                AudioRecorderService.StartRecording();
            }
        }

        //void OnButtonReleased (System.Object sender, System.EventArgs e)

        private async void OnButtonReleased (object sender, EventArgs e)
        {
            String audioFilePath = AudioRecorderService.GetAudioFilePath();

            await Navigation.PushAsync(new ThirdPage(audioFilePath));
        }

        //private async void Shuffle_OnClicked(object sender, EventArgs e)
       // {
            //await ;
       // }


    }


    

    }

