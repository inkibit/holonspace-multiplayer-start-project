# holonspace-multiplayer-start-project

This template assumes that you have limited experience with Unity, Oculus and Github but perhaps have tried a little. It will allow you to create a multiplayer Holonspace and build it out to the Meta Quest 2 and play with other people. Give yourself a couple of hours to do this tutorial.

 This template comes pre-configured with Universal Render Pipeline (URP), Multiplayer Quest Template, Photon Pun2 and Photon voice 2, settings configuration and Holoncore. You will then need to install the Oculus Integration SDK version 42 I'm using Unity version 2020.3.30 which works well. You can try later versions but there may be incompatibilities. I recommend you try this one first. We are using a VR ready PC for this tutorial.

## Set up your Quest and Oculus developer account
- You can follow this handy guide from Adafruit: https://learn.adafruit.com/sideloading-on-oculus-quest/enable-developer-mode

## Install Unity
- Download and install Unity Hub and create a 'personal account' https://unity.com/download

![image](https://user-images.githubusercontent.com/1101918/203062776-2b1b60eb-4cd8-4273-855e-7662823521f6.png)

- Follow the setup instructions for creating a personal licence
- Navigate to the installs tab
- Install version 2020.3.30f1 from the archive of installs

![image](https://user-images.githubusercontent.com/1101918/203075619-af0b8672-f9f2-4a47-b78f-777761a4148e.png)

![image](https://user-images.githubusercontent.com/1101918/203075849-a68a7465-1bb5-400a-a6e0-6b90100e94d9.png)

![image](https://user-images.githubusercontent.com/1101918/203074593-5ddf234b-fd99-4271-a44f-734b2f9c05d0.png)

- Add **Android Build Support** modules to your install. If you haven't already done this when you installed the version, you can see it here
![image](https://user-images.githubusercontent.com/1101918/203076587-46255f8b-7917-4d5d-9f87-9d0aeeb765d8.png)

- Make sure your modules have all Android build modules installed - it won't build to Quest without these. 

![image](https://user-images.githubusercontent.com/1101918/203076844-68bea8a0-0480-46df-834b-4469975b55ca.png)



## Set up your project
- Create new repository from this template into your own Github account

![image](https://user-images.githubusercontent.com/1101918/203054573-bdd173c7-5cb7-4cd6-9b3f-d4b4e83418a1.png)
- Clone the repository to your computer (I find Github desktop fairly easy) https://desktop.github.com/)

![image](https://user-images.githubusercontent.com/1101918/203054434-a6057c96-2cb2-4bcb-8480-6e3434335b97.png)

- Add project to Unity Hub and open (set to Android from Dropdown) https://unity.com/download (make sure you've installed the correct version)
![image](https://user-images.githubusercontent.com/1101918/203054023-8878ced9-e405-48fa-9297-7068a8392ce3.png)

## Configuring project in Unity
- When opening click **Ignore** the warning about compilation errors - we'll fix these next 

![image](https://user-images.githubusercontent.com/1101918/203056155-78dab5e7-7a22-4851-bc7c-54fc707f6f57.png)
You'll see a lot of errors in the **Console** panel. That's ok. It's missing Oculus Integration

![image](https://user-images.githubusercontent.com/1101918/203059965-3edab5d4-4e68-4400-a217-8ea6a2cea6f1.png)

## Install Oculus Integration SDK Unity package
We'll be using an older version of Oculus Integration package as we know it is compatible with Holonspace. You can try other versions but it may be unreliable.
- Download v42.0 of the Oculus Integration package https://developer.oculus.com/downloads/package/unity-integration/42.0

- Import into project by going to Assets > Import Package > Custom package then navigate to the downloaded file
- 
![image](https://user-images.githubusercontent.com/1101918/203310662-5bd81ee7-b3e0-442c-86b1-b88d16997a35.png)

- Click **Import** again when you see the list of files (this part can take a while as it's a big package...)

![image](https://user-images.githubusercontent.com/1101918/203061168-caf4dfaa-c887-4439-90a7-c0788c7c674b.png)
- Click **Yes** when prompted to install latest OVR plugin (v1.74)

- Click **Cancel** when asked about using the OpenXR backend. 
- 
![image](https://user-images.githubusercontent.com/1101918/203063121-8ff93af9-7920-4838-8f53-9126d926b863.png)
- Click **Ok** on next screen.
- Click **Restart** when prompted.
- Click **Upgrade** when asked to upgrade Oculus Spatializer.
- Click **Restart** if prompted again - this time it should restart Unity.

## Checking we have no errors now
At this point you might be in a random voice demo scene that Oculus chose for us and see pink objects - that's ok. But you should be error free - there are some yellow warning messages in your Console which you can clear using the button on the top of the panel. If you have any red warning messages, you'll have to fix those before continuing. It may be a Unity version or computer issue. 

![image](https://user-images.githubusercontent.com/1101918/203064590-5d7b9db7-6220-480a-b0c6-12f78b3cf766.png)
- Close the packages window if it's still open. 

## Upgrading Oculus materials to URP. 
You may see some pink materials if you leave the project as it is right now. We need to upgrade all Oculus materials to the URP render pipeline to convert them. 
- Go to Edit > Render Pipeline > Universal Render Pipeline > Upgrade project materials to UniversalRP Materials
- Click **Proceed** when prompted about overwriting materials. You'll see some messages in the console that some materials weren't upgraded. You can clear those.

![image](https://user-images.githubusercontent.com/1101918/203065061-b985cdf3-80e1-41c5-a599-024397dc023f.png)

## Configuring Photon multiplayer
We'll need to make the project connect to Photon so that we can play with other people using an internet connection. 

- Sign up for a Photon account - https://www.photonengine.com/
- Go to the dashboard and **Create a new app**

![image](https://user-images.githubusercontent.com/1101918/203066508-72f15cb3-be3b-476f-8f35-066eac50f968.png)
- Select 'Pun' from dropdown and give it a name - ignore the url part. Click **Create**

![image](https://user-images.githubusercontent.com/1101918/203067080-2f35f555-09f7-46ae-9fd9-f7f103728447.png)
- In the new app - copy the App ID into clipboard. We'll need this in a moment in Unity

![image](https://user-images.githubusercontent.com/1101918/203067510-c4b9d724-039c-434b-997f-9eec0210572d.png)
- In Unity go to Window > Photon Unity Networking > Highlight server settings

![image](https://user-images.githubusercontent.com/1101918/203067968-a9cd08a2-bf52-4816-aa1a-49ac9a3e297d.png)
- Paste the app id into the 3 slots at the top

![image](https://user-images.githubusercontent.com/1101918/203068291-b32f5739-2e5c-4de8-be6f-fac46cc17ab2.png)


# Opening scene and testing that it's working
We have 2 scenes set up that you can test. let's see if it works...
- Find the Projects panel at the bottom of the screen 
- Navigate the file browser until you find Assets > Holoncore > Scenes 
- Open the **Room** scene and you will see something like this. (Your panels may be laid out a bit differently but you should still see the scene)

![image](https://user-images.githubusercontent.com/1101918/203069344-07064497-9dc9-425b-b4c6-ee2fce278b1c.png)
- Save the project Edit > Save Project or CTRL S on keyboard - to ensure all the settings are saved. 
- Hit play at the top of the window and it should connect first to 'inkibit connecting' screen then to the room. You'll know it's working when you see something like this and there are connected messages in the console.

![image](https://user-images.githubusercontent.com/1101918/203070403-16e392a5-ed73-4edc-97c7-7d36e1bcf3f2.png)

- Switch tabs to the **Scene** view at the top of the screen so you can see better. That pink blob on the floor in front of the objects is your head...

![image](https://user-images.githubusercontent.com/1101918/203070676-39dbbc82-fa78-42d4-a9fb-f6f6078b8caf.png)

# Putting holonspace onto the Quest
At this point we can go ahead and try to build an .apk file (the file Quest recognises)to the headset. 
- Press the play button at the top of the window again to stop it (that is a bit confusing..)
- Navigate to File > Build settings

![image](https://user-images.githubusercontent.com/1101918/203071294-a23a6378-7ce5-434b-a7ee-f25d891ec246.png)

- Click on **Player Settings** at the bottom of screen
- Rename the build - this is what will show up in your headset so worth customising. 

![image](https://user-images.githubusercontent.com/1101918/203073310-75382259-c42a-4a71-9b7e-fc2cf18e18c7.png)

- Close this window using the little x at the top right of the window (there is no save button)

- Notice that there is a 'lobby' and 'room' scenes added to the 'Scenes in build' section at the top. 
- Click on 'Build' at the bottom of the panel (If you have the Quest already connected via link cable, you can click 'Build an run' 

![image](https://user-images.githubusercontent.com/1101918/203072364-ec2f842e-6004-4ed7-bc7b-b2834388029e.png)

- Create a new folder called 'Builds' when you get prompted for a location and name your apk and click **save**

![image](https://user-images.githubusercontent.com/1101918/203072637-1dbd0ce5-02b8-43b9-bab2-a24813ceaa6a.png)

- Fingers crossed.. This part can take a upto 15 minutes for the first build because it has a lot of initiation of files and shaders.. if it's going to fail, it will fail very soon when it can't find some files it needs. You'll know. By the time it's compiling shaders you should be ok.
- Check that your console says **Build completed with a result of 'Succeeded'**

![image](https://user-images.githubusercontent.com/1101918/203078306-eb58701f-4d81-42d5-acde-cba189de58cf.png)

##Install in Quest using Sidequest
If you haven't already built out to the headset using a link cable then you can use sidequest to 'sideload' the apk to the headset.

- Run Sidequest desktop app
- Connect your Quest to the PC using a link cable. 
- Put on the headset and authorise the various windows that appear - take off headset
- In Sidequest - check that you have a 'green light' that shows you are connected

![image](https://user-images.githubusercontent.com/1101918/203081446-cf95a089-194f-4f69-8e01-852f4d259280.png)

- Install the apk to your headset using icon/link (the folder with down arrow)

![image](https://user-images.githubusercontent.com/1101918/203081847-606605b7-d4e8-4695-bf29-09eda1946185.png)


- Check you have no error messages when installing
- Put the headset back on and navigate to the apps menu
- Select 'Unknown sources' from the drop down menu on the top right of the apps window
- As long as you are connected to the internet, you should see the inkibit connecting screen then you will be in Holonspace. This is your own app version. 
- Send apk to other people or install to other headsets to play in the same Holonspace. 

##Notes
- When you reopen the project you might find you get a message about deleting old files. You can safely delete them. 

###
Customising Holonspace - You will always need lobby up there as the first screen but the next scene but you can add any new scene next and it will load - as long as you have the scene set up properly for Holonspace to work. If you're not sure, duplicate the room screen from the Holoncore folder and 


# Making Holons
- new URP instructions coming...
Get in touch at mafj.inkibit@rootinteractive.com to ask for old ones and we'll send you a link


