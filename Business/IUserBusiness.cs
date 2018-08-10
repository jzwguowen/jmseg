using jmseg.Models;
using System.Collections.Generic;

namespace jmseg.Business
{
    public interface IUserBusiness
    {
        User Create(User user);
        User FindById(long id);
        object FindByLogin(User user);
        List<User> FindAll();
        User Update(User user);
        void Delete(long id);
    }
}
