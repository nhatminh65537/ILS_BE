using ILS_BE.Domain.Models;

namespace ILS_BE.Application.Interfaces
{
    public interface IPasswordService
    {
        User CreateUserWithHashedPassword(User user, string password);
        bool VerifyUserPassword(User user, string password);
    }
}
