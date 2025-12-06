namespace MySmartHome.Devices
{
    public class Heater(EventLogger logger) : ISmartDevice
    {
        private int _minTemperature = 10;
        private bool _isOn;

        public void HandleEvent(string eventType, object eventData)
        {
            if (eventType.Equals("TemperatureChanged", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    int temperature = (int)eventData;
                    if (temperature >= _minTemperature && _isOn)
                    {
                        _isOn = false;
                        Console.WriteLine("Heater turned off (Normal Temperature).");
                        logger.Log("Heater turned off (Normal Temperature).");
                    }
                    else if (temperature < _minTemperature && !_isOn)
                    {
                        _isOn = true;
                        Console.WriteLine("Heater turned on (Low Temperature).");
                        logger.Log("Heater turned on (Low Temperature).");
                    }
                }
                catch (Exception ex)
                {
                    logger.Log($"Error in Heater HandleEvent: {ex.Message}");
                }
            }
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.TryGetValue("MinTemperature", out var minTemperature))
                _minTemperature = (int)minTemperature;
            
            Console.WriteLine($"Heater configured: Min={_minTemperature}°C.");
            logger.Log($"Heater configured: Min={_minTemperature}°C.");
        }

        public void ExecuteCommand(string command)
        {
            try
            {
                if (command.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    _isOn = true;
                    Console.WriteLine("Heater manually turned on.");
                    logger.Log("Heater manually turned on.");
                }
                else if (command.Equals("Off", StringComparison.OrdinalIgnoreCase))
                {
                    _isOn = false;
                    Console.WriteLine("Heater manually turned off.");
                    logger.Log("Heater manually turned off.");
                }
                else
                {
                    Console.WriteLine("Invalid command for Heater.");
                    logger.Log("Invalid command for Heater.");
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Error in Heater ExecuteCommand: {ex.Message}");
            }
        }
    }
}