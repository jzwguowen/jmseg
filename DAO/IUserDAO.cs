using jmseg.Model;
using System.Collections.Generic;

namespace jmseg.DAO
{
    public interface IUserDAO
    {
        User Create(User user);
        User FindById(long id);
        User FindByEmail(string email);
        List<User> FindAll();
        User Update(User user);
        void Delete(long id);
        bool Exists(long? id);
    }
}
