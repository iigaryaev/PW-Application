using Model.Database;
using Model.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AuthContext
    {
        private readonly UnitOfWork database;

        private const int DefaultBalance = 500;

        public AuthContext(PWDatabase db)
        {
            this.database = new UnitOfWork(db);
        }

        public RegistrationResult Register(string login, string userName, string password)
        {
            var existingLogin = this.database.AccountRepository.GetByLogin(login);
            if(existingLogin != null)
            {
                return new RegistrationResult(false, "Login already exists");
            }


            var existingName = this.database.AccountRepository.GetByName(userName);
            if (existingName != null)
            {
                return new RegistrationResult(false, "User name already exists");
            }

            try
            {
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

                this.database.AccountRepository.Create(new Account { UserLogin = login, UserName = userName, PasswordMD5 = hash, Balance = DefaultBalance });
                this.database.Save();
            }
            catch(Exception ex)
            {
                return new RegistrationResult(false, "Registration error");
            }
            

            return new RegistrationResult(true);
        }

        public Account Authorize(string login, string password)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            var account = this.database.AccountRepository.GetByLogin(login);

            if(account == null)
            {
                return null;
            }

            var passwordsEqual = true;

            for(var i =0; i < Math.Max(hash.Length, account.PasswordMD5.Length); i++)
            {
                if(hash[i] != account.PasswordMD5[i])
                {
                    passwordsEqual = false;
                }
            }

            return passwordsEqual ? account : null;
        }
    }
}
