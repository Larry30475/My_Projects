using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Authentication;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MyCalculator : ICalculator
    {
        public int iAdd(int x, int y)
        {
            try
            {
                checked
                {
                    int result = x + y;
                    Console.WriteLine("Funkcja iAdd została wywołana. Zostały wprowadzone dwie liczby: " + x + " oraz " + y + ". Zwrócone zostało " + result);
                    return result;
                }
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("Przepełnienie przy dodawaniu.", ex);
            }
        }

        public int iSub(int x, int y)
        {
            try
            {
                checked
                {
                    int result = x - y;
                    Console.WriteLine("Funkcja iSub została wywołana. Zostały wprowadzone dwie liczby: " + x + " oraz " + y + ". Zwrócone zostało " + result);
                    return result;
                }
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("Przepełnienie przy odejmowaniu.", ex);
            }
        }
        public int iMul(int x, int y)
        {
            try
            {
                checked
                {
                    int result = x * y;
                    Console.WriteLine("Funkcja iMul została wywołana. Zostały wprowadzone dwie liczby: " + x + " oraz " + y + ". Zwrócone zostało " + result);
                    return result;
                }
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("Przepełnienie przy mnozeniu.", ex);
            }
        }

        public int iDiv(int x, int y)
        {
            if (y == 0)
            {
                throw new FaultException("Nie mozna dzielić przez zero.");
            }
            try
            {
                checked
                {
                    int result = x / y;
                    Console.WriteLine("Funkcja iDiv została wywołana. Zostały wprowadzone dwie liczby: " + x + " oraz " + y + ". Zwrócone zostało " + result);
                    return result;
                }
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("Przepełnienie przy dzieleniu.", ex);
            }
        }

        public int iMod(int x, int y)
        {
            if (y == 0)
            {
                throw new FaultException("Nie można robić modulo przez zero");
            }
            try
            {
                checked
                {
                    int result = x % y;
                    Console.WriteLine("Funkcja iMod została wywołana. Zostały wprowadzone dwie liczby: " + x + " oraz " + y + ". Zwrócone zostało " + result);
                    return result;
                }
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("Przepełnienie przy operacji modulo.", ex);
            }
        }

        public async Task<PrimeNumbersResult> CountAndFindMaxPrimeNumbersAsync(BigInteger L1, BigInteger L2)
        {
            if (L1 >= L2 || L1 < 0)
            {
                throw new FaultException("Nieprawidłowe wartości argumentów.");
            }

            // Funkcja sprawdzająca, czy liczba jest pierwsza
            bool IsPrime(BigInteger n)
            {
                if (n < 2)
                {
                    return false;
                }
                else if (n == 2 || n == 3)
                {
                    return true;
                }
                else if (n % 2 == 0)
                {
                    return false;
                }

                for (BigInteger i = 3; i * i <= n; i += 2)
                {
                    if (n % i == 0)
                    {
                        return false;
                    }
                }

                return true;
            }

            // Wyliczenie ilości liczb pierwszych w zakresie
            BigInteger count = 0;
            // Wyliczenie największej liczby pierwszej w zakresie
            BigInteger largestPrime = -1;
            for (BigInteger i = L1; i <= L2; i++)
            {
                if (IsPrime(i))
                {
                    count++;
                    if (i > largestPrime)
                    {
                        largestPrime = i;
                    }
                }
            }
            Console.WriteLine("Funkcja CountAndFindMaxPrimeNumbersAsync została wywołana. Zostały wprowadzone dwie liczby: " + L1 + " oraz " + L2 + ". " +
                "Zwrócone zostało " + count + " jako liczba liczb pierwszych i " + largestPrime + " jako największa liczba pierwsza");
            return await Task.FromResult(new PrimeNumbersResult { Count = count, LargestPrime = largestPrime });
        }
    }
}
