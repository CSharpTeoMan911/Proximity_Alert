namespace Proximity_Alert
{
    using System.Device.Gpio;
    using System.Threading;
    using System.Diagnostics;
    using System.Text;
    using System.Timers;

    class Program:Shared
    {
        // 'Trig' is the pin of the ultrasonic sensor that when a voltage of 3.3V is applied it sends an ultrasonic pulse
        private static readonly int Trig = 40;

        // 'Echo' is the pin of the ultrasonic sensor that has a voltage 3.3V by default and when the sensor sound is received the voltage drops to 0V  
        private static readonly int Echo = 38;

        // GPIO controller class used to control the GPIO of the RaspBerry PI
        private static GpioController gpio = new GpioController(PinNumberingScheme.Board);

        // 'DateTime' object that stores the datetime value when the ultrasonic pulse was initiated
        private static DateTime pulse_init;

        private static bool shudown;


        // Method used to initiate the ultrasonic pulse
        public static void SendUltrasonicPulse(){
            // Set the 'Trig' pin voltage to 3.3 V to initiate the ultrasonic pulse
            gpio?.Write(Trig, true);

            // Set the 'Trig' pin voltage to 0 V to terminate the ultrasonic pulse
            gpio?.Write(Trig, false);

            // Get the datetime when the ultrasonic pulse was initiated
            pulse_init = DateTime.Now;
        }


        // Main entry point of the application
        public static void Main(string[] args)
        {
            _= Main_Op().Result;
        }


        private static async Task<bool> Main_Op(){
            await Get_Config();

            System.Timers.Timer cache_cleanup = new System.Timers.Timer();
            cache_cleanup.Interval = 3600000;
            cache_cleanup.Elapsed += CacheCleanup;
            cache_cleanup.Start();
            

            AppDomain.CurrentDomain.ProcessExit += OnShutdown;
            Console.CancelKeyPress += Cancelled;

            // Set the 'Trig' pin to be an output pin. An output pin can only emit signals.
            gpio?.OpenPin(Trig, PinMode.Output);

            // Set the 'Echo' pin to be an input_pull_down pin. An input input_pull_down can only receive signals 
            // and its default voltage is the maximum voltage. When the circuit that includes this pin is opened,
            // the voltage will drop to 0 V. 
            gpio?.OpenPin(Echo, PinMode.InputPullDown);

            // Register a callback method to listen to voltage changes in the 'Echo' pin.
            gpio?.RegisterCallbackForPinValueChangedEvent(Echo, PinEventTypes.Falling | PinEventTypes.Rising, OnUltrasonicPulseReceived);

            while(shudown == false){
                // Send an ultrasonic pulse using the 'Trig' pin
                SendUltrasonicPulse();

                // Sleep the current CPU thread 1000 milliseconds (1 second)
                Thread.Sleep(100);
            }

            return true;
        }



        // Callback method that listens to voltage changes in the 'Echo' pin.
        public static async void OnUltrasonicPulseReceived(object sender, PinValueChangedEventArgs args){
            
            // If the voltage changed in the 'Echo' pin and the voltage is the minimum value (0 V)
            if(PinEventTypes.Falling == args.ChangeType)
            {
                // Get the total number of milliseconds and divide them by 1000 to get the total number of seconds ellapsed
                double seconds = (DateTime.Now - pulse_init).TotalMilliseconds / 1000;

                ///////////////////////////////////////////////////
                //                                               //
                //  (Speed of sound) S = 343 m/s                 //
                //                                               //
                //  S * 100 = 34300 cm/s                         //
                //                                               //
                //  (Time) t * (Speed) S = (Distance) d          //
                //                                               //
                //  t * 34300 cm/s = Distance to object and back //
                //                                               //
                //  (t * 34300 cm/s) / 2) = Distance to object   //
                //                                               //
                //  d = (t * s) / 2                              //
                //                                               //
                ///////////////////////////////////////////////////

                // Distance is the elapsed time times the speed of sound divided by 2
                int distance = (int)(seconds * 34300 / 2);

                if(distance <= 30){
                    if((DateTime.Now - last_proximity_alert).TotalMinutes >= 10){
                        last_proximity_alert = DateTime.Now;
                        await SnapShot();
                    }
                }
                
                Console.WriteLine($"\n\nDistance: {distance} cm"); 
            }
        }

        // Event callback that detects when the application process is shutting down 
        public static void OnShutdown(object? obj, EventArgs args)
        {
            DeallocateObjectsFromRAM();
        }

        // Event callback that detects when the application is terminated via [Ctrl + C]
        public static void Cancelled(object? obj, ConsoleCancelEventArgs args)
        {
            DeallocateObjectsFromRAM();
        }

        private static async Task<bool> SnapShot(){
            await Insert_Alert();
            return true;
        }

        private static async void CacheCleanup(object? sender, ElapsedEventArgs args){
            await Delete_Alerts();
        }

        // Method that is deallocating unmanaged objects from the RAM memory and that is closing the opened GPIO pins
        private static void DeallocateObjectsFromRAM(){
            shudown = true;
            gpio?.ClosePin(Trig);
            gpio?.ClosePin(Echo);
            gpio?.Dispose();
        }
    }
}
