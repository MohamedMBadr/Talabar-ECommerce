using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.core.Entities.Identity;
using Talabat.core.Services;

namespace Talabat.APIs.Controllers
{

	public class AccountsController : BaseApiController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;

		public AccountsController(
			UserManager<AppUser> userManager ,
			SignInManager<AppUser> signInManager ,
			ITokenService tokenService,
			IMapper mapper
			)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_mapper = mapper;
		}



		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null) return Unauthorized(new ApiResponse(401));
			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,

				Email = user.Email,
				Token =await _tokenService.createTokenAsync(user , _userManager)
			});
			

		}



		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> register(RegisterDto model)
		{
			if (chekEmailExists(model.Email).Result.Value)
				return BadRequest(new ApiValidationErrorReponseL() { Errors= new string[] {"this email is already used"} });

			var user = new AppUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.Email.Split('@')[0],
				PhoneNumber = model.PhoneNumber,

			};
			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded) return BadRequest(new ApiResponse(400));
			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _tokenService.createTokenAsync(user, _userManager)
			});
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email);
			return Ok(new UserDto()
			{
				Email = user.Email,
				DisplayName = user.DisplayName,
				Token = await _tokenService.createTokenAsync(user, _userManager)
			});

		}

		[Authorize]
		[HttpGet("address")]
		public async Task<ActionResult<AddressDto>> getUserAddress()
		{
			var user = await _userManager.FindUserWithAddressAsync(User);
			var address = _mapper.Map<Address, AddressDto>(user.Address);
			return Ok( address);
		}

		[Authorize]
		[HttpPut("address")]
		public async Task<ActionResult<AddressDto>> updateUserAddress(AddressDto updatedAddress)
		{
			var address = _mapper.Map<AddressDto, Address>(updatedAddress);


			var user = await _userManager.FindUserWithAddressAsync(User);

			address.Id = user.Address.Id;

			user.Address = address;

			var result = await _userManager.UpdateAsync(user);

			if(!result.Succeeded) return BadRequest(new ApiResponse(400));


			return Ok(updatedAddress);
		}

		[HttpGet("emailexists")]
		public async Task<ActionResult<bool>> chekEmailExists (string email)
		{
			return await _userManager.FindByEmailAsync(email) is not null;
		}

	}
}
