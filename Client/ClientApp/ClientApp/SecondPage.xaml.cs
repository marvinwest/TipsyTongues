﻿using System;
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
            "I am not the fig plucker nor the fig pluckers son but I will pluck figs till the fig plucker comes",
        "The thirty-three thieves thought that they thrilled the throne throughout Thursday",
        "Six sick hicks nick six slick bricks with picks and sticks",
        "Gobbling gorgoyles gobbled gobbling goblins",
        "Green glass globes glow greenly",
         "Four furious friends fought for the phone",
        "Two tiny tigers take two taxis to town",
        "Shut up the shutters and sit in the shop",
        "When you write copy you have the right to copyright the copy you write",
        "A big black bug bit a big black dog on his big black nose",
        "How many cookies could a good cook cook If a good cook could cook cookies?",
        "Thirty-three thirsty, thundering thoroughbreds thumped Mr. Thurber on Thursday",
        "Roofs of mushrooms rarely mush too much",
        "Roofs of mushrooms rarely mush too much",
        "The great Greek grape growers grow great Greek grapes",
        "I wish to wash my Irish wristwatch",
        "On a lazy laser raiser lies a laser ray eraser",
        "Wow, race winners really want red wine right away!",
        "The ruddy widow really wants ripe watermelon and red roses when winter arrives",
        "If you're keen on stunning kites and cunning stunts, buy a cunning stunning stunt kite",
        "I slit a sheet, a sheet I slit, upon a slitted sheet I sit",
        "Whoever slit the sheets is a good sheet slitter",
        "Crush grapes, grapes crush, crush grapes"};
        private String[] softModeSentences = {
        "A toast to those who wish me well and all the rest can go to hell",
        "Here is to doing and drinking not sitting and thinking",
        "Drinks are on the house so someone get a ladder",
        "Nothing uses up alcohol faster than political argument",
        "Cheap booze is a false economy.",
        "There are worse ways to die than warm and drunk",
        "The longest common word with all the letters in alphabetical order is 'almost'",
        "Americans spend more time watching other people on television cook than they do cooking themselves",
        "According to legend, cats were created when a lion on Noah’s ark sneezed and two kittens came out",
        "Beaver eyelids are transparent so that they can see through them as they swim underwater",
        "The neighbors of lottery winners are more likely to go bankrupt because they try to keep up with their neighbor’s new spending habits",
        "Believing in Santa Claus cultivates a child's imagination and the ability to think of possibilities and potentialities",
        "The term 'lawn mullet' means having a neatly manicured front yard and an unmowed mess in the back",
        "Even though the blue whale is the largest animal on earth, it can’t swallow anything bigger than a beach ball",
        "People weigh less if they stand at the equator than if they stand at the North or South poles",
        "White-faced capuchin monkeys greet each other by sticking their fingers up each others’ noses",
        "I like to have a martini,Two at the very most.After three I'm under the table,after four I'm under my host",
        "First you take a drink, then the drink takes a drink, then the drink takes you",
        "I went to the worst of bars hoping to get killed but all I could do was to get drunk again",
        "Always do sober what you said you'd do drunk. That will teach you to keep your mouth shut",
        "You never know what a wild cow's defenses are. They may spit blood, or even venomous toxins into your eyes in order to disable you. Once disabled, they may try to eat your internal organs",
        "Bonsai Kitten was a hoax website that claimed to provide instructions on how to raise a kitten in a jar, so as to mold the bones of the kitten into the shape of the jar as the cat grows, like how a bonsai plant is shaped"};
        private String[] sentences;
        private String sentence;

        private String modeButtonText;

        private bool isHardMode;

        private ElementSizeService elementSizeService;

        private Double frameHeight;
        private Double frameWidth;

        private Double secondRowHeight;
        private Double modeButtonWidth;
        private Double modeButtonHeight;
        private Double shuffleButtonWidth;

        private Double recordingButtonHeight;

        public SecondPage()
        {
            audioRecorderService = new AudioRecorderService();

            elementSizeService = new ElementSizeService();
            Sentences = softModeSentences;
            modeButtonText = "EASY";
            isHardMode = false;
            RandomizeSentence();

            FrameHeight = elementSizeService.calculateElementHeight(0.5);
            FrameWidth = elementSizeService.calculateElementWidth(0.9);

            secondRowHeight = elementSizeService.calculateElementHeight(0.075);
            modeButtonWidth = elementSizeService.calculateElementWidth(0.2);
            modeButtonHeight = elementSizeService.calculateElementHeight(0.07);
            ShuffleButtonWidth = elementSizeService.calculateElementWidth(0.15);

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
                ModeButtonText = "EASY";
                RandomizeSentence();
            }
            else
            {
                isHardMode = true;
                Sentences = hardModeSentences;
                ModeButtonText = "HARD";
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

        public Double ModeButtonHeight
        {
            get { return modeButtonHeight; }
            set { modeButtonHeight = value; }
        }

    }

}