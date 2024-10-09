namespace BetterIRacingSDK;

// Define structs for IRacing Header
public struct IRacingHeader()
{
    // API information
    public int Version; // Version of the API
    public BetterIRacingSDKStatus Status; // Is IRacing connected or not (BetterIRacingSDKStatus)
    public int TickRate; // Ticks per second

    // SessionInfo information
    public int SessionInfoUpdate; // Update number
    public int SessionInfoLength; // Length in bytes of SessionInfo
    public int SessionInfoOffset; // Offset to where the SessionInfo starts
        
    // Buffer information
    public int VariableCount; // Length of variables in array (array can be found using varHeaderOffset)
    public int VariableHeaderOffset; // Offset in bytes to the array of variable headers
    public int BufferAmount; // The amount of data buffers (Always 3)
    public int BufferLength; // The length in bytes of a single buffer

    // Skip 4 bytes (2 integers)
    private int Skip1;
    private int Skip2;

    public int TickCount; // Gets the amount of ticks executed
}

// Define complete struct for data
public struct IRacingData
{
    public IRacingHeader Header;
    public IRacingData(IRacingHeader header)
    {
        Header = header;
    }
}


public enum BetterIRacingSDKStatus
{
    StatusDisconnected = 0, 
    StatusConnected = 1
}