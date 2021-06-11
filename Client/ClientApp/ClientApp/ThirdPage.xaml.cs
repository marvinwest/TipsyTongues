﻿using System;
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
            ContentPage loadingPage = new LoadingPage();
            await Navigation.PushAsync(loadingPage);

            MultipartFormDataContent content = new MultipartFormDataContent();
            String url = "https://tipsy-tongues.herokuapp.com/recognition/audio";


            byte[] fileByteArray = File.ReadAllBytes(audioFilePath);
            var fileByteArrayContent = new ByteArrayContent(fileByteArray);
            content.Add(fileByteArrayContent, "audioFile", audioFilePath);

            StringContent sentenceContent = new StringContent(sentence);
            content.Add(sentenceContent, "sentence");

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            //just to check wether the Response is correct for now
            // writes content of the HTTPResponse to console
            // add guard here, which loads errorpage, if statuscode not OK
            Console.WriteLine(response.StatusCode.ToString());
            
            //parse response to json, then forward levelOfDrunkenness to next Page
            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(responseBody);
            var levelOfDrunkenness = jsonObject.Value<int>("levelOfDrunkenness");

            await Navigation.PushAsync(new FourthPage(levelOfDrunkenness));
            Navigation.RemovePage(this);
            Navigation.RemovePage(loadingPage);
        }

    }

}