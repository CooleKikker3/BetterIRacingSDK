using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace BetterIRacingSDK;

public class BetterIRacingMemoryReader
{
    private const string MemoryMapLocation = "Local\\IRSDKMemMapFileName";
    public MemoryMappedFile _memoryFile;
    public MemoryMappedViewAccessor _memoryAccessor;

    public BetterIRacingMemoryReader()
    {
        // Open the iRacing Shared Memory File and create an accessor to access the values in the file
        _memoryFile = MemoryMappedFile.OpenExisting(MemoryMapLocation);
        _memoryAccessor = _memoryFile.CreateViewAccessor();
    }

    public IRacingHeader ReadMemory()
    {
        IRacingHeader header;
        _memoryAccessor.Read(0, out header);
        return header;
    }

    public void Dispose()
    {
        //Clean up the memory when done (and the variables are initialized)
        _memoryAccessor?.Dispose();
        _memoryFile?.Dispose();
    }
    
}