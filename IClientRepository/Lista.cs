using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;

namespace IClientRepository
{
    public class Lista
    {
        public List<ClientRepo> accountList = new List<ClientRepo>();
        public Lista()
        {
            //List<ClientRepo> accountList = new List<ClientRepo>(); 
        }
        public void Add(ClientRepo person)
        {
            accountList.Add(person);
        }
        public ClientRepo Find(string name, string lastName)
        {
            ClientRepo FoundPerson = accountList.Find(oElement => oElement.Name.Equals(name) & oElement.Lastname.Equals(lastName));
            return FoundPerson;
        }
        public ClientRepo Find(Guid Id)
        {
            ClientRepo FoundPerson = accountList.Find(oElement => oElement.IdClient.Equals(Id));
            return FoundPerson;
        }
        public void RemoveClientByName(string name, string lastname)
        {
            accountList.Remove(accountList.Find(x => x.Name.Equals(name) & x.Lastname.Equals(lastname)));
        }
        public void RemoveClientById(Guid Id)
        {
            accountList.Remove(accountList.Find(x => x.IdClient.Equals(Id)));
        }
    }
}
