# TipsyTongues
An app, that guesses your alcohol level based on a voice recording.

Design:
- Prototype created in InVision Studios. Link to project: https://tabeagraszynski837873.invisionapp.com/prototype/Tipsy-Tongues-ckpfppisd0052vn01kvzsjlsz/play/5390a90f
- Design choices: Dark Design, to be enjoyable at night; Disclaimer when app is started; Beer scheme --> yellow as primary color; Tongues scheme: --> pink as secondary color
- Disclaimer: resembles color and waves of a beer getting poured in; warns user of drinking and driving
- Record button: Tongue wrapped around a microphone. Illustration made in Adobe Illustrator.
- Different sentences to read out loud, shuffle button to get a new text; Easy and hard mode; Hard mode with tongue twisters
- Listening button: Headphones that resemble a tongue. Illustration made in Adobe Illustrator. 
- Loading animation: Beer glass filling up. Animation made in Adobe AfterEffects, exported as lottiefile
- Results: Results are shown in a Scale in the shape of a beer glass. The fuller the glass is, the more drunk is the user; Caption below that states the result; Home button to go back to the Disclaimer
- Loading animation: https://lottiefiles.com/share/mjtwhdlq
- 15 seconds circle animation for recording: https://lottiefiles.com/share/ftbiikqh (with button), https://lottiefiles.com/share/wusiein9 (without button)


Front-End:
- cross compiled -> runs across multiple platforms (IOS, Android, Microsoft (opt.))
- Xamarin (C#)
- Visual Studio as IDE
- .Forms as the open source UI framework in XAML with CodeBehind in C#

Code Replication Steps: 
  - download the installer for Visual Studio IDE Community Version (https://visualstudio.microsoft.com/de/downloads/)
    - Setup for Windows:
      - double-click bootstrapper in your downloads: vs_community.exe, if you receive a User Account Control notice, choose Yes
      - acknowledge the Microsoft License Terms and the Microsoft Privacy Statement
      - Workloads: select Xamarin Mobile development with .NET (If VS is already installed, add Xamarin by re-running the Visual Studio installer to modify workload
    - Setup for Mac: 
      - click the VisualStudioforMacInstaller.dmg to mount the installer, then run it by double-clicking the arrow logo
      - acknowledge the privacy and license terms
      - Workloads: select .NET Core, Android and IOS 
      - install
      - sign in with Microsoft, choose your preferences
  - download ClientApp from the Repository and run our code 



Back-End:
- Uses Python Flask for serverside developing
- Uses Azure Cognitive Services pronunciation recognition to compare given sentence to audiofile
- Return Level of drunkenness based on the comparison

Code Replication Steps:
  - Install python 3
  - download Server from the repository
  - Add a python virtual environment (venv) in the Server-folder
  - Activate the virtual environment
  - Install packages in requirements.txt (pip install)
  - Following you need access to Azure Cognitive Services
  - Add a file named server_keys.py with public attributes to Server-folder:
      - authorization_key = "your authorization key to use in POST header"
      - azure_service_key = "your azure service key"
      - azure_service_region = "your azure service region"
      - frontend_testing_key = "your frontend testing key"
  - Run app.py to start the server locally
 

Designer: Tabea Graszynski</br>
Front-End: Jule Emily Buschmann, Marvin Westphal</br>
Back-End: Marvin Westphal</br>
