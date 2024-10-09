using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace BetterIRacingSDK;

public class BetterIRacingSDK
{
    public BetterIRacingMemoryReader _memoryReader = new BetterIRacingMemoryReader();
    public bool isConnected()
    {
        if (_memoryReader.ReadMemory().Status == BetterIRacingSDKStatus.StatusConnected)
        {
            return true;
        }

        return false; 
    }

    public void ReadMemory()
    {
        Console.WriteLine(_memoryReader.ReadMemory().TickCount / _memoryReader.ReadMemory().TickRate);
    }
}