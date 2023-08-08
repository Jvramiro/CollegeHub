using CollegeHub.Models;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CollegeHub.Services {
    public class ValidateService {

        public static bool ValidateUser(User user, out IResult result) {

            result = !ValitadeEmail(user.Email) ?
                        Results.BadRequest("Invalid Email") :
                    !ValidateCPF(user.CPF) ?
                        Results.BadRequest("Invalid CPF") :
                    !ValidatePhone(user.Phone) ?
                        Results.BadRequest("Invalid Phone") :
                    Results.Ok();

            return result == Results.Ok();

        }

        public static bool ValitadeEmail(string email) {
            return new EmailAddressAttribute().IsValid(email);
        }

        public static bool ValidateCPF(string CPF) {
            bool patternCPF = Regex.IsMatch(CPF, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
            bool digitsOnlyCPF = Regex.IsMatch(CPF, @"^\d+$") && CPF.Count() == 11;
            return patternCPF || digitsOnlyCPF;
        }

        public static bool ValidatePhone(string phone) {
            return Regex.IsMatch(phone, @"^\d+$") && phone.Count() == 13;
        }

    }
}
