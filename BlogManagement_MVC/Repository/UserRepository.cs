

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogManagement_MVC.Data;
using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Models;

namespace BlogManagement_MVC.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<bool> Create(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<User>> FindAll()
        {
            var userInfo = await _db.Users.ToListAsync();
            return userInfo;
        }

        public async Task<User> FindById(int id)
        {
            var userInfo = await _db.Users.FindAsync(id);
            return userInfo;
        }

        public async Task<bool> isExists(string id)
        {
            bool isExist = await _db.Users.AnyAsync(q => q.Id == id);           
            return isExist;
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
