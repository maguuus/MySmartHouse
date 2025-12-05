using System;
using System.Collections.Generic;

namespace MySmartHome.Devices
{
    public interface ISmartDevice
    {
        void HandleEvent(string eventType, object eventData);
        void Configure(Dictionary<string, object> settings);
        void ExecuteCommand(string command);
    }
}