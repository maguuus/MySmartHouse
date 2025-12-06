using MySmartHome.Devices;

namespace MySmartHome
{
    class Program
    {
        static void Main()
        {
            SmartHomeController controller = new SmartHomeController();

            // Create devices
            Light light = new Light("Living room Light", controller.Logger);
            AirConditioner airConditioner = new AirConditioner("DEXP AC-1", controller.Logger);
            Heater heater = new Heater("DEXP TF-2", controller.Logger);

            // Register devices
            controller.RegisterDevice(light);
            controller.RegisterDevice(airConditioner);
            controller.RegisterDevice(heater);

            // Subscribe devices to events
            controller.DayTimeChanged += light.OnDayTimeChanged;
            controller.TemperatureChanged += airConditioner.OnTemperatureChanged;
            controller.TemperatureChanged += heater.OnTemperatureChanged;

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
                string choice = Console.ReadLine()?.Trim() ?? "";

                if (choice == "1")
                {
                    Console.WriteLine("Select event:\n1. Change Daytime\n2. Change Temperature\n3. Detect Motion");
                    string eventChoice = Console.ReadLine()?.Trim() ?? "";
                    switch (eventChoice)
                    {
                        case "1":
                            Console.Write("Enter daytime (Morning/Night): ");
                            string timeOfDay = Console.ReadLine()?.Trim() ?? "";
                            controller.ChangeDayTime(timeOfDay);
                            break;
                        case "2":
                            Console.Write("Enter temperature: ");
                            string? tempInput = Console.ReadLine()?.Trim();
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
                    string deviceName = Console.ReadLine()?.Trim() ?? "";
                    Console.Write("Enter command (On/Off): ");
                    string command = Console.ReadLine()?.Trim() ?? "";
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
