using System.Collections.Generic;
using System.Security.Cryptography;
using jmseg.Models;
using jmseg.DAO;
//using BCrypt;

using System;
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
            //user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // check a password
            //bool validPassword = BCrypt.Net.BCrypt.Verify(userSubmittedPassword, hashedPassword);

            return dao.Create(user);
        }

        public User FindById(long id)
        {
            return dao.FindById(id);
        }

        public object FindByLogin(User user)
        {
            //return dao.FindByLogin(user);

            bool credentialsIsValid = false;
            
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                var baseUser = dao.FindByLogin(user);
                credentialsIsValid = (baseUser != null && user.Email == baseUser.Email && user.Password == baseUser.Password);
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
                message = "Failed to autheticate"
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
    }
}
