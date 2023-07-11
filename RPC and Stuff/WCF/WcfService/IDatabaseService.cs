using System.Collections;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
    [ServiceKnownType(typeof(User))]
    public interface IDatabaseService
    {
        [OperationContract]
        User AddUser(string name, int age, string email);

        [OperationContract]
        User GetUser(int ID);

        [OperationContract]
        User UpdateUser(int ID, string name, int age, string email);

        [OperationContract]
        User DeleteUser(int ID);

        [OperationContract]
        int GetUserDatabaseSize();

        [OperationContract]
        ArrayList GetAllUsers();

        [OperationContract]
        Task<ArrayList> FilterBy(string name);
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
