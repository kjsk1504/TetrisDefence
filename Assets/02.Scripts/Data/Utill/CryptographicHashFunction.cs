using System.Security.Cryptography;
using System.Text;

namespace TetrisDefence.Data.Utill
{
    /// <summary>
    /// 암호학적 해시 함수 (<see cref="MD5"/>, <see cref="SHA256"/>, <see cref="HMACSHA256"/>)
    /// </summary>
    public static class CryptographicHashFunction
    {
        /// <summary>
        /// 입력 문자열의 <see cref="MD5"/> 해시 값을 계산
        /// </summary>
        /// <param name="inputString"> 입력 문자열 </param>
        /// <returns> 해시 값 </returns>
        public static string CalculateMD5(string inputString)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder stringBuilder = new StringBuilder();

                foreach (byte hashByte in hashBytes)
                {
                    stringBuilder.Append(hashByte.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// 입력 문자열의 <see cref="SHA256"/> 해시 값을 계산
        /// </summary>
        /// <param name="inputString"> 입력 문자열 </param>
        /// <returns> 해시 값</returns>
        public static string CalculateSHA256(string inputString)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder stringBuilder = new StringBuilder();

                foreach (byte hashByte in hashBytes)
                {
                    stringBuilder.Append(hashByte.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// 입력 문자열의 <see cref="HMACSHA256"/> 해시 값을 계산
        /// </summary>
        /// <param name="inputString"> 입력 문자열 </param>
        /// <param name="secretKey"> 비밀 키 </param>
        /// <returns> 해시 값</returns>
        public static string CalculateHMACSHA256(string inputString, string secretKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hashBytes = hmac.ComputeHash(inputBytes);

                StringBuilder stringBuilder = new StringBuilder();

                foreach (byte hashByte in hashBytes)
                {
                    stringBuilder.Append(hashByte.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
