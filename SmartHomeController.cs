using System;
using System.Collections.Generic;
using MySmartHome.Devices;

namespace SmartHomeSystem
{
    public class SmartHomeController
    {
        public event Action<string> OnDayTimeChanged;
        public event Action<int> OnTemperatureChanged;
        public event Action OnMotionDetected;

        private readonly List<ISmartDevice> devices = new List<ISmartDevice>();
        private readonly EventLogger logger = new EventLogger();

        public void RegisterDevice(ISmartDevice device)
        {
            devices.Add(device);
            string deviceName = device.GetType().Name;
            Console.WriteLine($"Device registered: {deviceName}.");
            logger.Log($"Device registered: {deviceName}.");
            // Implement adding a device to the devices list.
        }

        public void ChangeDayTime(string timeOfDay)
        {
            Console.WriteLine($"Event: Daytime changed to {timeOfDay}.");
            logger.Log($"Daytime changed to {timeOfDay}.");
            OnDayTimeChanged?.Invoke(timeOfDay);
        }

        public void ChangeTemperature(int temperature)
        {
            Console.WriteLine($"Event: Temperature changed to {temperature}°C.");
            logger.Log($"Daytime changed to {temperature}°C.");
            OnTemperatureChanged?.Invoke(temperature);
            // Implement triggering the OnTemperatureChanged event and logging the event.
        }

        public void DetectMotion()
        {
            Console.WriteLine($"Event: Motion detected.");
            logger.Log($"Motion detected.");
            OnMotionDetected?.Invoke();
            // Implement triggering the OnMotionDetected event and logging the event.
        }

        public void TriggerDevice(string deviceName, string command)
        {
            bool found = false;
            foreach (var device in devices)
            {
                if (device.GetType().Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    device.ExecuteCommand(command);
                    logger.Log($"Command {command} sent to {device.GetType().Name}.");
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Device not found: {deviceName}.");
            }
            // Implement finding the device by name, calling ExecuteCommand, and logging.
        }

        public void ShowLog()
        {
            logger.ShowLog();
            // Implement showing the event log via logger.
        }
    }
}