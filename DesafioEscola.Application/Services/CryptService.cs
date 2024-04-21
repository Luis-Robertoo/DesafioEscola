using DesafioEscola.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace DesafioEscola.Application.Services
{
    public class CryptService : ICryptService
    {
        private HashAlgorithm _algoritmo;

        public CryptService()
        {
            _algoritmo = SHA256.Create(); 
        }

        public string EncryptPassword(string password)
        {
            var encodedValue = Encoding.UTF8.GetBytes(password);
            var encryptedPassword = _algoritmo.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
