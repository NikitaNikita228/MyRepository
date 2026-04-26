using BLL.Service;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using Our_Project.Attributes;
using Serilog;
=======
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Our_Project.Attributes;
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
using System.Threading.Tasks;

namespace Our_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly BlockService _blockService;
<<<<<<< HEAD
        private readonly ILogger<BlockController> _logger;
        public BlockController(BlockService blockService, ILogger<BlockController> logger)
        {
            _blockService = blockService;
            _logger = logger;
=======
        private readonly IValidator<CreateBlockRequest> _validator;

        public BlockController(BlockService blockService, IValidator<CreateBlockRequest> validator)
        {
            _blockService = blockService;
            _validator = validator;
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
        }

        [HttpPost("hash")]
        public IActionResult GenerateHash([FromBody] string text)
        {
<<<<<<< HEAD
            _logger.LogInformation("Received request to generate hash");
            if (string.IsNullOrWhiteSpace(text))
            {
                _logger.LogWarning("Empty text provided for hashing");
                return BadRequest("Text is empty");
            }

            var hash = _blockService.ComputeHash(text);
            _logger.LogInformation("Hash generated successfully: {Hash}", hash);
            return Ok(new { hash });

=======
            if (string.IsNullOrWhiteSpace(text)) return BadRequest("Text is empty");
            var hash = _blockService.ComputeHash(text);
            return Ok(new { hash });
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
        }

        [ApiKey]
        [HttpPost("block")]
<<<<<<< HEAD
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
=======
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
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
        }

        [HttpGet("validate")]
        public async Task<IActionResult> Validate()
        {
<<<<<<< HEAD
            _logger.LogInformation("Chain validation requested");

            var isValid = await _blockService.ValidateChainAsync();

            if (isValid)_logger.LogInformation("Blockchain is valid");
            else _logger.LogWarning("Blockchain is invslid");

=======
            var isValid = await _blockService.ValidateChainAsync();
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
            return Ok(new { isValid });
        }
    }
}