using jmseg.Model;
using System.Collections.Generic;

namespace jmseg.Business
{
    public interface IUserBusiness
    {
        User Create(User user);
        User FindById(long id);
        User FindByEmail(string email);
        List<User> FindAll();
        User Update(User user);
        void Delete(long id);
    }
}
