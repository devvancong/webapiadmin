using WebDataModel.BaseClass;
using WebDataModel.ViewModel;
using WebEntryModel.EF;

namespace WebService.Interface
{
    public interface IUserService : IService<Uservm, Userview>
    {
        Task<Resultreturn<Uservm>> GetAllUser(SearchParameters searchParameters);
        Task<bool> AddUser(Uservm model);
        Task<bool> UpdateUser(Uservm model);
        Task<bool> Delete(int id);
        Task<UserResult> Login(UserLogin userLogin);
        Task<Uservm> Get(int id);
    }
}