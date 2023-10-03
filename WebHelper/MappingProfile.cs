using AutoMapper;
using WebDataModel.ViewModel;
using WebEntryModel.EF;

namespace WebHelper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Uservm, Userview>();
            CreateMap<Userview, Uservm>();
        }
    }
}