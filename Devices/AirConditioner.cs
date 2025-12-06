namespace MySmartHome.Devices
{
    public class AirConditioner(EventLogger logger) : ISmartDevice
    {
        private int _minTemperature = 18;
        private int _maxTemperature = 25;
        private bool _isOn;

        public void HandleEvent(string eventType, object eventData)
        {
            if (eventType.Equals("TemperatureChanged", StringComparison.OrdinalIgnoreCase))
            {
                int temperature = (int)eventData;
                try
                {
                    if (temperature > _maxTemperature && !_isOn)
                    {
                        _isOn = true;
                        Console.WriteLine("Air Conditioner turned on (High Temperature).");
                        logger.Log($"Air Conditioner turned on (High Temperature).");
                    }
                    else if (temperature < _minTemperature && _isOn)
                    {
                        _isOn = false;
                        Console.WriteLine("Air Conditioner turned off (Low Temperature).");
                        logger.Log("Air Conditioner turned off (Low Temperature).");
                    }
                }
                catch (Exception ex)
                {
                    logger.Log($"Error in Air Conditioner HandleEvent: {ex.Message}");
                }
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.TryGetValue("MinTemperature", out var minTemperature))
                _minTemperature = (int) minTemperature;
            if (settings.TryGetValue("MaxTemperature", out var maxTemperature))
                _maxTemperature = (int) maxTemperature;

            Console.WriteLine($"Air Conditioner configured: Min={_minTemperature}°C, Max={_maxTemperature}°C.");
            logger.Log($"Air Conditioner configured: Min={_minTemperature}°C, Max={_maxTemperature}°C."); 
        }

        public void ExecuteCommand(string command)
        {
            try
            {
                if (command.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    _isOn = true;
                    Console.WriteLine("Air Conditioner manually turned on.");
                    logger.Log($"Air Conditioner manually turned on.");
                }
                else if (command.Equals("Off", StringComparison.OrdinalIgnoreCase))
                {
                    _isOn = false;
                    Console.WriteLine("Air Conditioner manually turned off.");
                    logger.Log("Air Conditioner manually turned off.");
                }
                else
                {
                    Console.WriteLine("Invalid command for Air Conditioner.");
                    logger.Log("Invalid command for Air Conditioner.");
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Error in Air Conditioner ExecuteCommand: {ex.Message}");
            }
        }
    }
}