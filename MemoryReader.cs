using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace BetterIRacingSDK;

public class BetterIRacingMemoryReader
{
    private const string MemoryMapLocation = "Local\\IRSDKMemMapFileName";
    public MemoryMappedFile _memoryFile;
    public MemoryMappedViewAccessor _memoryAccessor;
    public long _lastTick = 0;
    public IRacingData lastData;
    public int maxTickRate = 0;

    public BetterIRacingMemoryReader()
    {
        // Open the iRacing Shared Memory File and create an accessor to access the values in the file
        _memoryFile = MemoryMappedFile.OpenExisting(MemoryMapLocation);
        _memoryAccessor = _memoryFile.CreateViewAccessor();
    }
    
    public BetterIRacingMemoryReader(int maxTickRate)
    {
        // Open the iRacing Shared Memory File and create an accessor to access the values in the file
        _memoryFile = MemoryMappedFile.OpenExisting(MemoryMapLocation);
        _memoryAccessor = _memoryFile.CreateViewAccessor();
        this.maxTickRate = maxTickRate;
    }
    
    

    // Reads the data from the memory and sets it to lastData
    private void ReadFromMemory()
    {
        IRacingHeader header;
        _memoryAccessor.Read(0, out header);
        IRacingData data = new IRacingData(header);
        this.lastData = data;
    }

    public IRacingData ReadMemory()
    {
        // If there is no data defined yet, read it from the shared memory
        if (_lastTick == 0)
        {
            this.ReadFromMemory();
            _lastTick = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            return this.lastData;
        }
        else
        {
            // Calculate the maximum tick rate in milliseconds
            int maxTickRateMilliseconds = 1000 / this.maxTickRate;

            // Calculate the tick rate based on last data but not exceeding max tick rate
            int tickRate = Math.Max(1000 / this.lastData.Header.TickRate, maxTickRateMilliseconds);
            long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // Calculate the elapsed time since the last tick
            long elapsedTime = currentTime - _lastTick;

            // Check if the elapsed time is greater than or equal to the tick rate
            if (elapsedTime >= tickRate)
            {
                Console.WriteLine("Tijd verstreken sinds laatste tick: " + elapsedTime);
                // Read from memory if enough time has passed
                this.ReadFromMemory();
                _lastTick = currentTime;
            }

            return this.lastData; // Return the last data read
        }
    }

    public void Dispose()
    {
        //Clean up the memory when done (and the variables are initialized)
        _memoryAccessor?.Dispose();
        _memoryFile?.Dispose();
    }
    
}