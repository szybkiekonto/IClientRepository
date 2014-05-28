using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using IClientRepository.Domain;
using Contracts;
using NHibernate.Mapping;

namespace IClientRepository
{
    public class Baza
    {
        public void Add(ClientRepo person)
        {
            var osoba = new Clients { Name = person.Name, Lastname = person.Lastname, address = person.address, PESEL = person.PESEL };
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(osoba);
                    transaction.Commit();
                }
            }
        }

        public ClientRepo Find(string name, string lastname)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    ClientRepo wynik = new ClientRepo();
                    var result = session.QueryOver<Clients>().Where(x => x.Name == name && x.Lastname == lastname).SingleOrDefault();
                    wynik.Name = result.Name;
                    wynik.Lastname = result.Lastname;
                    wynik.address = result.address;
                    wynik.PESEL = result.PESEL;
                    wynik.IdClient = result.IdClient;

                    return wynik;
                }
            }
        }

        public ClientRepo Find(Guid Id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    ClientRepo wynik = new ClientRepo();
                    var result = session.QueryOver<Clients>().Where(x => x.IdClient == Id).SingleOrDefault();

                    wynik.Name = result.Name;
                    wynik.Lastname = result.Lastname;
                    wynik.address = result.address;
                    wynik.PESEL = result.PESEL;
                    wynik.IdClient = result.IdClient;

                    return wynik;
                }
            }
        }

        public void RemoveClientByName(string name, string lastname)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var result = session.QueryOver<Clients>().Where(x => x.Name == name && x.Lastname == lastname).SingleOrDefault();
                    session.Delete(result);
                    transaction.Commit();
                }
            }
        }

        public void RemoveClientById(Guid Id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var result = session.QueryOver<Clients>().Where(x => x.IdClient == Id).SingleOrDefault();
                    session.Delete(result);
                    transaction.Commit(); 
                }
            }
        }
       
    }
}
