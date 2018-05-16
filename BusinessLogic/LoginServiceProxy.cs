using ApplicationHealth;
using System;
using System.Security.Authentication;

namespace BusinessLogic
{
    public class LoginServiceProxy
    {
        private IHealthStatusRecorder healthStatusRecorder;
        private readonly string backendUrl;

        public LoginServiceProxy(string backendUrl, IHealthStatusRecorder healthStatusRecorder)
        {
            this.healthStatusRecorder = healthStatusRecorder;
            this.backendUrl = backendUrl;
        }

        public LoginResponse Login(string username, string password)
        {
            try
            {
                //
                // do actual login work -> TODO
                //

                if (password.Equals("invalid"))
                {
                    throw new AuthenticationException();
                }

                if (password.Equals("crash"))
                {
                    throw new TimeoutException();
                }
                
                var result = new LoginResponse
                {
                    IsSuccess = true,
                    AuthToken = "ABC-DEF-GHI-JKL-123"
                };

                healthStatusRecorder.SetState(HealthState.Ok);
                return result;
            }
            catch (AuthenticationException ex)
            {
                healthStatusRecorder.SetState(HealthState.Ok);
                return new LoginResponse { IsSuccess = false, Message = "Invalid username or password" };
            }
            catch (Exception ex)
            {
                healthStatusRecorder.SetState(ex, "Error during login operation");
                return new LoginResponse { IsSuccess = false, Message = "Unexpected error during login operation" };
            }
        }
    }
}
