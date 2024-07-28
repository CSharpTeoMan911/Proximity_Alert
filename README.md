# Raspberry PI 4 ( Ubuntu Server with proximity alert )

This is a Raspberry Pi 4 Microserver with a proximity alert service. This service has the purpose of monitoring physical activity near the server and create alerts when an object is near the server at a distance of 30 cm or less. The server has the **Ubuntu Server 24.04 LTS** as its operatiing system and **Ubuntu Gnome Desktop** as its desktop environment. The desktop environment service is disabled by default, thus allowing the server to operate with a minimum resource consuption. When the user/sys admin desires to use the desktop environment, it can be activated by starting the **Gnome Desktop service (GDM)** through the **systemctl** OS level service manager. 

## Sever setup

Download [Raspberry PI imager](https://www.raspberrypi.com/software/) and use an SD card (32 GB size is the preffered size) to burn an Linux OS image on it.

![imager](https://github.com/user-attachments/assets/c20ad05a-2dea-4ac9-b0ee-22d66c4c3108)

Press next, then edit the customisation settings. Add your Wi-Fi network and password, afterwards go to the **Services** tab.

![Screenshot from 2024-07-27 23-51-03](https://github.com/user-attachments/assets/22aab77b-134c-4a3d-ae53-57d5f79ebfc3)

In the **Services** tab, enable SSH with password authentication to be able to operate the Raspberry PI microserver remotely, via SSH.

![Screenshot from 2024-07-27 23-51-33](https://github.com/user-attachments/assets/b969e2cc-d901-4010-b9b8-1cad149d46dc)

After the OS image was mounted on the SD card, put the card in the Raspberry PI, plug in the Raspberry PI into a monitor / TV, and plug in the Raspberry PI into a power supply to turn it on. After the Raspberry PI booted the OS, log in and update your OS and upgrade your packages using the command **sudo apt update -y && sudo apt upgrade -y**. After the OS level update was completed download the package **raspi-config** using the command **sudo apt install raspi-config**. After the **raspi-config** was installed start the utility by using the command **sudo raspi-config**. Go to **Display options** -> **Underscan** and select yes. This must be done so that the display output won't have a mismatched resolution.
![Raspi config](https://github.com/user-attachments/assets/2588866a-3a8f-42f9-8dc9-ca5576689926)

![Raspi config 2](https://github.com/user-attachments/assets/d1156b33-3de7-4a64-8660-5c674c68ab4a)

![Raspi config 3](https://github.com/user-attachments/assets/c6a943f6-1c9d-4772-9dfe-036c3c4d710b)

![Raspi config 4](https://github.com/user-attachments/assets/bf8d6d55-79ce-4e9d-812e-c6945093cbc4)

Afterwards, go to **Performance options** -> **GPU Memory** and type 300. This make the Raspberry PI reserve 300 MB from the RAM memory to be used as its GPU VRAM.

![Perf options 1](https://github.com/user-attachments/assets/35d446c0-dbeb-4a6c-a0bf-336ef64d95f7)

![Perf options 2](https://github.com/user-attachments/assets/b0fc25e0-094b-4518-bce7-fc62b39e9630)

![Perf options 3](https://github.com/user-attachments/assets/589314e8-2a8d-4dac-a88f-fe11b48e396a)

Afterwards edit the **config.txt** file within the path **/boot/firmware** by using the command **sudo vim /boot/firmware/config.txt**. 
Add the following lines at the end of the configuration file:

**# Make the first HDMI port of Raspberry PI plug and play**
<br/>
**hdmi_force_hotplug:0=1**

**# Make the second HDMI port of Raspberry PI plug and play**
<br/>
**hdmi_force_hotplug:1=1**

**# Make the HDMI ports use the CEA formats which consists of resolutions in pixels (480p, 576p, 720p, 1080p, etc.)**
<br/>
**hdmi_group=1**

**# Make the HDMI ports render the video output at a resolution of 1080p and a referesh rate of 60 Hz**
<br/>
**hdmi_mode=16**

![Boot config](https://github.com/user-attachments/assets/3176767e-f7a3-4c5f-806e-de7d5d9a02f1)

Afterwards install the **Gnome Desktop Environment** by using the command **sudo apt install ubuntu-gnome-desktop -y**. After the installation is completed run the command **sudo systemctl disable gdm** to disable the GUI from running after boot in order for the server to have the same low resource consumption. When the user/sys admin needs to use the GUI, the command **sudo systemctl start gdm** must be used. After all of these configurations the system must be rebooted using the command **reboot**.   




## Proximity alert service managemnt

![20240727_223555](https://github.com/user-attachments/assets/eeaa2bbc-0a39-434f-9e40-813c114b8e3b)

![20240727_223401](https://github.com/user-attachments/assets/0cb1b161-16ac-42d7-958e-c125d79c0431)

For the server to be able to detect objects within its proximity it needs an ultrasonic sensor. The ultrasonic sensor is sending ultrasonic sensors from an emitter and receives the ultrasonic pulses at a receiver, which is converting them to electrical pulses using a piezo-electric cristal.

![Ultrasonic sensor schematics](https://github.com/user-attachments/assets/3c77f498-eb2f-4824-9f13-873712e92855)

![Calculate distance](https://github.com/user-attachments/assets/a7f2946b-cbc6-4914-bd7e-4d4524c3ea2c)

![Raspberry Pi pinnout](https://github.com/user-attachments/assets/46841a28-44bd-4319-a234-0b6617f687a6)

<br/>
<br/>

After the ultrasonic sensor was attached, go to the release page of this repository and download and unzip the executable binary. 

![Eva_Capture289888893](https://github.com/user-attachments/assets/f3c9dd9a-98b8-4969-936b-8ddd05abc09f)

![Eva_Capture1496962042](https://github.com/user-attachments/assets/e84d5706-115e-42d6-81fc-ca07589122ba)

Alternatively, you can clone the GitHub repository and build the app by using the command **dotnet publish --framework net8.0 --runtime linux-arm64**

![Publish  NET](https://github.com/user-attachments/assets/540c4e9c-6a3d-4e76-8e8c-7e0f42d1740d)

Afterwards, navigate the folder into the **/etc** directory, then navigate into the directory (**cd app_dir_name**) and start the application using the command **sudo ./Proximity_Alert**

![Proxi location](https://github.com/user-attachments/assets/3bfa8daf-72fa-4442-ac4a-13269eb0a62a)

![Proxi exe loc](https://github.com/user-attachments/assets/c939c768-4174-47a4-abfe-ec17ed469a93)

Then enter the command to ensure that the GPIO drivers are installed within the system:

**# Ensure the GPIO drivers are installed**
<br/>
**sudo apt install gpio -y || sudo apt install wiringpi -y && sudo apt install libgpiod-dev -y**

![GPIO driver command](https://github.com/user-attachments/assets/d1ec4f39-551c-456f-9ec3-5887176f8c96)


<br/>
<br/>

The app will throw an error and create a config file within a directory named **config**. This is happening because the app cannot use the required **Firebase Database** credentials because the config file did not exist.

![Config file](https://github.com/user-attachments/assets/916692bc-1731-4016-9842-ad77b0ebadf7)

<br/>
<br/>

After the config file is created, go to [Firebase](https://console.firebase.google.com) and create a project

![Eva_Capture471128567](https://github.com/user-attachments/assets/c3de1344-47ea-4ca2-bca4-effaf8ea343d)

![Eva_Capture886266121](https://github.com/user-attachments/assets/92872065-5176-4651-8100-7fa4f844ab67)

![Eva_Capture340840882](https://github.com/user-attachments/assets/4498e51f-accb-4183-aa78-9b8ac9894c0f)

![Eva_Capture144383587](https://github.com/user-attachments/assets/6320675e-8535-4c1a-8484-dd72030e54d8)

<br/>
<br/>

Afterwards, initialise the **Realtime Database** for this project within the **Firebase** app.

![Eva_Capture1584556632](https://github.com/user-attachments/assets/17500289-fd11-48a4-8da4-6a8d5ca1318c)

![Eva_Capture1909942881](https://github.com/user-attachments/assets/10cf377d-7ac1-410d-ba3d-c67d50c23fb8)

![Eva_Capture1171014450](https://github.com/user-attachments/assets/e41f2b05-4740-44f5-89fa-0022b1824287)

![Eva_Capture1529947429](https://github.com/user-attachments/assets/9db08781-ad36-4bbb-9e64-2dc984c23355)

![Eva_Capture294930515](https://github.com/user-attachments/assets/d0020622-0511-455c-9414-3a14320db83a)

<br/>
<br/>

Afterwards, initialise the **Authentication** service for this project within the **Firebase** app, enable **Email and Password** authentication, and add a user with your desired email and password.

![Eva_Capture1842421617](https://github.com/user-attachments/assets/956b23eb-6a2e-45e8-b62f-ef52bdcd9a23)

![Eva_Capture1429696448](https://github.com/user-attachments/assets/8fa8538e-8ceb-4dd9-8bed-c40b65b7636e)

![Eva_Capture1945010461](https://github.com/user-attachments/assets/b5fb369a-3061-4672-b078-1967fff81e75)

![Eva_Capture70934884](https://github.com/user-attachments/assets/7fa0d37d-0144-4fbc-8db4-263525ca5f85)

![Eva_Capture1864088023](https://github.com/user-attachments/assets/5869e8b7-09b1-48f8-a16e-7fadfc9cb31a)

<br/>
<br/>

Afterwards, copy the **Firebase database rules** file within the app's repository within the **Database rules** of the **Firebase app's Realtime Database**.

![Eva_Capture2140988189](https://github.com/user-attachments/assets/500c7a27-ac4b-4383-a371-b657cbd6745d)

![Eva_Capture1772627372](https://github.com/user-attachments/assets/9182a3b0-46c5-4d70-8931-61b9a7821c4a)

![Eva_Capture1104690279](https://github.com/user-attachments/assets/7011c895-4dc0-48d3-af93-35ec82d0bdce)

<br/>
<br/>

Afterwards, copy the **UID** of the user created in the **Authentication** service and replace the **YOUR_USER_ID** sections within your database rules with it.

![Eva_Capture598950295](https://github.com/user-attachments/assets/26dbd8ca-5afa-4e8c-863f-6c2be9484e27)

![Eva_Capture744311447](https://github.com/user-attachments/assets/b72ea501-7348-4a9f-abab-d5468aeb69e4)

<br/>
<br/>

Afterwards go to the project settings and create a web app.

![Eva_Capture453275120](https://github.com/user-attachments/assets/edcdea97-2868-4708-a1dc-5cd5b21d89c8)

![Eva_Capture1287317653](https://github.com/user-attachments/assets/42310937-601f-462f-975f-884b90f7b0bb)


<br/>
<br/>

Afterwards, go to the project's, before mentioned, config file, and replace the values within the config file with the values within the **Firebase project settings** app page config (**apiKey**, **authDomain**, and **databaseURL**). Also, replace the fields within the config file that contain the email and password with the **Firebase** generated user's email and password.

![Eva_Capture1561710461](https://github.com/user-attachments/assets/213baff8-52ea-43ba-9dcc-232ab0c967d3)

![Eva_Capture180253262](https://github.com/user-attachments/assets/7c50f7b5-1fa7-417f-906c-8216db7bb63f)

![config file](https://github.com/user-attachments/assets/5097a189-6432-4d50-8205-8653a01dcba9)


<br/>
<br/>

Then, create a Linux service within the OS. Firsly, create a Linux service file by typing the command **sudo vim /etc/systemd/system/proximity-alert.service**, copy the the text bellow and replace the *YOUR_PATH_TO_THE_APP* to the path to the application executable:

<br/>

**[Unit]**
<br/>
**Description=Service sends proximity alerts when objects are at a proximity of 30cm or less**
<br/>
**[Service]**
<br/>
**ExecStart=/etc/YOUR_PATH_TO_THE_APP/Proximity_Alert**
<br/>
**[Install]**
<br/>
**WantedBy=multi-user.target**


<br/>
<br/>

Then, start the service and enable the service in order to be ran at system bootup by using the following commands:
<br/>
<br/>
**systemctl start proximity-alert.service**
<br/>
**systemctl enable proximity-alert.service**

<br/>
<br/>

Now the Proximity Alert Service is up and operational. Every time someone or something is 30 cm or closer to the server it will create an alert within the **Firebase database**. The app will do this every 10 minutes after the previous alert or instantly if no alert was created at all.


![ProximityAlertDemo-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/75450e1e-f91d-413a-9c63-c39685fb25d9)
