using DAL.Repository;
using Domain.Models;
<<<<<<< HEAD
using Microsoft.Extensions.Logging;
=======
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class BlockService
    {
        private readonly BlockRepository _blockRepository;
<<<<<<< HEAD
        private readonly ILogger<BlockService> _logger;
        public BlockService(BlockRepository blockRepository, ILogger<BlockService> logger)
        {
            _blockRepository = blockRepository;
            _logger = logger;
=======

        public BlockService(BlockRepository blockRepository)
        {
            _blockRepository = blockRepository;
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
        }


        public async Task<Block> AddBlockAsync(string text)
        {
<<<<<<< HEAD
            _logger.LogInformation("Creating new block...");
=======
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
            var documentHash = ComputeHash(text);
            var lastBlock = await _blockRepository.GetLastAsync();

            var newBlock = new Block
            {
                Index = lastBlock == null ? 0 : lastBlock.Index + 1,
                DocumentHash = documentHash,
                PreviousHash = lastBlock?.CurrentHash ?? "0",
                TimeStamp = DateTime.UtcNow
            };

            newBlock.CurrentHash = ComputeHash(
                $"{newBlock.Index}{newBlock.DocumentHash}{newBlock.PreviousHash}{newBlock.TimeStamp}"
            );

            await _blockRepository.AddAsync(newBlock);
            await _blockRepository.SaveChangesAsync();
<<<<<<< HEAD
            _logger.LogInformation("New block created with index {Index}", newBlock.Index);
=======

>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
            return newBlock;
        }

        public string ComputeHash(string input)
        {
<<<<<<< HEAD
            _logger.LogInformation("Computing hash...");
=======
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public async Task<bool> ValidateChainAsync()
        {
<<<<<<< HEAD
            _logger.LogInformation("Validating chain...");
=======
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
            var blocks = await _blockRepository.GetAllAsync();

            for (int i = 1; i < blocks.Count; i++)
            {
                var current = blocks[i];
                var previous = blocks[i - 1];

                if (current.PreviousHash != previous.CurrentHash) return false;
<<<<<<< HEAD
                var recalculatedHash = ComputeHash($"{current.Index}{current.DocumentHash}{current.PreviousHash}{current.TimeStamp}");

                if (current.CurrentHash != recalculatedHash)
                {
                    _logger.LogError("Chain is invalid");
                    return false;
                }
            }
            _logger.LogInformation("Chain is valid");
=======

                var recalculatedHash = ComputeHash(
                    $"{current.Index}{current.DocumentHash}{current.PreviousHash}{current.TimeStamp}"
                );

                if (current.CurrentHash != recalculatedHash) return false;
            }

>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
            return true;
        }
    }
}