namespace WebDataModel.BaseClass
{
    public class AuthenticatedResponse
    {
        public bool Success { get; set; }=false;
        public string Token { get; set; } = "notoken";
        public string RefreshToken { get; set; } = "noRefreshToken";
    }
}