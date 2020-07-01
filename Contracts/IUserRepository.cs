using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers(bool trackChanges);
        User GetUser(Guid userId, bool trackChanges);
        IEnumerable<User> GetUsersById(IEnumerable<Guid> ids, bool trackChanges);
        void CreateUser(User user);
        void DeleteUser(User user);

    }
}