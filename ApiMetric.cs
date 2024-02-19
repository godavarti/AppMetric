using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace APIMetric
{
    using System.Net.Sockets;
    using System.Reflection.Metadata.Ecma335;
    namespace apimetric
    {
        public interface IApicall
        {
            string action { get; set; }
            long elapsedMS { get; set; }
            int userid { get; set; }
            bool succcess { get; set; }
 
        }
 
        public interface IApiHandler
        {
            void apiv1callback(IApicall api);
            Task Apiv1CallbackAsync(IApicall api);
        }
 
 
        public class Apicall : IApicall
        {
 
            string _action = "";
            long _elapsedMS;
            int _userid;
            bool _succcess;
            public string action
            {
                get { return _action; }
                set { _action = value; }
            }
            public long elapsedMS
            {
                get { return _elapsedMS; }
                set { _elapsedMS = value; }
            }
            public int userid
            {
                get { return _userid; }
                set { _userid = value; }
            }
            public bool succcess
            {
                get { return _succcess; }
                set { _succcess = value; }
            }
        //constructor 
            public Apicall()
            {
 
            }
 
        }
 
        public class ApiHandler : IApiHandler
        {
 
            public static long _totalcalls = 0;
            public static long _totelapsedMS = 0;
            public static long _totalsuccess = 0;
            private static readonly object _lock = new object();
            public async void apiv1callback(IApicall api)
            {
                _totalcalls = _totalcalls + 1;
                _totelapsedMS = _totelapsedMS + api.elapsedMS;
                if (api.succcess == true)
                {
                    _totalsuccess = _totalsuccess + 1;
 
                }
            }
 
            public async Task Apiv1CallbackAsync(IApicall api)
            {
                // Simulate API call processing
                await Task.Delay(10);
 
                // Update metrics (thread-safe)
                lock (_lock)
                {
                    _totalcalls++;
                    _totelapsedMS += api.elapsedMS;
                    if (api.succcess)
                        _totalsuccess++;
                }
            }
 
            public static string getmetrc()
            {
                return "totcalls:" + _totalcalls.ToString() + "  totelpsedMS:" + _totelapsedMS.ToString() + " totsuccalls:" + Convert.ToString(_totalsuccess);
            }
        }
    }
 
 
}