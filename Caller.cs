using APIMetric.apimetric;
 
using System.Net.NetworkInformation;
 
public class Caller
{
    public static void Main()
    {
        string action = "abc";
        long elapsedMS = 0;
        int userid = 0;
        bool succcess = true;
        IApiHandler ah = new ApiHandler();
        IApicall api = new Apicall();
 
        Random ran = new Random();
        for (int i = 0; i < 100; i++)
        {
            api.action = "abc" + Convert.ToString(i);
            api.elapsedMS = ran.Next(10);
            api.succcess = api.elapsedMS > 5 ? false : true;
            //Sync version 
           // ah.apiv1callback(api);
            //Async Version for scale 
           var task= Task.Run(()=> ah.Apiv1CallbackAsync(api));
            task.Wait();
 
        }
 
 
        Console.WriteLine(ApiHandler.getmetrc());
 
 
    }
}