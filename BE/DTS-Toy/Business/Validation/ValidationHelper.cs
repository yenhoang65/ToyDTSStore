using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Validation
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Kiểm tra số điện thoại Việt Nam (10 hoặc 11 số)
            string pattern = @"^(0|\+84)(\d{9,10})$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            string pattern = @"^(http|https):\/\/[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,}(\/\S*)?$";
            return Regex.IsMatch(url, pattern);
        }

        public static bool IsValidLength(string input, int minLength, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.Length >= minLength && input.Length <= maxLength;
        }

        public static bool ContainsOnlyLetters(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.All(char.IsLetter);
        }

        public static bool ContainsOnlyNumbers(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.All(char.IsDigit);
        }

        public static bool ContainsLettersAndNumbers(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.Any(char.IsLetter) && input.Any(char.IsDigit);
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt
            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        public static bool IsValidDateFormat(string date)
        {
            return DateTime.TryParse(date, out _);
        }

        public static bool IsFutureDate(DateTime date)
        {
            return date > DateTime.Now;
        }

        public static bool IsPastDate(DateTime date)
        {
            return date < DateTime.Now;
        }

        public static bool IsInRange(decimal value, decimal min, decimal max)
        {
            return value >= min && value <= max;
        }

        public static bool IsInRange(int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static bool ContainsSpecialCharacters(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string pattern = @"[!@#$%^&*(),.?""':{}|<>]";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            string pattern = @"^[\w\-. ]+$";
            return Regex.IsMatch(fileName, pattern);
        }

        public static bool IsValidFileExtension(string fileName, string[] allowedExtensions)
        {
            if (string.IsNullOrWhiteSpace(fileName) || allowedExtensions == null || allowedExtensions.Length == 0)
                return false;

            string extension = Path.GetExtension(fileName)?.ToLower();
            return !string.IsNullOrEmpty(extension) && allowedExtensions.Contains(extension.ToLower());
        }
    }
}
