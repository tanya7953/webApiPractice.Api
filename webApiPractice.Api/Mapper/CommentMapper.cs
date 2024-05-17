using Microsoft.AspNetCore.Http.HttpResults;
using webApiPractice.Contracts;
using webApiPractice.Dto.Comment;
using webApiPractice.Dto.Stock;

namespace webApiPractice.Api.Mapper
{ 
    public static class CommentMapper
    {
        public static Comment ToCommentFromCreateDTO(this CreateCommentRequestDto commentDto,int stockId)
        {
            return new Comment() 
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                
                StockId = stockId
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto)
        {
            return new Comment()
            {
                Title = commentDto.Title,
                Content = commentDto.Content
            };
        }
    }
}
