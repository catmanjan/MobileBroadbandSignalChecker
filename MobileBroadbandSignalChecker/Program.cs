using System;
using MbnApi;

namespace MobileBroadbandSignalChecker
{
    class Program
    {
        private static void Main()
        {
            while (true)
            {
                var mbnInfMgr = new MbnInterfaceManager();
                var infMgr = (IMbnInterfaceManager)mbnInfMgr;
                var interfaces = (IMbnInterface[])infMgr.GetInterfaces();

                foreach (var mobileInterface in interfaces)
                {
                    var capabilities = mobileInterface.GetInterfaceCapability();
                    var provider = mobileInterface.GetHomeProvider();
                    var readyState = mobileInterface.GetReadyState();
                    var radio = mobileInterface as IMbnRadio;
                    var signal = mobileInterface as IMbnSignal;
                    var signalStrength = signal.GetSignalStrength();
                    var signalStrengthDb = -113 + (int)signalStrength * 2;

                    Console.WriteLine("Manufacturer:        " + capabilities.manufacturer);
                    Console.WriteLine("Model:               " + capabilities.model);
                    Console.WriteLine("DeviceID:            " + capabilities.deviceID);
                    Console.WriteLine("FirmwareInfo:        " + capabilities.firmwareInfo);
                    Console.WriteLine("Ready State :        " + readyState);
                    if (radio != null)
                    {
                        Console.WriteLine("HardwareRadioState:  " + radio.HardwareRadioState);
                        Console.WriteLine("SoftwareRadioState:  " + radio.SoftwareRadioState);
                    }
                    Console.WriteLine("InterfaceID:         " + mobileInterface.InterfaceID);
                    Console.WriteLine("Provider:            " + provider.providerName);
                    Console.WriteLine("ProviderID:          " + provider.providerID);
                    Console.WriteLine("ProviderState:       " + provider.providerState);

                    if (signalStrength != (uint)MBN_SIGNAL_CONSTANTS.MBN_RSSI_UNKNOWN)
                    {
                        Console.WriteLine("Signal Strength:     " + signalStrengthDb + " dBm");
                    }
                    else
                    {
                        Console.WriteLine("Signal Strength:         unknown");
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("Press enter to scan again, or close the window to exit");
                Console.ReadKey();
            }
        }
    }
}