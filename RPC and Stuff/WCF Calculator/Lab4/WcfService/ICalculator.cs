using System.Numerics;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
    public interface ICalculator
    {
        [OperationContract]
        int iAdd(int val1, int val2);
        [OperationContract]
        int iSub(int val1, int val2);
        [OperationContract]
        int iMul(int val1, int val2);
        [OperationContract]
        int iDiv(int val1, int val2);
        [OperationContract]
        int iMod(int val1, int val2);
        [OperationContract]
        Task<PrimeNumbersResult> CountAndFindMaxPrimeNumbersAsync(BigInteger L1, BigInteger L2);
    }

    public class PrimeNumbersResult
    {
        public BigInteger Count { get; set; }
        public BigInteger LargestPrime { get; set; }
    }
}
