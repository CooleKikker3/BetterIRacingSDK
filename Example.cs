using System.Security.AccessControl;

namespace BetterIRacingSDK;

public class Example
{
    public static void Main(String[] args)
    {
        BetterIRacingSDK sdk = new BetterIRacingSDK();

        while (true)
        {
            if (sdk.isConnected())
            {
                Console.WriteLine("IRacing is verbonden!");
            }
            else
            {
                Console.WriteLine("IRacing is niet verbonden!");
            }
            
            Thread.Sleep(100);
        }
    }
}