using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesiMarket.Models;
using DesiMarket.Repositories;
using System.Collections;
using Microsoft.Extensions.Logging;
using log4net.Repository.Hierarchy;

namespace DesiMarket.Controllers
{
    [Route("api/{version:apiversion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        //private readonly OnlineGroceryDbContext _context;
        private readonly IUsersRepository _userRepository;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUsersRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        [HttpGet]
        [Route("AllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            return Ok(users);
        }
        [HttpGet,MapToApiVersion("1.0")]
        [Route("AllUsers")]
        public async Task<IActionResult> GetUsersv1()
        {
            var users = await _userRepository.GetUsersAsync();
            _logger.LogInformation("Userdetails fetched successfully");
            return Ok(users);
        }
        [HttpGet("{id}")]
        //[Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogError("USerID doesn't exist");
                return NotFound();
            }
            return Ok(user);
        }
       // [HttpGet("{Email}/{Password}")]
       [Route("Login")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            Hashtable err = new Hashtable();
            try
            {

                var user = await _userRepository.LoginAsync(Email, Password);
                if (user != null)
                {
                    _logger.LogInformation("User login successful");
                    return Ok(user);
                }   
                else
                {
                    err.Add("Status", "Error");
                    err.Add("Message", "Invalid Credentials");
                    _logger.LogError("Invalid credentials");
                    return Ok(err);
                }
            }
            catch(Exception) 
            {
                throw;
            }
        }
        [HttpPost]
        [Route("SignUp")]
       
        public async Task<IActionResult> Register(Users user)
        {

            await _userRepository.CreateUserAsync(user);
            _logger.LogInformation("Signup successfully");
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, Users user)
        {
            var u = await _userRepository.GetUserByIdAsync(id);
            if (u == null)
            {
                return NotFound();
            }
            else
            {
                u.Email= user.Email;
                u.Password= user.Password;
                u.Name= user.Name;
                u.IsAdmin= user.IsAdmin;
                u.Address= user.Address;
                u.PhoneNumber= user.PhoneNumber;
                u.Pincode= user.Pincode;
            }
            await _userRepository.UpdateUserAsync(u);
            _logger.LogInformation("Uspdated successfully");
            return Ok();
        }
        [HttpDelete("{id}")]
        //[Route("DeleteAccount")]
        public async Task<IActionResult> DeleteUserAccount(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userRepository.DeleteUserAsync(user);
            _logger.LogInformation("Deleted successfully");
            return Ok();
        }
        //public UsersController(OnlineGroceryDbContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Users
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Users>>> GetUser()
        //{
        //    return await _context.User.ToListAsync();
        //}

        //// GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Users>> GetUsers(int id)
        //{
        //    var users = await _context.User.FindAsync(id);

        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return users;
        //}

        //// PUT: api/Users/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUsers(int id, Users users)
        //{
        //    if (id != users.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(users).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsersExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Users
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Users>> PostUsers(Users users)
        //{
        //    _context.User.Add(users);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUsers", new { id = users.UserId }, users);
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Users>> DeleteUsers(int id)
        //{
        //    var users = await _context.User.FindAsync(id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.User.Remove(users);
        //    await _context.SaveChangesAsync();

        //    return users;
        //}

        //private bool UsersExists(int id)
        //{
        //    return _context.User.Any(e => e.UserId == id);
        //}
    }
}
