namespace WebDataModel.BaseClass
{
    public class Tokens
    {
        public string Token { get; set; } = "notoken";
        public string RefreshToken { get; set; } = "noRefreshToken";
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; } = false;
        public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.Now;
    }
}