using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : Controller
{
    private readonly IConfiguration _configuration;
    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    // split to another class file
    public class AuthenticationRequestBody
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    
    // split to another class file

    private class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public User(
            int userId, 
            string userName, 
            string firstName, 
            string lastName
            )
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }
    }
    
    
    [HttpPost("authenticate")] // it will be /api/authentication I prefer keep it post only
    public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
    {
        
        // split it to have service level 
        // create authentication service
        
        var user = ValidateUserCredential(
            authenticationRequestBody.UserName,
            authenticationRequestBody.Password
            );
        if (user == null)
        {
            return Unauthorized();
        }
        
        var secretKey = _configuration["Authentication:SecretForKey"];
        var issuer = _configuration["Authentication:Issuer"];
        var audience = _configuration["Authentication:Audience"];
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
        });
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = signingCredentials
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        
        return Ok(tokenString);
    }
    
    private User? ValidateUserCredential(string? userName, string? password)
    {
        // you can use the db to validate the user
        // save the credentials in the db
        if (!(userName == "jameel" && password == "123456"))
        {
            return null;
        }
        
        return new User(
            1,
            userName ?? "",
            "Jameel",
            "Sawafta"
            );
    }
}