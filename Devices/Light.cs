using System;
using System.Collections.Generic;

namespace MySmartHome.Devices
{
    public class Light : ISmartDevice
    {
        private bool isOn;

        public void HandleEvent(string eventType, object eventData)
        {
            if (eventType == "DayTimeChanged")
            {
                string timeOfDay = (string)eventData;

                if (timeOfDay == "Morning" && !isOn)
                {
                    isOn = true;
                    Console.WriteLine("Light turned on (Morning).");
                }
                else if (timeOfDay == "Night" && isOn)
                {
                   isOn = false;
                   Console.WriteLine("Light turned off (Night).");
                }
            }
            // Implement handling "DayTimeChanged" event:
            // Turn on light in the morning, turn off light at night.
        }

        public void Configure(Dictionary<string, object> settings)
        {
            Console.WriteLine("Light Configured");
            // Implement configuring light parameters (e.g., brightness).
        }

        public void ExecuteCommand(string command)
        {
            if (command == "On")
            {
                isOn = true;
                Console.WriteLine("Light manually turned on.");
            }
            else if (command == "Off")
            {
                isOn = false;
                Console.WriteLine("Light manually turned off.");
            }
            else
            {
                Console.WriteLine("Invalid command for Light.");
            }
            // Implement manual control of the light (turn on/off).
        }
    }
}