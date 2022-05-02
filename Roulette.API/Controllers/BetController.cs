using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roulette.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly IBetService _betservice;
        private readonly ISessionService _sessionService;
        public BetController(IBetService betService, ISessionService sessionService)
        {
            _betservice = betService;
            _sessionService = sessionService;

        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBetById(string Id)
        {
            var bet = await _betservice.GetBetById(Guid.Parse(Id));
            return Ok(bet);
        }
        
        [HttpPost]
        [Route("/s={activeSessionId},bt={ChosenBetTypeId},n={ChosenNumbers},ba={bettingAmount}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult PlaceBet(string activeSessionId, int ChosenBetTypeId, string ChosenNumbers, int bettingAmount)
        {

            if (!_sessionService.SessionIsAvailableForSpin(activeSessionId))
            {
                return BadRequest("Session cannot be spun.");
            }

            var bet = new Bet
            {
                Id = Guid.NewGuid(),
                sessionId = Guid.Parse(activeSessionId),
                betTypeId = ChosenBetTypeId,
                numbers = ChosenNumbers.Split(',').Select(Int32.Parse).ToList(),
                betAmount = bettingAmount,
                payoutAmount = 0
            };
            _betservice.AddBet(bet);
            return CreatedAtAction(nameof(GetBetById), new { Id = bet.Id }, bet);
        }
    }
}
