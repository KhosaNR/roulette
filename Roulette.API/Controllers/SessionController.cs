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
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSessions()
        {
            var sessions = await _sessionService.GetAllSessions();
            return Ok(sessions);
        }
        
        [HttpGet]
        //[Route("previousspins")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ShowPreviousSpins()
        {
            var sessions = await _sessionService.GetDoneSessions();
            return Ok(sessions);
        }
        
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSessionByID(string Id)
        {
            var session = await _sessionService.GetSessionByID(Id);
            return Ok(session);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateSession()
        {
            var sessionstring = Guid.NewGuid().ToString("N");
            //var activeSession = _sessionService.GetActiveSession();
            //if(!(activeSession is null))
            //{
            //    throw new Exception("Cannot create a new session because there is already an active session.");
            //}
            var activeSession = new Session
            {
                Id = sessionstring,
                HasSpun = false
            };
            _sessionService.AddSession(activeSession);
            return CreatedAtAction(nameof(GetSessionByID), new { Id = activeSession.Id }, activeSession);

        }


        [HttpPut]
        // [Route("spin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Spin([FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (session.HasSpun)
            {
                return BadRequest("Session already spun");
            }
            session.SetWinningNumber();
            session.HasSpun = true;
            _sessionService.UpdateSession(session);
            return Ok();
        }

        [HttpPut]
        [Route("{sessionId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Spin(string sessionId)
        {
            if (!_sessionService.SessionIsAvailableForPlacingBets(sessionId))
            {
                return BadRequest("Cannot spin, either sesson is already spun or does not exist.");
            }
            var bets = _sessionService.GetAllBetsForSession(sessionId);
            if (!bets.Result.Any())
            {
                return BadRequest("Session has no active bets");
            }
            var session = _sessionService.GetSessionByID(sessionId);
            session.Result.SetWinningNumber();
            session.Result.HasSpun = true;
            _sessionService.UpdateSession(session.Result);
            return Ok();
        }

        [HttpPost]
        [Route("s={activeSessionId},bt={ChosenBetTypeId},n={ChosenNumbers},ba={bettingAmount}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult PlaceBet(string activeSessionId, int ChosenBetTypeId, string ChosenNumbers, int bettingAmount)
        {

            if (!_sessionService.SessionIsAvailableForPlacingBets(activeSessionId))
            {
                return BadRequest("Cannot place bet, session cannot accept bets.");
            }

            var bet = new Bet
            {
                Id = Guid.NewGuid().ToString("N"),
                SessionId = (activeSessionId),
                BetTypeId = ChosenBetTypeId,
                Numbers = ChosenNumbers,
                BetAmount = bettingAmount,
                PayoutAmount = 0
            };
            _sessionService.AddBet(bet);
            return CreatedAtAction(nameof(GetBetById), new { Id = bet.Id }, bet);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBetById(string Id)
        {
            var bet = await _sessionService.GetBetById(Id);
            return Ok(bet);
        }
    }
}
