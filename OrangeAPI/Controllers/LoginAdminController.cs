using OrangeAPI.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    [EnableCors("*", "*", "*")]
    public class LoginController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }        

        [HttpPost]
        [Route("Admin")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            var user = db.UserAdmins.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password && x.State == true);
            var userEmail = db.UserAdmins.FirstOrDefault(x => x.Email == login.Email && x.State == true);
            var userPassword = db.UserAdmins.FirstOrDefault(x => x.Password == login.Password && x.State == true);
            
            if (userEmail == null || userPassword == null)
            {
                return BadRequest("Usuario o contrasena incorrectos.");

            }

            if (user == null)
            {
                return BadRequest("El usuario no existe.");
            }
            else
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Email);
                return Ok(token);
            }
        }
    }
}