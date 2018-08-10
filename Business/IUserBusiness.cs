using jmseg.Models;
using System.Collections.Generic;
using jmseg.VO;

namespace jmseg.Business
{
    public interface IUserBusiness
    {
        User Create(User user);
        User FindById(long id);
        object FindByLogin(User user);
        User FindByEmail(string email);
        List<User> FindAll();
        User Update(User user);
        void Delete(long id);
        object ResetPassword(User user, ResetPasswordVO obj);
    }
}
