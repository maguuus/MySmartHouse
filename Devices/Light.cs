using MySmartHome.Enums;

namespace MySmartHome.Devices
{
    public class Light(string name, EventLogger logger): ISmartDevice
    {
        public string Name { get; } = name;
        private bool _isOn;

        public void OnDayTimeChanged(DayTime timeOfDay)
        {
            try
            {
                if (timeOfDay == DayTime.Morning && !_isOn)
                {
                    _isOn = true;
                    Console.WriteLine($"{Name} turned on (Morning).");
                    logger.Log($"{Name} turned on (Morning).");
                }
                else if (timeOfDay == DayTime.Night && _isOn)
                {
                    _isOn = false;
                    Console.WriteLine($"{Name} turned off (Night).");
                    logger.Log($"{Name} turned off (Night).");
                }
                else
                {
                    logger.Log($"{Name} ignored DayTime: {timeOfDay}");
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Error in {Name} OnDayTimeChanged: {ex.Message}");
            }
            // Implement handling "DayTimeChanged" event:
            // Turn on light in the morning, turn off light at night.
        }

        public void OnTemperatureChanged(int temperature) {}

        public void OnMotionDetected() {}

        public void Configure(Dictionary<string, object> settings)
        {
            Console.WriteLine("Light configured.");
            logger.Log("Light configured.");
            // Implement configuring light parameters (e.g., brightness).
        }

        public void ExecuteCommand(string command)
        {
            try
            {
                if (command.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    _isOn = true;
                    Console.WriteLine($"{Name} manually turned on.");
                    logger.Log($"{Name} manually turned on.");
                }
                else if (command.Equals("Off", StringComparison.OrdinalIgnoreCase))
                {
                    _isOn = false;
                    Console.WriteLine($"{Name} manually turned off.");
                    logger.Log($"{Name} manually turned off.");
                }
                else
                {
                    Console.WriteLine($"Invalid command for {Name}.");
                    logger.Log($"Invalid command for {Name}.");
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Error in {Name} ExecuteCommand: {ex.Message}");
            }
            // Implement manual control of the light (turn on/off).
        }
    }
}