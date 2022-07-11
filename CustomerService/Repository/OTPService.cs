using Microsoft.AspNetCore.Identity;

namespace CustomerService.Repository
{
    public class OTPService
    {

        public static string OTPServiceExtensions(string number)
        {
            var otp = string.Empty;

            if (string.IsNullOrEmpty(number))
            {
                return "invalid Number";
            }
            else
            {
                var random = new Random();
                otp = random.Next(3, 6).ToString();
            }

            return otp;
        }
    }
}
