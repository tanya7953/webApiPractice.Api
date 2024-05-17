using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using webApiPractice.Api.Helper;
using webApiPractice.Api.Mapper;
using webApiPractice.Contracts;
using webApiPractice.Data;
using webApiPractice.Dto.Stock;
using webApiPractice.Helper;
using webApiPractice.Repository;

namespace webApiPractice.Api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepository;
        public StockController(DataContext context,IMapper mapper,IStockRepository stockRepository)
        {
            _context = context;
            _mapper = mapper;
            _stockRepository = stockRepository;

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
       
            var stockModel = await _stockRepository.GetAllAsync(query);
            var stocks =  _mapper.Map<List<StockDto>>(stockModel);
            return Ok(stocks);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var stockModel = await _stockRepository.GetByIdAsync(id);
            var stock =  _mapper.Map<List<StockDto>>(stockModel);
            if(stock == null)
                return NotFound();
            return Ok(stock);
        
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepository.CreateAsync(stockModel);
            
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id},stockModel);
             
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var stockModel = await _stockRepository.UpdateAsync(id, updateDto);
            if (stockModel == null)
                return NotFound();

            return Ok(stockModel);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var stockModel =await _stockRepository.DeleteAsync(id);
            if (stockModel == null)
                return NotFound();
          
            return Ok();
        }

    }
}