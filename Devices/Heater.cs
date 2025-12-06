using MySmartHome.Enums;

namespace MySmartHome.Devices
{
    public class Heater(string name, EventLogger logger) : ISmartDevice
    {
        public string Name { get;  } = name;
        private int _minTemperature = 10;
        private bool _isOn;

        public void OnDayTimeChanged(DayTime timeOfDay) {}

        public void OnTemperatureChanged(int temperature)
        {
            try
            {
                if (temperature >= _minTemperature && _isOn)
                {
                    _isOn = false;
                    Console.WriteLine($"{Name} turned off (Normal Temperature).");
                    logger.Log($"{Name} turned off (Normal Temperature).");
                }
                else if (temperature < _minTemperature && !_isOn)
                {
                    _isOn = true;
                    Console.WriteLine($"{Name} turned on (Low Temperature).");
                    logger.Log($"{Name} turned on (Low Temperature).");
                }
                else
                {
                    logger.Log($"{Name} ignored TemperatureChanged: {temperature}");
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Error in {Name} OnTemperatureChanged: {ex.Message}");
            }
        }

        public void OnMotionDetected() {}

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.TryGetValue("MinTemperature", out var minTemperature))
                _minTemperature = (int)minTemperature;
            
            Console.WriteLine($"{Name} configured: Min={_minTemperature}°C.");
            logger.Log($"{Name} configured: Min={_minTemperature}°C.");
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
        }
    }
}