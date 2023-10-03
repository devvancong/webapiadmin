using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using WebDataModel.BaseClass;
using WebRepository.Interface;
using WebService.Interface;

namespace WebService.Implement
{
    public class Service<TModel, TEnty> : IService<TModel, TEnty>
        where TModel : class
        where TEnty : class

    {
        private readonly IRepository<TEnty> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Service(IRepository<TEnty> repository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(TModel model)
        {
            try
            {
                var data = _mapper.Map<TEnty>(model);
                var result = await _repository.AddAsync(data);
                await _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<bool> Delete(Expression<Func<TEnty, bool>> expression)
        {
            try
            {
                var result = await _repository.GetAsync(expression);
                var data = _repository.Remove(result);
                await _unitOfWork.Commit();
                return data;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<TModel> Get(Expression<Func<TEnty, bool>> expression)
        {
            try
            {
                var result = await _repository.GetAsync(expression);
                var data = _mapper.Map<TModel>(result);

                return data;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<Resultreturn<TModel>> GetAll(Paginationpage<TEnty> paginationpage)
        {
            try
            {
                Resultreturn<TModel> resultreturn = new Resultreturn<TModel>();
                resultreturn.Status = true;
                resultreturn.PageNumber = paginationpage.PageNumber;
                resultreturn.PageSize = paginationpage.PerPage;

                resultreturn.Results = await _mapper.ProjectTo<TModel>(_repository.GetAll(paginationpage))
                                                    .ToListAsync();
                return resultreturn;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<bool> Update(TModel model)
        {
            try
            {
                var data = _mapper.Map<TEnty>(model);
                var result = _repository.Update(data);
                await _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}