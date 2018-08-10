using jmseg.Models;
using System.Collections.Generic;

namespace jmseg.DAO
{
    public interface IUserDAO
    {
        User Create(User user);
        User FindById(long id);
        User FindByLogin(User user);
        List<User> FindAll();
        User Update(User user);
        void Delete(long id);
        bool Exists(long? id);
    }
}
