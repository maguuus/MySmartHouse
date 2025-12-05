using System;
using System.Collections.Generic;

namespace MySmartHome.Devices
{
    public class Heater : ISmartDevice
    {
        private int minTemperature = 10;
        private bool isOn;

        public void HandleEvent(string eventType, object eventData)
        {
            if (eventType == "TemperatureChanged")
            {
                int temperature = (int)eventData;
                if (temperature >= minTemperature && isOn)
                {
                    isOn = false;
                    Console.WriteLine("Heater turned off (Normal Temperature).");
                }
                else if (temperature < minTemperature && !isOn)
                {
                    isOn = true;
                    Console.WriteLine("Heater turned on (Low Temperature).");
                }
            }
            // Implement handling "TemperatureChanged" event:
            // Turn on heater if temperature is below minTemperature.
            // Turn off heater if temperature is above or equal to minTemperature.
        }

        public void Configure(Dictionary<string, object> settings)
        {
            if (settings.ContainsKey("MinTemperature"))
                minTemperature = (int)settings["MinTemperature"];
            
            Console.WriteLine($"Heater configured: Min={minTemperature}°C.");
            // Implement configuring the minimum temperature for turning on the heater.
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                Console.WriteLine("Heater manually turned on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                Console.WriteLine("Heater manually turned off.");
            }
            else
            {
                Console.WriteLine("Invalid command for Heater.");
            }
        }
    }
}