using ECommon.Configurations;
using ECommon.Socketing;
using EQueue.Configurations;
using EQueue.NameServer;
using System;
using System.Net;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace EQueueNameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeEQueue();
            var port = 9493;
            var envPort = Environment.GetEnvironmentVariable("NAMESERVER_PORT");
            if (!string.IsNullOrWhiteSpace(envPort) && int.TryParse(envPort, out int iPort))
            {
                port = iPort;
            }
            var bindingAddress = Environment.GetEnvironmentVariable("NAMESERVER_BINDINGADDRESS"); //ConfigurationManager.AppSettings["bindingAddress"];
            var bindingIpAddress = string.IsNullOrEmpty(bindingAddress) ? SocketUtils.GetLocalIPV4() : IPAddress.Parse(bindingAddress);
            new NameServerController(new NameServerSetting
            {
                BindingAddress = new IPEndPoint(bindingIpAddress, port)
            }).Start();
            Console.ReadLine();
        }

        static void InitializeEQueue()
        {
            var configuration = ECommonConfiguration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler()
                .RegisterEQueueComponents()
                .BuildContainer();
        }
    }
}
