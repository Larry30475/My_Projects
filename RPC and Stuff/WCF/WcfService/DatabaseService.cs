using System;
using System.Collections;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DatabaseService : IDatabaseService
    {
        private ArrayList _users = new ArrayList();
        private int _highestID = 3;

        public DatabaseService()
        {
            _users.Add(new User { ID = 1, Name = "Wiktor", Age = 12, Email = "wiktor@onet.pl" });
            _users.Add(new User { ID = 2, Name = "Mariusz", Age = 27, Email = "marisuz@gazeta.pl" });
        }

        public User AddUser(string name, int age, string email)
        {
            Console.WriteLine($"...called AddUser");

            foreach (User user in _users)
            {
                if (user.Name == name && user.Age == age && user.Email == email)
                {
                    throw new FaultException("Użytkownik już jest w bazie.");
                }
            }

            var newUser = new User { ID = _highestID, Name = name, Age = age, Email = email };
            _users.Add(newUser);
            _highestID++;
            return newUser;
        }

        public User GetUser(int ID)
        {
            Console.WriteLine($"...called GetUser");
            foreach (User user in _users)
            {
                if (user.ID == ID)
                {
                    return user;
                }
            }
            throw new FaultException("Użytkownik nie istnieje w bazie.");
        }

        public User UpdateUser(int ID, string name, int age, string email)
        {
            Console.WriteLine($"...called UpdateUser");

            foreach (User user in _users)
            {
                if (user.ID == ID)
                {
                    user.Name = name;
                    user.Age = age;
                    user.Email = email;
                    return user;
                }
            }
            throw new FaultException("Użytkownik nie istnieje w bazie.");
        }

        public User DeleteUser(int ID)
        {
            Console.WriteLine($"...called DeleteUser");

            foreach (User user in _users)
            {
                if (user.ID == ID)
                {
                    _users.Remove(user);
                    Console.WriteLine($"...returning {user}");
                    return user;
                }
            }
            throw new FaultException("Użytkownik nie istnieje w bazie.");
        }

        public ArrayList GetAllUsers()
        {
            Console.WriteLine($"...called GetAllUsers");
            return _users;
        }



        public int GetUserDatabaseSize()
        {
            Console.WriteLine($"...called GetUserDatabaseSize()");
            return _users.Count;
        }

        public async Task<ArrayList> FilterBy(string name)
        {
            Console.WriteLine($"...called FilterBy");

            var result = new ArrayList();
            foreach (User user in _users)
            {
                if (user.Name == name)
                {
                    result.Add(user);
                }
            }

            await Task.Delay(10000);
            return result;
        }
    }
}