using System.Collections.Generic;
using jmseg.Models;
using jmseg.Models.Context;
using System;
using System.Linq;

namespace jmseg.DAO.Implementation
{
    public class UserDAO : IUserDAO
    {
        private readonly MySQLContext _context;

        public UserDAO(MySQLContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            try
            {
                _context.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public User FindById(long id)
        {
            return _context.Users.SingleOrDefault(p => p.Id.Equals(id));
        }

        public User FindByEmail(string email)
        {
            return new User();
        }

        public List<User> FindAll()
        {
            return _context.Users.ToList();
        }

        public User Update(User user)
        {
            // Verificamos se a pessoa existe na base
            // Se não existir retornamos uma instancia vazia de pessoa
            if (!Exists(user.Id)) return null;

            // Pega o estado atual do registro no banco
            // seta as alterações e salva
            var result = _context.Users.SingleOrDefault(b => b.Id == user.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        public void Delete(long id)
        {
            var result = _context.Users.SingleOrDefault(i => i.Id.Equals(id));
            try
            {
                if (result != null) _context.Users.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            return _context.Users.Any(b => b.Id.Equals(id));
        }
    }
}
