using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharpHomework.Model;
using CSharpHomework.Tools.Managers;

namespace CSharpHomework.Tools.DataStorage
{
    internal class SerializedDataStorage:IDataStorage
    {
        private readonly List<Person> _users;

        internal SerializedDataStorage()
        {
            try
            {
                _users = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = new List<Person>();
                //todo create 50 users

                

                for (int i = 0; i < 50; i++)
                {
                    Person person = generatePerson();//new Person();
                    AddUser(person);
                }



            }
        }


        private Person generatePerson()
        {

            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz";

            DateTime startDate = new DateTime(DateTime.Now.Year - 100, DateTime.Now.Month, DateTime.Now.Day);
            DateTime endDate = DateTime.Now;
            TimeSpan timeSpan = endDate - startDate;



            int length = random.Next(3, 7);
            string name = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            name = char.ToUpper(name[0]) + name.Substring(1);
            

            length = random.Next(3, 7);
            string surname = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            surname = char.ToUpper(surname[0]) + surname.Substring(1);

            length = random.Next(3, 7);
            string email = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            email += "@gmail.com";


            TimeSpan newSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;

            return new Person(name,surname,email,newDate);
        }

        public bool UserExists(string login)
        {
            return _users.Exists(u => u.Name == login);
        }

        public Person GetUserByLogin(string login)
        {
            return _users.FirstOrDefault(u => u.Name == login);
        }

        public void AddUser(Person user)
        {
            _users.Add(user);
            
            SaveChanges();

        }

        public void RemoveUser(Person user)
        {
            _users.Remove(user);

            SaveChanges();

        }

        public List<Person> UsersList
        {
            get { return _users.ToList(); }
        }

        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }
    }
}

