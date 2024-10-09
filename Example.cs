using System.Security.AccessControl;

namespace BetterIRacingSDK;

public class Example
{
    public static void Main(String[] args)
    {
        BetterIRacingSDK sdk = new BetterIRacingSDK();
        if (sdk.isConnected())
        {
            sdk.ReadMemory();
        }
        else
        {
            Console.WriteLine("Not connected!");
        }
    }
}