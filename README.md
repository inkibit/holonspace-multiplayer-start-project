# holonspace-multiplayer-start-project

Starting from a template
This template comes pre-configured with Universal Render Pipeline (URP), Multiplayer Quest Template, settings configuration and Holoncore. You will then need to install some packages. I'm using Unity version 2020 (LTS)

- Clone this repository (I find Github desktop fairly easy) https://desktop.github.com/
- Add project to Unity Hub (set to Android from Dropdown) https://unity.com/download
- Open Unity project

## Oculus & Sidequest
- Set up your Oculus ecosystem and sidequest https://sidequestvr.com/setup-howto
- Download and import Oculus Integration Unity Package (it's big) https://developer.oculus.com/downloads/package/unity-integration/


## Multiplayer
- Download Photon Pun 2 and Open in Unity > Import into project https://assetstore.unity.com/packages/tools/network/pun-2-free-119922
- Download Photon Voice 2 import into project https://assetstore.unity.com/packages/tools/audio/photon-voice-2-130518

- Create a new Photon account Pun app in Photon Dashboard https://www.photonengine.com/
- Copy Photon Pun App ID into the Photon server settings
- Update project material to URP shaders as they may appear magenta 

# Testing multiplayer setup
You will need to connect your Quest at this point.

- Build settings > Add scenes 
- QuestPun2Template > Scenes > Photon2Lobby
- QuestPun2Template > Scenes > Photon2Room
- Save Unity Project (settings might not be saved otherwise)
- RESTART UNITY (seems to make a difference when things seem weird)
- Press Play on editor - you should see the test room after brief ‘connecting’ message


## Test in Oculus Quest from Unity Editor (PC only)
- Connect Quest to PC via Oculus link cable or Air link. 
- Hit Play - you should see the scene. 

### Things to test
- Inkibit Connecting… shows up on screen
- Demo scene loads up with a table and cube on it
- You can see your hands
- You can move around using joystick on left controller
- You can rotate with joystick on right controller
- You can grab the cube from table and clone using right controller trigger while grabbing
- The cloned cubes will be static whilst the original has gravity
- Pressing the exit button on side takes you briefly to lobby and back againi

# Making Holons
- new URP instructions coming...
Get in touch at mafj.inkibit@rootinteractive.com to ask for old ones and we'll send you a link


