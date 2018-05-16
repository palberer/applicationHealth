namespace BusinessLogic
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string AuthToken { get; set; }
    }
}