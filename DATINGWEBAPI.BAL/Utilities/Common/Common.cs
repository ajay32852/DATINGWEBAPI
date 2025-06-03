using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DATINGWEBAPI.BAL.Utilities.Common
{
    public static class Common
    {
        /// <summary>
        /// Validate if a password will generate the passed in salt:hash.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <param name="saltHash">The "salt:hash" this password should generate.</param>
        /// <returns>true if we have a match.</returns>
        public static bool IsPasswordValid(string password, string saltHash)
        {
            string[] parts = saltHash.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)

                return false;

            string computedHash = string.Empty;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                computedHash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }

            return parts[1].Equals(computedHash);
        }

        /// <summary>
        /// This method is used to create the password salt.
        /// </summary>
        public static string CreatePasswordSalt(string password)
        {
            var buf = new byte[16];
            string salt = "";
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(buf);
                salt = Convert.ToBase64String(buf);
            }
            // Create a SHA256 
            string hash = string.Empty;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
            return salt + ':' + hash;
        }

        /// <summary>
        /// Helper to get enum description value
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (field == null)
                return enumValue.ToString();

            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return enumValue.ToString();
        }

        public static bool isEmailValid(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static string OTPGenerate()
        {
            var otp = new Random().Next(1000, 9999).ToString();
            return otp;
        }

        public static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var year = today.Year;
            var age = year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }



    }
}
