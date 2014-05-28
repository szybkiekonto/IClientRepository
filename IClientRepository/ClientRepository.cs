using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.ServiceModel;
using Contracts;

namespace IClientRepository
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ClientRepository : Contracts.IClientRepository
    {
        //public Lista accountList = new Lista(); 
        Baza accountList = new Baza();
        public ClientRepository()
        {
            ClientRepo osoba = new ClientRepo();
            osoba.address = "Sandomierz";
            osoba.IdClient = Guid.NewGuid();
            osoba.Lastname = "Laskowski";
            osoba.Name = "Tomek";
            osoba.PESEL = "92019203912";
            accountList.Add(osoba);

            osoba = new ClientRepo();
            osoba.address = "Kraków";
            osoba.IdClient = Guid.NewGuid();
            osoba.Lastname = "Popaprany";
            osoba.Name = "Wojtek";
            osoba.PESEL = "90013203912";
            accountList.Add(osoba);

            osoba = new ClientRepo();
            osoba.address = "Wieliczka";
            osoba.IdClient = Guid.NewGuid();
            osoba.Lastname = "Jagodzińska";
            osoba.Name = "Justyna";
            osoba.PESEL = "92100203912";
            accountList.Add(osoba);
        }
        public Guid CreateClient(string name, string lastname, string pesel, string adres)
        {
            ClientRepo person = new ClientRepo();
            person.address = adres;
            person.IdClient = Guid.NewGuid();
            person.Lastname = lastname;
            person.Name = name;
            person.PESEL = pesel;
            accountList.Add(person);
            return person.IdClient;
        }

        public ClientRepo GetClientInformationByName(string name, string lastName)
        {
            ClientRepo FoundPerson = accountList.Find(name, lastName);
            return FoundPerson;
        }

        public ClientRepo GetClientInformationById(Guid Id)
        {
            ClientRepo FoundPerson = accountList.Find(Id);
            return FoundPerson;
        }

        public void RemoveClientByName(string name, string lastname)
        {
            accountList.RemoveClientByName(name, lastname);
        }

        public void RemoveClientById(Guid Id)
        {
            accountList.RemoveClientById(Id);
        }

        /*public void ShowAll()
        {
            foreach(ClientRepo person in accountList.accountList)
            {
                Console.WriteLine(person.IdClient + " " + person.Name + " " + person.Lastname + " " + person.address + " " + person.PESEL);
            }
        }*/

    }

}
