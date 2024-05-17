using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webApiPractice.Api.Mapper;
using webApiPractice.Data;
using webApiPractice.Dto.Comment;
using webApiPractice.Dto.Stock;
using webApiPractice.Interface;
using webApiPractice.Repository;

namespace webApiPractice.Api.Controllers
{
    [Route("api/comment")]
   /* [ApiController]*/
    public class CommentController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(DataContext context, IMapper mapper, ICommentRepository commentRepository,IStockRepository stockRepository)
        {
            _context = context;
            _mapper = mapper;
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            
            }
            var model = await _commentRepository.GetAllAsync();
            var comment = _mapper.Map<List<StockDto>>(model);
            if (comment == null)
                return NotFound();
            return Ok(comment);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var comments = await _commentRepository.GetByIdAsync(id);
            var comment = _mapper.Map<List<StockDto>>(comments);
            if (comment == null)
                return NotFound();
            return Ok(comment);
        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromBody] int stockID, CreateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (!await _stockRepository.StockExist(stockID))
            {
                return BadRequest();
            }
            var commentModel = commentDto.ToCommentFromCreateDTO(stockID);
            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = commentModel.Id }, commentModel);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var comment = await _commentRepository.UpdateAsync(id, updateDto.ToCommentFromUpdate());
            if (comment == null)
            {
                NotFound("Comment Not Found");
            }
            return Ok(comment);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var commentModel = await _commentRepository.DeleteAsync(id);
            if (commentModel == null)
                return NotFound();

            return Ok();
        }
    }
   
}
