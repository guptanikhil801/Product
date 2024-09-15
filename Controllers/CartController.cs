using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Product.Classes;
using Product.Classes.Models;
using Product.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ICartService cartService;

        public CartController(IConfiguration configuration, ICartService CartService)
        {
            this.configuration = configuration;
            this.cartService = CartService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] int CartID)
        {
            return Ok(new { token = GenerateToken() });
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return this.cartService.GetCart();
        }

        [Authorize]
        [HttpPost("addProductToCart")]
        public async Task<IActionResult> AddProductToCart(Cart Cart)
        {
            if (!ModelState.IsValid || !await this.cartService.AddProductsToCartAsync(Cart))
            {
                return BadRequest(false);
            }

            return Ok(true);
        }

        private string GenerateToken()
        {
            var settings = configuration.GetSection("JwtValues").Get<JwtValues>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); ;
            var token = new JwtSecurityToken(
                settings.Issuer,
                settings.Audience, null,
                expires: DateTime.Now.AddMinutes(settings.ExpiresInMinutes),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
