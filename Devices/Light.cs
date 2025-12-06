namespace MySmartHome.Devices
{
    public class Light(EventLogger logger): ISmartDevice
    {
        private bool _isOn;

        public void HandleEvent(string eventType, object eventData)
        {
            if (eventType.Equals("DayTimeChanged", StringComparison.OrdinalIgnoreCase))
            {
                var timeOfDay = eventData as string ?? "";
                try
                {
                    if (timeOfDay.Equals("Morning", StringComparison.OrdinalIgnoreCase) && !_isOn)
                    {
                        _isOn = true;
                        Console.WriteLine("Light turned on (Morning).");
                        logger.Log("Light turned on (Morning).");
                    }
                    else if (timeOfDay.Equals("Night", StringComparison.OrdinalIgnoreCase) && _isOn)
                    {
                        _isOn = false;
                        Console.WriteLine("Light turned off (Night).");
                        logger.Log("Light turned off (Night).");
                    }
                }
                catch (Exception ex)
                {
                    logger.Log($"Error in Light HandleEvent: {ex.Message}");
                }
            }
            // Implement handling "DayTimeChanged" event:
            // Turn on light in the morning, turn off light at night.
        }

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
                    Console.WriteLine("Light manually turned on.");
                    logger.Log("Light manually turned on.");
                }
                else if (command.Equals("Off", StringComparison.OrdinalIgnoreCase))
                {
                    _isOn = false;
                    Console.WriteLine("Light manually turned off.");
                    logger.Log("Light manually turned off.");
                }
                else
                {
                    Console.WriteLine("Invalid command for Light.");
                    logger.Log("Invalid command for Light.");
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Error in Light ExecuteCommand: {ex.Message}");
            }
            // Implement manual control of the light (turn on/off).
        }
    }
}