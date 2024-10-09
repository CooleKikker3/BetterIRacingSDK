using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace BetterIRacingSDK;

public class BetterIRacingSDK
{
    public BetterIRacingMemoryReader _memoryReader;

    public BetterIRacingSDK(int maxTickRate)
    {
        this._memoryReader = new BetterIRacingMemoryReader(maxTickRate);
    }

    public BetterIRacingSDK()
    {
        this._memoryReader = new BetterIRacingMemoryReader();
    }

    public bool isConnected()
    {
        IRacingData data = _memoryReader.ReadMemory();
        if (data.Header.Status == BetterIRacingSDKStatus.StatusConnected)
        {
            return true;
        }
        
        return false;
    }
}