﻿using JarochosDev.Utilities.Net.NetStandard.Common.Converters;
using JarochosDev.WindowsActivityTracker.Common.Models;
using JarochosDev.WindowsActivityTracker.Common.Observers;
using JarochosDev.WindowsActivityTracker.Common.Test.Fakes;
using Microsoft.Win32;
using Moq;
using NUnit.Framework;

namespace JarochosDev.WindowsActivityTracker.Common.Test.Observers
{
    class SessionEndedObserverTest
    {
        private Mock<IWindowsSystemEventHandler> _mockWindowsSystemEventHandler;
        private Mock<IObjectConverter<SessionEndedEventArgs, IWindowsSystemEvent>> _mockConverter;
        private SessionEndedObserver _observer;

        [SetUp]
        public void SetUp()
        {
            _mockWindowsSystemEventHandler = new Mock<IWindowsSystemEventHandler>();
            _mockConverter = new Mock<IObjectConverter<SessionEndedEventArgs, IWindowsSystemEvent>>();
            _observer = new SessionEndedObserver(_mockWindowsSystemEventHandler.Object, _mockConverter.Object);
        }

        [Test]
        public void Test_Constructor()
        {
            Assert.AreSame(_mockWindowsSystemEventHandler.Object, _observer.WindowsSystemEventHandler);
            Assert.AreSame(_mockConverter.Object, _observer.SessionEndedToSystemEventConverter);
        }

        [Test]
        public void Test_OnNext_Calls_EventHandler_And_Converter()
        {
            var windowsSystemEvent = new FakeWindowsSystemEvent();

            var mockToConvert = new Mock<SessionEndedEventArgs>(null);
            _mockConverter.Setup(c => c.Convert(mockToConvert.Object)).Returns(windowsSystemEvent);
            _mockWindowsSystemEventHandler.Setup(h => h.Handle(windowsSystemEvent));

            _observer.OnNext(mockToConvert.Object);

            _mockWindowsSystemEventHandler.VerifyAll();
        }
    }
}
