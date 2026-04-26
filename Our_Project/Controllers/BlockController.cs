using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using Our_Project.Attributes;
using Serilog;
using System.Threading.Tasks;

namespace Our_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly BlockService _blockService;
        private readonly ILogger<BlockController> _logger;
        public BlockController(BlockService blockService, ILogger<BlockController> logger)
        {
            _blockService = blockService;
            _logger = logger;
        }

        [HttpPost("hash")]
        public IActionResult GenerateHash([FromBody] string text)
        {
            _logger.LogInformation("Received request to generate hash");
            if (string.IsNullOrWhiteSpace(text))
            {
                _logger.LogWarning("Empty text provided for hashing");
                return BadRequest("Text is empty");
            }

            var hash = _blockService.ComputeHash(text);
            _logger.LogInformation("Hash generated successfully: {Hash}", hash);
            return Ok(new { hash });

        }

        [ApiKey]
        [HttpPost("block")]
        public async Task<IActionResult> AddBlock([FromBody] string text)
        {
            _logger.LogInformation("Request to add new block");

            if (string.IsNullOrWhiteSpace(text))
            {
                _logger.LogWarning("Empty text provided for block creation");
                return BadRequest("Text is empty");
            }

            try
            {
                var block = await _blockService.AddBlockAsync(text);

                if (block == null)
                {
                    _logger.LogError("Block creation failed");
                    return BadRequest("Failed to add block");
                }

                _logger.LogInformation("Block created: Index={Index}, Hash={Hash}",block.Index, block.CurrentHash);

                return Ok(block);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating block");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("validate")]
        public async Task<IActionResult> Validate()
        {
            _logger.LogInformation("Chain validation requested");

            var isValid = await _blockService.ValidateChainAsync();

            if (isValid)_logger.LogInformation("Blockchain is valid");
            else _logger.LogWarning("Blockchain is invslid");

            return Ok(new { isValid });
        }
    }
}