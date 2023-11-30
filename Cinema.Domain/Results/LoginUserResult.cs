namespace Cinema.Domain.Results
{
    public class LoginUserResult
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }
    }
}
