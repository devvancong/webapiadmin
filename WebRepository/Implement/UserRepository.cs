using WebEntryModel;
using WebEntryModel.EF;
using WebRepository.Interface;

namespace WebRepository.Implement
{
    public class UserRepository : Repository<Userview>, IUserRepository
    {
        public UserRepository(AppDatabase appDatabase):base(appDatabase) 
        {
            
        }
    }
}