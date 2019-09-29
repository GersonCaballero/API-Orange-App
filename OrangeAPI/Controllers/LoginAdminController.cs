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
            var user = db.UserAdmins.FirstOrDefault(x => x.Name == login.Username && x.Password == login.Password);

            if (user != null)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                return Ok(token);
            }
            else
            {
                return Ok(new { message = "Usuario o contrasena incorrectos." });
            }
        }
    }
}