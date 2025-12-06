using MySmartHome.Devices;

namespace MySmartHome
{
    class Program
    {
        static void Main()
        {
            SmartHomeController controller = new SmartHomeController();

            // Create devices
            Light light = new Light(controller.Logger);
            AirConditioner airConditioner = new AirConditioner(controller.Logger);
            Heater heater = new Heater(controller.Logger);

            // Register devices
            controller.RegisterDevice(light);
            controller.RegisterDevice(airConditioner);
            controller.RegisterDevice(heater);

            // Subscribe devices to events
            controller.OnDayTimeChanged += time => light.HandleEvent("DayTimeChanged", time);
            controller.OnTemperatureChanged += temp => airConditioner.HandleEvent("TemperatureChanged", temp);
            controller.OnTemperatureChanged += temp => heater.HandleEvent("TemperatureChanged", temp);

            // Example of configuring a device
            Dictionary<string, object> acSettings = new Dictionary<string, object>
            {
                { "MinTemperature", 20 },
                { "MaxTemperature", 30 }
            };
            airConditioner.Configure(acSettings);

            // Control menu
            while (true)
            {
                Console.WriteLine("Menu:\n1. Trigger Event\n2. Control Device\n3. Show Event Log\n4. Exit");
                string choice = Console.ReadLine() ?? "";

                if (choice == "1")
                {
                    Console.WriteLine("Select event:\n1. Change Daytime\n2. Change Temperature\n3. Detect Motion");
                    string eventChoice = Console.ReadLine() ?? "";
                    switch (eventChoice)
                    {
                        case "1":
                            Console.Write("Enter daytime (Morning/Night): ");
                            string timeOfDay = Console.ReadLine() ?? "";
                            controller.ChangeDayTime(timeOfDay);
                            break;
                        case "2":
                            Console.Write("Enter temperature: ");
                            string tempInput = Console.ReadLine() ?? "";
                            if (int.TryParse(tempInput, out int temp))
                                controller.ChangeTemperature(temp);
                            else
                                Console.WriteLine("Invalid temperature input.");
                            break;
                        case "3":
                            controller.DetectMotion();
                            break;
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Enter device name: ");
                    string deviceName = Console.ReadLine() ?? "";
                    Console.Write("Enter command (On/Off): ");
                    string command = Console.ReadLine() ?? "";
                    controller.TriggerDevice(deviceName, command);
                }
                else if (choice == "3")
                {
                    controller.ShowLog();
                }
                else if (choice == "4")
                {
                    break;
                }
            }
        }
    }
}
