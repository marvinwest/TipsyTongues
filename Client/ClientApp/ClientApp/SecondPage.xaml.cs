﻿using System;
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

        //mocked for now
        //TODO: add outputtext to represent sentence in SecondPage.xaml,
        //  Let textfield be generated via Method on loading of page and on new Sentence Button.
        //  method should take one sentence randomly out of an array of sentences.
        private String sentence;
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
            
            //delete this mocked sentence when outputtext is implemented, see TODO above
            this.sentence = "I am a mocked Sentence for now";
            await Navigation.PushAsync(new ThirdPage(audioFilePath, sentence));
        }

    }
}