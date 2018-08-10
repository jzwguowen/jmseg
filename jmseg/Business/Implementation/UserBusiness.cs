using System.Collections.Generic;
using System.Security.Cryptography;
using jmseg.Models;
using jmseg.DAO;
using jmseg.VO;

using System;
using System.Text;
using jmseg.Security.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

namespace jmseg.Business.Implementation
{
    public class UserBusiness : IUserBusiness
    {
        private IUserDAO dao;
        private SigningConfigurations _signingConfigurations;
        private TokenConfiguration _tokenConfigurations;

        public UserBusiness(IUserDAO repository, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration)
        {
            dao = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfiguration; 
        }

        public User Create(User user)
        {
            user.Password = GerarHashMd5(user.Password);

            return dao.Create(user);
        }

        public User FindById(long id)
        {
            return dao.FindById(id);
        }

        public User FindByEmail(string email) {
            return dao.FindByEmail(email);
        }

        public object FindByLogin(User user)
        {
            bool credentialsIsValid = false;
            
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                var baseUser = dao.FindByLogin(user);

                //
                // check a password
                //
                if (baseUser != null)
                {
                    bool isValidPassword = baseUser.Password.Equals(GerarHashMd5(user.Password));

                    credentialsIsValid = (user.Email == baseUser.Email && isValidPassword);
                }
            }

            if (credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Email, "Login"),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                        }
                    );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, token);
            } else
            {
                return ExceptionObject();
            }
        }

        public List<User> FindAll()
        {
            return dao.FindAll();
        }

        public User Update(User user)
        {
            user.Password = GerarHashMd5(user.Password);

            return dao.Update(user);
        }

        public void Delete(long id)
        {
            dao.Delete(id);
        }

        public bool Exists(long id)
        {
            return dao.Exists(id);
        }

        public object ResetPassword(User user, ResetPasswordVO obj)
        {
            bool credentialsIsValid = false;
            
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                //
                // check a password
                //
                credentialsIsValid = user.Password.Equals(GerarHashMd5(obj.Password));
            }

            if (credentialsIsValid)
            {
                user.Password = GerarHashMd5(obj.NewPassword);

                dao.Update(user);

                return new
                {
                    message = "OK"
                };
            } else {
                return new
                {
                    message = "Failed"
                };
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to authenticate"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }

        public string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
