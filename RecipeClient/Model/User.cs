using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace RecipeClient.Model
{
    public class User : ModelBase
    {
        private static int ObjectCounter { get; set; } = 0;

        [Required]
        [StringLength(50)]
        public string Username { get; private set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please provide a valid email address!")]
        public string Email { get; private set; }

        [Required]
        [MinLength(12, ErrorMessage = "Password needs min length of 12!")]
        public string Password { get; private set; }

        private User(
            string username,
            string email,
            string password)
        {
            ObjectCounter += 1;

            Id = ObjectCounter;
            Username = username;
            Email = email;
            Password = password;
        }

        public void Update(string username, string email)
        {
            if (!IsValidUsername(username) || !IsValidEmail(email))
            {
                throw new Exception(
                    "Something went wrong when updating the user (Replace by application exception)");
            }

            Username = username.Trim();
            Email = email.Trim();
        }

        public void UpdatePassword(string newPassword)
        {
            if (!IsValidPassword(newPassword))
            {
                throw new Exception(
                    "Something went wrong when updating the password (Replace by application exception)");
            }

            Password = GenerateSha512PasswordHash(newPassword);
        }

        public static bool IsValidEmail(string email) =>
            !string.IsNullOrWhiteSpace(email) && Validator.TryValidateValue(
                email,
                new ValidationContext(email),
                new List<ValidationResult>(),
                new List<ValidationAttribute>()
                {
                new RequiredAttribute(),
                new EmailAddressAttribute()
                });

        public static bool IsValidUsername(string username) =>
            !string.IsNullOrWhiteSpace(username) && Validator.TryValidateValue(
                    username,
                    new ValidationContext(username),
                    new List<ValidationResult>(),
                    new List<ValidationAttribute>()
                    {
                    new RequiredAttribute(),
                    new MaxLengthAttribute(50)
                    });

        public static bool IsValidPassword(string password) =>
            !string.IsNullOrWhiteSpace(password) && Validator.TryValidateValue(
                password,
                new ValidationContext(password),
                new List<ValidationResult>(),
                new List<ValidationAttribute>()
                {
                new RequiredAttribute(),
                new MinLengthAttribute(12)
                });

        public static string GenerateSha512PasswordHash(string validPassword)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(validPassword);
            byte[] hashBytes = SHA512.HashData(passwordBytes);
            return Convert.ToBase64String(hashBytes);
        }

        public static User Create(
            string username,
            string email,
            string password)
        {
            if (!IsValidEmail(email) ||
                !IsValidUsername(username) ||
                !IsValidPassword(password))
            {
                throw new Exception("This is a validation error in the user (Will be replaced with a application exception)");
            }

            string passwordHash = GenerateSha512PasswordHash(password);

            return new User(username.Trim(), email.Trim(), passwordHash);
        }
    }
}
