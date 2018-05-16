using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly LoginServiceProxy loginServiceProxy;

        public LoginController(LoginServiceProxy loginServiceProxy)
        {
            this.loginServiceProxy = loginServiceProxy;
        }

        [HttpGet("SimulateSuccess")]
        public LoginResponse SimulateSuccess()
        {
            return loginServiceProxy.Login("palberer", "success");
        }

        [HttpGet("SimulateCrash")]
        public LoginResponse SimulateCrash()
        {
            return loginServiceProxy.Login("palberer", "crash");
        }
    }
}