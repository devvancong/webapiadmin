using AutoMapper;
using WebDataModel.BaseClass;
using WebDataModel.ViewModel;
using WebEntryModel.EF;
using WebHelper;
using WebRepository.Interface;
using WebService.Interface;

namespace WebService.Implement
{
    public class UserService : Service<Uservm, Userview>, IUserService
    {
        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        : base(userRepository, unitOfWork, mapper)
        {
        }

        public async Task<bool> AddUser(Uservm model)
        {
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            return await base.Add(model);
        }

        public async Task<bool> Delete(int id)
        {
            return await base.Delete(exp => exp.Id == id);
        }

        public async Task<Uservm> Get(int id)
        {
            return await base.Get(exp => exp.Id == id);
        }

        public async Task<Resultreturn<Uservm>> GetAllUser(SearchParameters searchParameters)
        {
            try
            {
                Paginationpage<Userview> _paginationpage = new Paginationpage<Userview>();
                _paginationpage.PerPage = searchParameters.totalnumber;
                _paginationpage.PageNumber = searchParameters.number;
                if (searchParameters.keywork != null)
                {
                    _paginationpage.Expression.Add(exp => exp.Name.Contains(searchParameters.keywork));
                }

                return await GetAll(_paginationpage);
            }catch(Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<UserResult> Login(UserLogin userLogin)
        {
            UserResult userResult = new UserResult();
            try
            {
                var data = await base.Get(exp => exp.Name == userLogin.UserName);
                if (data != null)
                {
                    userLogin.Password = Base64Decode(userLogin.Password);

                    bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(userLogin.Password, data.Password);
                    if (isPasswordMatch)
                    {
                        userResult.Status = true;
                        userResult.Message = "Đăng nhập thành công";
                        userResult.Error = "Không có lỗi gì sảy ra!";
                        userResult.UserName = data.Name;
                    }
                    else
                    {
                        userResult.Status = false;
                        userResult.Message = "Mật khẩu không đúng!";
                        userResult.Error = "Sai mật khẩu!";
                        userResult.UserName = data.Name;
                    }
                }
                else
                {
                    userResult.Status = false;
                    userResult.Message = "Không tồn tại tài khoản!";
                    userResult.Error = "Sai tên đăng nhập!";
                    return userResult;
                }
            }
            catch (Exception ex)
            {
                userResult.Status = false;
                userResult.Message = "Sảy ra lỗi!";
                userResult.Error = ex.Message;
            }
            return userResult;
        }

        public async Task<bool> UpdateUser(Uservm model)
        {
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            return await base.Update(model);
        }

        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}