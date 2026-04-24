using BLL.Service;
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

        public BlockController(BlockService blockService)
        {
            _blockService = blockService;
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
        public async Task<IActionResult> AddBlock([FromBody] string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return BadRequest("Text is empty");
            var block = await _blockService.AddBlockAsync(text);
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