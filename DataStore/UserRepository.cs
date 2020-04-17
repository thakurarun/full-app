using DataStore.Context;
using DataStore.Entities;
using DataStore.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DataStore
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Guid id);
        Task<bool> UserWithEmailExistAsync(string email);
        Task SaveUserAsync(User user);
        Task UpdateUserAsync(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<bool> UserWithEmailExistAsync(string email)
        {
            email = email.Trim();
            return await context.Users.AsNoTracking().AnyAsync(user => user.Email == email);
        }

        public async Task SaveUserAsync(User user)
        {
            if (user.Id == Guid.Empty)
            {
                user.Id = Guid.NewGuid();
            }
            ThrowException.If(await UserWithEmailExistAsync(user.Email), new UserAlreadyRegisteredWithEmailException());
            user.CreatedOn = DateTime.UtcNow;
            user.ModifiedOn = DateTime.UtcNow;
            user.UserProfile.CreatedOn = DateTime.UtcNow;
            user.UserProfile.ModifiedOn = DateTime.UtcNow;
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            ThrowException.If(user.Id == Guid.Empty, new NoUserExistException());
            var existingUser = await context.Users.AsNoTracking()
                .FirstOrDefaultAsync(existingUser => existingUser.Id == user.Id);
            ThrowException.If(existingUser == null, new NoUserExistException());

            // TODO

            user.ModifiedOn = DateTime.UtcNow;
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
    }
}
