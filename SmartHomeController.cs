using MySmartHome.Devices;

namespace MySmartHome
{
    public class SmartHomeController
    {
        public event Action<string>? OnDayTimeChanged;
        public event Action<int>? OnTemperatureChanged;
        public event Action? OnMotionDetected;

        private readonly List<ISmartDevice> _devices = new List<ISmartDevice>();
        private readonly EventLogger _logger = new EventLogger();
        
        public EventLogger Logger => _logger;

        public void RegisterDevice(ISmartDevice device)
        {
            string deviceName = device.GetType().Name;
            if (_devices.Contains(device))
            {
                _logger.Log($"Device {deviceName} is already registered.");
                Console.WriteLine($"Device {deviceName} is already registered.");
                return;
            }
            _devices.Add(device);
            Console.WriteLine($"Device registered: {deviceName}.");
            _logger.Log($"Device registered: {deviceName}.");
            // Implement adding a device to the devices list.
        }

        public void ChangeDayTime(string timeOfDay)
        {
            Console.WriteLine($"Event: Daytime changed to {timeOfDay}.");
            _logger.Log($"Daytime changed to {timeOfDay}.");
            SafeInvoke(OnDayTimeChanged, timeOfDay, nameof(OnDayTimeChanged));
        }

        public void ChangeTemperature(int temperature)
        {
            Console.WriteLine($"Event: Temperature changed to {temperature}°C.");
            _logger.Log($"Daytime changed to {temperature}°C.");
            SafeInvoke(OnTemperatureChanged, temperature, nameof(OnTemperatureChanged));
            // Implement triggering the OnTemperatureChanged event and logging the event.
        }

        public void DetectMotion()
        {
            Console.WriteLine($"Event: Motion detected.");
            _logger.Log($"Motion detected.");
            SafeInvoke(OnMotionDetected, nameof(OnMotionDetected));
            // Implement triggering the OnMotionDetected event and logging the event.
        }
    
        public void TriggerDevice(string deviceName, string command)
        {
            ISmartDevice? device = _devices.Find(d =>
                d.GetType().Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase));
            
            if (device is null)
            {
                Console.WriteLine($"Error: Device not found: {deviceName}");
                _logger.Log($"Error: Device not found: {deviceName}");
                return;
            }
            
            try
            {
                _logger.Log($"Command {command} sent to {device.GetType().Name}.");
                device.ExecuteCommand(command);
            }
            catch (Exception ex)
            {
                _logger.Log($"Command {command} failed on {device.GetType().Name}: {ex.Message}.");
                Console.WriteLine($"Command {command} failed on {device.GetType().Name}: {ex.Message}.");
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