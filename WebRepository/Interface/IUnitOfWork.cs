namespace WebRepository.Interface
{
    public interface IUnitOfWork
    {
        Task Commit();

        Task Rollback();
    }
}