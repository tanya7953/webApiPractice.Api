using AutoMapper;
using webApiPractice.Contracts;
using webApiPractice.Dto.Comment;
using webApiPractice.Dto.Stock;

namespace webApiPractice.Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Stock, StockDto>();
            CreateMap<CreateStockRequestDto, Stock>();
            CreateMap<Comment, CommentDto>();
        }
    }
}
