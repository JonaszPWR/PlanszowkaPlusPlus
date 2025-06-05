using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<Member> _passwordHasher;
        public MembersController(AppDbContext appDbContext, IPasswordHasher<Member> passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(Member member)
        {
            //if(null == member.Rent) //this way creating a new member doesn't require this field
            //{ 
            //    member.Rent = new List<Rent>();
            //}

            member.PasswordHash = _passwordHasher.HashPassword(member, member.PasswordHash);

            _appDbContext.Members.Add(member);
            await _appDbContext.SaveChangesAsync();

            return Ok(member);
        }
        [HttpGet]
        public async Task<IActionResult> ViewMembers()
        {
            var members = await _appDbContext.Members.ToListAsync();
            return Ok(members);
        }
    }
}
