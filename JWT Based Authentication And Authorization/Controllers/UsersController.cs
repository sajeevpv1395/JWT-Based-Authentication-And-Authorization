using JWT_Based_Authentication_And_Authorization.Model;
using JWT_Based_Authentication_And_Authorization.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Based_Authentication_And_Authorization.Controllers
{
    
		[Authorize]
		[Route("api/[controller]")]
		[ApiController]
		public class UsersController : Controller
		{
			private readonly IJWTManagerRepository _jWTManager;

			public UsersController(IJWTManagerRepository jWTManager)
			{
				this._jWTManager = jWTManager;
			}

			[HttpGet]
			public List<string> Get()
			{
				var users = new List<string>
		{
			"Satinder Singh",
			"Amit Sarna",
			"Davin Jon"
		};

				return users;
			}

			[AllowAnonymous]
			[HttpPost]
			[Route("authenticate")]
			public IActionResult Authenticate(Users usersdata)
			{
				var token = _jWTManager.Authenticate(usersdata);

				if (token == null)
				{
					return Unauthorized();
				}

				return Ok(token);
			}
		}
	
}
