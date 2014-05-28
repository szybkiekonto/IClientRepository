using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using log4net;
using log4net.Repository.Hierarchy;
using System.ServiceModel;
using Contracts;
using System.ServiceModel.Description;
using System.Timers;
using IClientRepository.Domain;

namespace IClientRepository
{
    class Program
    {
        public const string serviceAddress = "net.tcp://localhost:50000/IClientRepository";
        public const string serviceRepositoryAddress = "net.tcp://localhost:11900/IServiceRepository";
        public const string serviceName = "IClientRepository";
        public static IServiceRepository repository { get; set; }
        public static IAccountRepository repository2 { get; set; }

        static void Main(string[] args)
        {
            LoadHibernateCfg();
            ClientRepository test = new ClientRepository();
            //test.ShowAll(); tylo dla lista 
            //ClientRepo wynik = test.GetClientInformationByName("Wojtek", "Popaprany");
            //Console.WriteLine(wynik.IdClient);
            //test.ShowAll(); 
            //Logger
            log4net.Config.XmlConfigurator.Configure();
            
            //Configuration
            ClientRepository accountRep = new ClientRepository();
            ServiceHost sh = new ServiceHost(accountRep, new Uri[] { new Uri(serviceAddress) });
            sh.AddServiceEndpoint(typeof(Contracts.IClientRepository), new NetTcpBinding(SecurityMode.None), serviceAddress);
           
            //Service starting
            sh.Open();
            Logger.Info("IClientRepository started!");


            //Register service in IServiceRepository
            ChannelFactory<IServiceRepository> cf = new ChannelFactory<IServiceRepository>(new NetTcpBinding(SecurityMode.None), serviceRepositoryAddress);
            repository = cf.CreateChannel();
            Logger.Info("Connection with IServiceRepository completed!");
            var timer = new System.Threading.Timer(e => imAlive(repository), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            repository.registerService(serviceName, serviceAddress.Replace(serviceRepositoryAddress, serviceName));
            Logger.Info("Service registered!");

            //Przykładowe korzystanie z IAccountRepository
            /*
            //pobieranie adresu z servicerepository
            string accountAdress = repository.getServiceAddress("AccountRepository");

            ChannelFactory<IAccountRepository> cf2 = new ChannelFactory<IAccountRepository>(new NetTcpBinding(SecurityMode.None), accountAdress);
            repository2 = cf2.CreateChannel();
            Logger.Info("Connection with accountRepository completed!");
            Guid clientId = new Guid();
            AccountDetails details = new AccountDetails();
            Console.WriteLine(repository2.CreateAccount(clientId, details));
            */
            //Click to close service
            Console.ReadLine();

            //Service closing
            Logger.Info("IClientRepository closing!");
            Console.Read();
        }

        private static void imAlive(IServiceRepository serviceRepository)
        {
            serviceRepository.isAlive(serviceRepositoryAddress);
            Logger.Info("Sending message IamAlive");
        }

        public static void LoadHibernateCfg()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Clients).Assembly);
            new SchemaExport(cfg).Execute(true, true, false);

        }
    }
    public static class Logger
    {
        public static void Info(string Message)
        {
            Console.WriteLine(Message);
        }
    }
}
