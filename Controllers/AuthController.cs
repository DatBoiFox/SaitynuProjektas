using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SaitynuProjektas.Models;

namespace SaitynuProjektas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly DataBaseContext _context;

        public AuthController(DataBaseContext context)
        {
            _context = context;
        }

        //[HttpPost("anon")]
        //public ActionResult GetTokenAnonymous()
        //{
        //    //security key
        //    string securityKey = "this_is_our_supper_long_security_key_for_token_validation_project_2018_09_07$smesk.in";
        //    //symmetric security key
        //    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

        //    //signing credentials
        //    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        //    //add claims
        //    var claims = new List<Claim>();
        //    claims.Add(new Claim(ClaimTypes.Role, "Anonymous"));
        //    //claims.Add(new Claim(ClaimTypes.Role, "Reader"));
        //    //claims.Add(new Claim("Our_Custom_Claim", "Our custom value"));
        //    //claims.Add(new Claim("Id", "110"));


        //    //create token
        //    var token = new JwtSecurityToken(
        //        issuer: "smesk.in",
        //        audience: "readers",
        //        expires: DateTime.Now.AddMinutes(1),
        //        signingCredentials: signingCredentials,
        //        claims: claims
        //    );

        //    //return token
        //    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //}
        [HttpPost("user")]
        public ActionResult GetTokenUser()
        {
            //security key
            string securityKey = "this_is_our_supper_long_security_key_for_token_validation_project_2018_09_07$smesk.in";
            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //add claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "User"));
            //claims.Add(new Claim(ClaimTypes.Role, "Reader"));
            //claims.Add(new Claim("Our_Custom_Claim", "Our custom value"));
            //claims.Add(new Claim("Id", "110"));


            //create token
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: signingCredentials,
                claims: claims
            );

            //return token
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost("admin")]
        public ActionResult GetTokenAdministrator()
        {
            //security key
            string securityKey = "this_is_our_supper_long_security_key_for_token_validation_project_2018_09_07$smesk.in";
            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //add claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            //claims.Add(new Claim(ClaimTypes.Role, "Reader"));
            //claims.Add(new Claim("Our_Custom_Claim", "Our custom value"));
            //claims.Add(new Claim("Id", "110"));


            //create token
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: signingCredentials,
                claims: claims
            );

            //return token
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        private string GetAuthorizationToken(string userLevel)
        {
            if (userLevel.Equals("0"))
            {
                //security key
                string securityKey = "this_is_our_supper_long_security_key_for_token_validation_project_2018_09_07$smesk.in";
                //symmetric security key
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                //signing credentials
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                //add claims
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                //claims.Add(new Claim(ClaimTypes.Role, "Reader"));
                //claims.Add(new Claim("Our_Custom_Claim", "Our custom value"));
                //claims.Add(new Claim("Id", "110"));


                //create token
                var token = new JwtSecurityToken(
                    issuer: "smesk.in",
                    audience: "readers",
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: signingCredentials,
                    claims: claims
                );

                //return token
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else if (userLevel.Equals("1"))
            {
                //security key
                string securityKey = "this_is_our_supper_long_security_key_for_token_validation_project_2018_09_07$smesk.in";
                //symmetric security key
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                //signing credentials
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                //add claims
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                //claims.Add(new Claim(ClaimTypes.Role, "Reader"));
                //claims.Add(new Claim("Our_Custom_Claim", "Our custom value"));
                //claims.Add(new Claim("Id", "110"));


                //create token
                var token = new JwtSecurityToken(
                    issuer: "smesk.in",
                    audience: "readers",
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: signingCredentials,
                    claims: claims
                );

                //return token
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return "None";
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(Login login)
        {
            User user = null; //= _context.Users.Single(e => e.nickName.Equals(login.UserName) &&
                                                  // e.password.Equals(login.Password));
            try
            {
                user = _context.Users.Single(e => e.nickName.Equals(login.UserName) &&
                                                  e.password.Equals(login.Password));
            }
            catch (Exception e)
            {
            }
            //User user = _context.Users.Single(e => e.nickName.Equals(login.UserName) &&
            //                                   e.password.Equals(login.Password));

            if (user != null)
            {
                return Ok(GetAuthorizationToken(user.uerLevel));
            }

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetUser", new { id = user.id }, user);
            return Unauthorized();
        }

    }
}