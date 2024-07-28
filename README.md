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

#Make the first HDMI port of Raspberry PI plug and play
<br/>
hdmi_force_hotplug:0=1

#Make the second HDMI port of Raspberry PI plug and play
<br/>
hdmi_force_hotplug:1=1

#Make the HDMI ports use the CEA formats which consists of resolutions in pixels (480p, 576p, 720p, 1080p, etc.)
<br/>
hdmi_group=1

#Make the HDMI ports render the video output at a resolution of 1080p and a referesh rate of 60 Hz
<br/>
hdmi_mode=16

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

Move the folder into the **/etc** directory enter within the directory and start the application using the command **sudo ./Proximity_Alert**

![Proxi location](https://github.com/user-attachments/assets/3bfa8daf-72fa-4442-ac4a-13269eb0a62a)

![Proxi exe loc](https://github.com/user-attachments/assets/c939c768-4174-47a4-abfe-ec17ed469a93)



![ProximityAlertDemo-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/75450e1e-f91d-413a-9c63-c39685fb25d9)
