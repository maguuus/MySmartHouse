using MySmartHome.Devices;
using MySmartHome.Enums;

namespace MySmartHome
{
    public class SmartHomeController
    {
        public event Action<DayTime>? DayTimeChanged;
        public event Action<int>? TemperatureChanged;
        public event Action? MotionDetected;

        private readonly List<ISmartDevice> _devices = new List<ISmartDevice>();
        private readonly EventLogger _logger = new EventLogger();
        
        public EventLogger Logger => _logger;

        public void RegisterDevice(ISmartDevice device)
        {
            if (_devices.Exists(d => d.Name.Equals(device.Name, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.Log($"Device {device.Name} is already registered.");
                Console.WriteLine($"Device {device.Name} is already registered.");
                return;
            }
            _devices.Add(device);
            Console.WriteLine($"Device registered: {device.Name}.");
            _logger.Log($"Device registered: {device.Name}.");
            // Implement adding a device to the devices list.
        }

        public void ChangeDayTime(string timeOfDay)
        {
            if (Enum.TryParse<DayTime>(timeOfDay, true, out var dayTime))
            {
                Console.WriteLine($"Event: Daytime changed to {dayTime}.");
                _logger.Log($"Daytime changed to {dayTime}.");
                SafeInvoke(DayTimeChanged, dayTime, nameof(DayTimeChanged));
            }
            else
            {
                _logger.Log($"Error: Invalid daytime input: {timeOfDay}.");
                Console.WriteLine($"Error: Invalid daytime input: {timeOfDay}. Excepted 'Morning' or 'Night'.");
            }
        }

        public void ChangeTemperature(int temperature)
        {
            Console.WriteLine($"Event: Temperature changed to {temperature}°C.");
            _logger.Log($"Daytime changed to {temperature}°C.");
            SafeInvoke(TemperatureChanged, temperature, nameof(TemperatureChanged));
            // Implement triggering the OnTemperatureChanged event and logging the event.
        }

        public void DetectMotion()
        {
            Console.WriteLine($"Event: Motion detected.");
            _logger.Log($"Motion detected.");
            SafeInvoke(MotionDetected, nameof(MotionDetected));
            // Implement triggering the OnMotionDetected event and logging the event.
        }
    
        public void TriggerDevice(string deviceName, string command)
        {
            ISmartDevice? device = _devices.FirstOrDefault(d =>
                d.Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase));
            
            if (device is null)
            {
                Console.WriteLine($"Error: Device not found: {deviceName}");
                _logger.Log($"Error: Device not found: {deviceName}");
                return;
            }
            
            try
            {
                _logger.Log($"Command {command} sent to {device.Name}.");
                device.ExecuteCommand(command);
            }
            catch (Exception ex)
            {
                _logger.Log($"Command {command} failed on {device.Name}: {ex.Message}.");
                Console.WriteLine($"Command {command} failed on {device.Name}: {ex.Message}.");
            }   
            // Implement finding the device by name, calling ExecuteCommand, and logging.
        }

        public void ShowLog()
        {
            _logger.ShowLog();
            // Implement showing the event log via logger.
        }

        private void SafeInvoke<T>(Action<T>? eventHandler, T arg, string eventName)
        {
            if (eventHandler is null)
                return;

            foreach (var handler in eventHandler.GetInvocationList())
            {
                try
                {
                    ((Action<T>)handler).Invoke(arg);
                }
                catch (Exception ex)
                {
                    _logger.Log($"Error in subscriber of {eventName}: {ex.Message}.");
                }   
            }
        }

        private void SafeInvoke(Action? eventHandler, string eventName)
        {
            if (eventHandler is null)
                return;

            foreach (var handler in eventHandler.GetInvocationList())
            {
                try
                {
                    ((Action)handler).Invoke();
                }
                catch (Exception ex)
                {
                    _logger.Log($"Error in subscriber of {eventName}: {ex.Message}.");
                }
            }
        }
        
    }
}