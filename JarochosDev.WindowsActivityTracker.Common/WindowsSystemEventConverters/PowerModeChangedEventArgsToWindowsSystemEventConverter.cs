﻿using JarochosDev.Utilities.Net.NetStandard.Common.Converters;
using JarochosDev.WindowsActivityTracker.Common.Models;
using JarochosDev.WindowsActivityTracker.Common.Utilities;
using Microsoft.Win32;

namespace JarochosDev.WindowsActivityTracker.Common.WindowsSystemEventConverters
{
    public class PowerModeChangedEventArgsToWindowsSystemEventConverter : IObjectConverter<PowerModeChangedEventArgs, IWindowsSystemEvent>
    {
        public IWindowsSystemEventConstructor WindowsSystemEventConstructor { get; }

        public PowerModeChangedEventArgsToWindowsSystemEventConverter(IWindowsSystemEventConstructor windowsSystemEventConstructor)
        {
            WindowsSystemEventConstructor = windowsSystemEventConstructor;
        }

        public IWindowsSystemEvent Convert(PowerModeChangedEventArgs sessionSwitchEventArgs)
        {
            var windowsSystemEventType = GetWindowsSystemEventType(sessionSwitchEventArgs.Mode);
            var eventMessage = $"PowerModeChangedEvent:{sessionSwitchEventArgs.Mode}";
            return WindowsSystemEventConstructor.Construct(eventMessage, windowsSystemEventType);
        }

        private WindowsSystemEventType GetWindowsSystemEventType(PowerModes mode)
        {
            switch (mode)
            {
                case PowerModes.Suspend:
                    return WindowsSystemEventType.StopWorking;
                case PowerModes.Resume:
                    return WindowsSystemEventType.StartWorking;
                case PowerModes.StatusChange:
                    return WindowsSystemEventType.None;
                default:
                    return WindowsSystemEventType.NotImplemented;
            }
        }
    }
}