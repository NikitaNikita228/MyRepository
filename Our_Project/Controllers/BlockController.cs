using BLL.Service;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Our_Project.Attributes;
using System.Threading.Tasks;

namespace Our_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly BlockService _blockService;
        private readonly IValidator<CreateBlockRequest> _validator;

        public BlockController(BlockService blockService, IValidator<CreateBlockRequest> validator)
        {
            _blockService = blockService;
            _validator = validator;
        }

        [HttpPost("hash")]
        public IActionResult GenerateHash([FromBody] string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return BadRequest("Text is empty");
            var hash = _blockService.ComputeHash(text);
            return Ok(new { hash });
        }

        [ApiKey]
        [HttpPost("block")]
        public async Task<IActionResult> AddBlock([FromBody] CreateBlockRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var block = await _blockService.AddBlockAsync(request.DataText);

            if (block == null) return BadRequest("Failed to add block");

            return Ok(block);
        }

        [HttpGet("validate")]
        public async Task<IActionResult> Validate()
        {
            var isValid = await _blockService.ValidateChainAsync();
            return Ok(new { isValid });
        }
    }
}