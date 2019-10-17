using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormyWebService.Entities;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Services;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            //            return await _context.Users.ToListAsync();

            throw new NotImplementedException();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            //            var user = await _context.Users.FindAsync(id);
            //
            //            if (user == null)
            //            {
            //                return NotFound();
            //            }
            //
            //            return user;

            throw new NotImplementedException();
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            //            if (id != user.UserId)
            //            {
            //                return BadRequest();
            //            }
            //
            //            _context.Entry(user).State = EntityState.Modified;
            //
            //            try
            //            {
            //                await _context.SaveChangesAsync();
            //            }
            //            catch (DbUpdateConcurrencyException)
            //            {
            //                if (!UserExists(id))
            //                {
            //                    return NotFound();
            //                }
            //                else
            //                {
            //                    throw;
            //                }
            //            }
            //
            //            return NoContent();

            throw new NotImplementedException();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //            _context.Users.Add(user);
            //            await _context.SaveChangesAsync();
            //
            //            return CreatedAtAction("GetUser", new { id = user.UserId }, user);

            throw new NotImplementedException();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            //            var user = await _context.Users.FindAsync(id);
            //            if (user == null)
            //            {
            //                return NotFound();
            //            }
            //
            //            _context.Users.Remove(user);
            //            await _context.SaveChangesAsync();
            //
            //            return user;

            throw new NotImplementedException();
        }

        private bool UserExists(int id)
        {
            //            return _context.Users.Any(e => e.UserId == id

            throw new NotImplementedException();
        }
    }
}
