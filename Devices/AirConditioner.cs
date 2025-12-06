using MySmartHome.Enums;

namespace MySmartHome.Devices
{
    public class AirConditioner(string name, EventLogger logger) : ISmartDevice
    {
        public string Name { get; } = name;
        private int _minTemperature = 18;
        private int _maxTemperature = 25;
        private bool _isOn;

        public void OnDayTimeChanged(DayTime timeOfDay) {}

        public void OnTemperatureChanged(int temperature)
        {
            try
            {
                if (temperature > _maxTemperature && !_isOn)
                {
                    _isOn = true;
                    Console.WriteLine($"{Name} turned on (High Temperature).");
                    logger.Log($"{Name} turned on (High Temperature).");
                }
                else if (temperature < _minTemperature && _isOn)
                {
                    _isOn = false;
                    Console.WriteLine($"{Name} turned off (Low Temperature).");
                    logger.Log($"{Name} turned off (Low Temperature).");
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
                _minTemperature = (int) minTemperature;
            if (settings.TryGetValue("MaxTemperature", out var maxTemperature))
                _maxTemperature = (int) maxTemperature;

            Console.WriteLine($"{Name} configured: Min={_minTemperature}°C, Max={_maxTemperature}°C.");
            logger.Log($"{Name} configured: Min={_minTemperature}°C, Max={_maxTemperature}°C."); 
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