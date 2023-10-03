namespace WebDataModel.ViewModel
{
    public class UserResult
    {
        public bool Status { get; set; } = false;
        public string Message { get; set; } = "Đăng nhập thành công!";
        public string Error { get; set; } = "Sảy ra lỗi!";
        public string UserName { get; set; }
        public string Role { get; set; } = "admin";
    }
}