using MySmartHome.Enums;

namespace MySmartHome.Devices
{
    public interface ISmartDevice
    {
        // void HandleEvent(string eventType, object eventData);
        // void Configure(Dictionary<string, object> settings);
        // void ExecuteCommand(string command);
        string Name { get; }
        
        void OnDayTimeChanged(DayTime timeOfDay);
        void OnTemperatureChanged(int temperature);
        void OnMotionDetected();

        void Configure(Dictionary<string, object> settings);
        void ExecuteCommand(string command);
    }
}