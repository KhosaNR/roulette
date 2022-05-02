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
        private readonly IBetService _betservice;
        public SessionController(ISessionService sessionService, IBetService betService)
        {
            _sessionService = sessionService;
            _betservice = betService;
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
        //[Route("new")]
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
                hasSpun = false
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
            if (session.hasSpun)
            {
                return BadRequest("Session already spun");
            }
            session.SetWinningNumber();
            session.hasSpun = true;
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
                return BadRequest("Session cannot be spun, either sesson is already spun or does not exist.");
            }
            var bets = _sessionService.GetAllBetsForSession(sessionId);
            if (bets is null)
            {
                return BadRequest("Session has no active bets");
            }
            var session = _sessionService.GetSessionByID(sessionId);
            /*if(session is null)
            {
                return BadRequest("Session already spun");
            }
            if (session.Result.hasSpun)
            {
                return BadRequest("Session already spun");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }*/
            session.Result.SetWinningNumber();
            session.Result.hasSpun = true;
            _sessionService.UpdateSession(session.Result);
            return Ok();
        }

        //public bool SesssionHasRelatedBets(string sessionId)
        //{
            
        //}

        [HttpPost]
        [Route("s={activeSessionId},bt={ChosenBetTypeId},n={ChosenNumbers},ba={bettingAmount}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult PlaceBet(string activeSessionId, int ChosenBetTypeId, string ChosenNumbers, int bettingAmount)
        {

            if (!_sessionService.SessionIsAvailableForPlacingBets(activeSessionId))
            {
                return BadRequest("Session cannot be spun.");
            }

            var bet = new Bet
            {
                Id = Guid.NewGuid().ToString("N"),
                sessionId = (activeSessionId),
                betTypeId = ChosenBetTypeId,
                numbers = ChosenNumbers,
                betAmount = bettingAmount,
                payoutAmount = 0
            };
            _betservice.AddBet(bet);
            return CreatedAtAction(nameof(GetBetById), new { Id = bet.Id }, bet);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBetById(string Id)
        {
            var bet = await _betservice.GetBetById(Id);
            return Ok(bet);
        }
    }
}
