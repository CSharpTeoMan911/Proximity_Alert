# Raspberry Pi4 (Ubuntu Server with Proximity Alert) 🛠️🔍
## Overview
This project leverages a Raspberry Pi 4 microserver running Ubuntu Server 24.04 LTS, with a proximity alert service designed to detect objects within a 30 cm radius. It utilizes advanced features like ultrasonic sensors and C# to create a robust, real-time monitoring system.

# Key Features
* Proximity Detection: Uses ultrasonic sensors for real-time object distance detection.
* Efficient Server: Runs efficiently without a GUI, activating only when required, making the most of Raspberry Pi’s capabilities. 💪

## 🖥️ Server Setup

1. Prepare the SD Card
* ➡️Download and use [Raspberry PI imager](https://www.raspberrypi.com/software/) to burn Ubuntu Server 24.04 LTS onto a 32 GB SD card. Then, set up your Wi-Fi network and enable SSH for remote access. 

![imager](https://github.com/user-attachments/assets/c20ad05a-2dea-4ac9-b0ee-22d66c4c3108)

* ➡️Press next, then edit the customisation settings. Add your Wi-Fi network and password, afterwards go to the **Services** tab.

![Screenshot from 2024-07-27 23-51-03](https://github.com/user-attachments/assets/22aab77b-134c-4a3d-ae53-57d5f79ebfc3)

* ➡️In the **Services** tab, enable SSH with password authentication to be able to operate the Raspberry PI microserver remotely, via SSH.

![Screenshot from 2024-07-27 23-51-33](https://github.com/user-attachments/assets/b969e2cc-d901-4010-b9b8-1cad149d46dc)

2) Configuring the Raspberry Pi
After booting the Raspberry Pi with the OS:

* ➡️Run ```sudo apt update -y && sudo apt upgrade -y``` to update.
* ➡️Install raspi-config: ```sudo apt install raspi-config```
* ➡️Adjust the display settings to prevent resolution issues (via raspi-config > Display options > Underscan).
* ➡️Allocate 300 MB for GPU memory under Performance options.

![Raspi config](https://github.com/user-attachments/assets/2588866a-3a8f-42f9-8dc9-ca5576689926)

![Raspi config 2](https://github.com/user-attachments/assets/d1156b33-3de7-4a64-8660-5c674c68ab4a)

![Raspi config 3](https://github.com/user-attachments/assets/c6a943f6-1c9d-4772-9dfe-036c3c4d710b)

![Raspi config 4](https://github.com/user-attachments/assets/bf8d6d55-79ce-4e9d-812e-c6945093cbc4)

![Perf options 1](https://github.com/user-attachments/assets/35d446c0-dbeb-4a6c-a0bf-336ef64d95f7)

![Perf options 2](https://github.com/user-attachments/assets/b0fc25e0-094b-4518-bce7-fc62b39e9630)

![Perf options 3](https://github.com/user-attachments/assets/589314e8-2a8d-4dac-a88f-fe11b48e396a)

* ➡️Edit the **config.txt** file within the path **/boot/firmware** and add the configuration below at the end of the config file
```
# Make the first HDMI port of Raspberry PI plug and play
hdmi_force_hotplug:0=1**

# Make the second HDMI port of Raspberry PI plug and play
hdmi_force_hotplug:1=1

# Make the HDMI ports use the CEA formats which consists of resolutions in pixels (480p, 576p, 720p, 1080p, etc.)
hdmi_group=1

# Make the HDMI ports render the video output at a resolution of 1080p and a referesh rate of 60 Hz
hdmi_mode=16
```

![Boot config](https://github.com/user-attachments/assets/3176767e-f7a3-4c5f-806e-de7d5d9a02f1)

3. Desktop Environment (Gnome) Setup
* ➡️Install Ubuntu Gnome Desktop:
```
# Install the Gnome Desktop Environment
sudo apt install ubuntu-gnome-desktop -y
```
* ➡️Disable GDM by default for low resource usage (This will disable the GUI at startup):
```
# Disable the GUI at startup by disabling the Gnome Desktop Environment service
sudo systemctl disable gdm
```
* ➡️Start it only when needed using:
```
# Start the Gnome Desktop Environment service only when needed
sudo systemctl start gdm
```
This enables you to have a full desktop interface when needed, without compromising the server's efficiency! 🎯 




## 🔊 Proximity Alert Service Management
1. Hardware Setup
* ➡️To enable proximity detection, you’ll need an ultrasonic sensor. This sensor works by emitting ultrasonic pulses and detecting their reflection off objects, determining the distance.

![20240727_223555](https://github.com/user-attachments/assets/eeaa2bbc-0a39-434f-9e40-813c114b8e3b)

![20240727_223401](https://github.com/user-attachments/assets/0cb1b161-16ac-42d7-958e-c125d79c0431)

![Ultrasonic sensor schematics](https://github.com/user-attachments/assets/3c77f498-eb2f-4824-9f13-873712e92855)

![Calculate distance](https://github.com/user-attachments/assets/a7f2946b-cbc6-4914-bd7e-4d4524c3ea2c)

![Raspberry Pi pinnout](https://github.com/user-attachments/assets/46841a28-44bd-4319-a234-0b6617f687a6)

* ➡️Wiring: Connect the ultrasonic sensor to the Raspberry Pi's GPIO pins (detailed schematics are available).

<br/>
<br/>

2. Download & Build the Application
* ➡️Option 1: Download the precompiled binary from the [Releases](https://github.com/CSharpTeoMan911/Proximity_Alert/releases) page.
* ➡️Option 2: Clone the repo and build the app: dotnet publish --framework net8.0 --runtime linux-arm64.

Once the app is built, ensure you have the necessary GPIO drivers installed:
```
sudo apt install gpio -y || sudo apt install wiringpi -y && sudo apt install libgpiod-dev -y
```
Start the application:
```
sudo ./Proximity_Alert
```
<br/>

The app will throw an error and create a config file within a directory named **config**. This is happening because the app cannot use the required **Firebase Database** credentials because the config file within the app's directory did not exist.

![Config file](https://github.com/user-attachments/assets/916692bc-1731-4016-9842-ad77b0ebadf7)

<br/>
<br/>

3. Firebase Configuration:

The app integrates with Firebase for alert storage. Follow these steps to set it up:

* ➡️Create a Firebase project.
* ➡️Enable Realtime Database and Email & Password authentication.
* ➡️Configure your Firebase credentials in the app's config file.

Ensure your Firebase rules are set to allow access from the app and replace YOUR_USER_ID in the database rules with your Firebase user ID.

<br/>
<br/>

## Firebase App Setup:

* ➡️ After the config file is created, go to [Firebase](https://console.firebase.google.com) and create a project

![Eva_Capture471128567](https://github.com/user-attachments/assets/c3de1344-47ea-4ca2-bca4-effaf8ea343d)

![Eva_Capture886266121](https://github.com/user-attachments/assets/92872065-5176-4651-8100-7fa4f844ab67)

![Eva_Capture340840882](https://github.com/user-attachments/assets/4498e51f-accb-4183-aa78-9b8ac9894c0f)

![Eva_Capture144383587](https://github.com/user-attachments/assets/6320675e-8535-4c1a-8484-dd72030e54d8)

<br/>
<br/>
<br/>

* ➡️ Initialise the **Realtime Database** for this project within the **Firebase** app.

![Eva_Capture1584556632](https://github.com/user-attachments/assets/17500289-fd11-48a4-8da4-6a8d5ca1318c)

![Eva_Capture1909942881](https://github.com/user-attachments/assets/10cf377d-7ac1-410d-ba3d-c67d50c23fb8)

![Eva_Capture1171014450](https://github.com/user-attachments/assets/e41f2b05-4740-44f5-89fa-0022b1824287)

![Eva_Capture1529947429](https://github.com/user-attachments/assets/9db08781-ad36-4bbb-9e64-2dc984c23355)

![Eva_Capture294930515](https://github.com/user-attachments/assets/d0020622-0511-455c-9414-3a14320db83a)

<br/>
<br/>
<br/>

* ➡️ Initialise the **Authentication** service for this project within the **Firebase** app, enable **Email and Password** authentication, and add a user with your desired email and password.

![Eva_Capture1842421617](https://github.com/user-attachments/assets/956b23eb-6a2e-45e8-b62f-ef52bdcd9a23)

![Eva_Capture1429696448](https://github.com/user-attachments/assets/8fa8538e-8ceb-4dd9-8bed-c40b65b7636e)

![Eva_Capture1945010461](https://github.com/user-attachments/assets/b5fb369a-3061-4672-b078-1967fff81e75)

![Eva_Capture70934884](https://github.com/user-attachments/assets/7fa0d37d-0144-4fbc-8db4-263525ca5f85)

![Eva_Capture1864088023](https://github.com/user-attachments/assets/5869e8b7-09b1-48f8-a16e-7fadfc9cb31a)

<br/>
<br/>
<br/>

* ➡️ Copy the **Firebase database rules** file within the app's repository within the **Database rules** of the **Firebase app's Realtime Database**.

![Eva_Capture2140988189](https://github.com/user-attachments/assets/500c7a27-ac4b-4383-a371-b657cbd6745d)

![Eva_Capture1772627372](https://github.com/user-attachments/assets/9182a3b0-46c5-4d70-8931-61b9a7821c4a)

![Eva_Capture1104690279](https://github.com/user-attachments/assets/7011c895-4dc0-48d3-af93-35ec82d0bdce)

<br/>
<br/>
<br/>

* ➡️ Copy the **UID** of the user created in the **Authentication** service and replace the **YOUR_USER_ID** sections within your database rules with it.

![Eva_Capture598950295](https://github.com/user-attachments/assets/26dbd8ca-5afa-4e8c-863f-6c2be9484e27)

![Eva_Capture744311447](https://github.com/user-attachments/assets/b72ea501-7348-4a9f-abab-d5468aeb69e4)

<br/>
<br/>
<br/>

* ➡️ Go to the project settings and create a web app.

![Eva_Capture453275120](https://github.com/user-attachments/assets/edcdea97-2868-4708-a1dc-5cd5b21d89c8)

![Eva_Capture1287317653](https://github.com/user-attachments/assets/42310937-601f-462f-975f-884b90f7b0bb)

<br/>
<br/>
<br/>

* ➡️ Go to the project's, before mentioned, config file, and replace the values within the config file with the values within the **Firebase project settings** app page config (**apiKey**, **authDomain**, and **databaseURL**). Also, replace the fields within the config file that contain the email and password with the **Firebase** generated user's email and password.

![Eva_Capture1561710461](https://github.com/user-attachments/assets/213baff8-52ea-43ba-9dcc-232ab0c967d3)

![Eva_Capture180253262](https://github.com/user-attachments/assets/7c50f7b5-1fa7-417f-906c-8216db7bb63f)

```
// Example of configuration file structure and content
{
  "api_key": "AIzaSyB-n2fwkr6_ZzRRTpzIE FEQW3x9YMIaPg", 
  "user_email": "your_email_address@gmail.com",
  "user_password": "your_email_password",
  "firebase_auth_domain": "proximity-alert-bf91a.firebaseapp.com",
  "firebase_database_url": "https://proximity-alert-bf91a-default-rtdb.firebaseio.com",
  "proximity_alert_expiration_start: 10"
}
```

<br/>
<br/>
<br/>

# 🚀 Make it Run as a Service
Create a Linux service to run the proximity alert app on boot:

1) Create the service file:

```
sudo vim /etc/systemd/system/proximity-alert.service
```
2) Service File Content:

```
[Unit]
Description=Service sends proximity alerts when objects are at a proximity of 30cm or less

[Service]
ExecStart=/YOUR_PATH_TO_THE_APP/Proximity_Alert

[Install]
WantedBy=multi-user.target
```
Replace the ```YOUR_PATH_TO_THE_APP``` to the path to the app's directory. It is recomened for the service to be placed in the ```/etc``` directory.

3) Enable and Start the Service:

```
systemctl start proximity-alert.service
systemctl enable proximity-alert.service
```

# 💥 Proximity Alert in Action

With the service running, the system will periodically check the proximity of objects, triggering an alert in the Firebase database when something is detected within 30 cm.

## Proximity alert demo:

![ProximityAlertDemo-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/75450e1e-f91d-413a-9c63-c39685fb25d9)

# 🌱 Conclusion
This project demonstrates the power of the Raspberry Pi 4, C# hardware manipulation, and IoT capabilities with an ultrasonic sensor. The system can be easily expanded with more sensors or customizations to improve its functionality. Plus, by integrating Firebase, you get real-time cloud storage of the alerts.
