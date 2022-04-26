using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlaylistService.Application.TrackLogic.CQRS.Queries;
using PlaylistService.Shared.DTOs.TrackDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistService.API.Controllers
{
  [Route("api/p/[controller]")]
  [ApiController]
  public class TracksController : ControllerBase
  {
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TracksController(IMediator mediator, IMapper mapper)
    {
      _mediator = mediator;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadTrackDTO>>> GetTracks()
    {
      var response = await _mediator.Send(new GetTracksQuery());
      return _mapper.Map<IEnumerable<ReadTrackDTO>>(response).ToList();
    }

    [HttpGet("{**trackId}", Name = "GetTrack")]
    public async Task<ActionResult<ReadTrackDTO>> GetTrack(string trackId)
    {
      try
      {
        var response = await _mediator.Send(new GetTrackQuery { Id = trackId });
        return _mapper.Map<ReadTrackDTO>(response);
      }
      catch (ArgumentNullException)
      {
        return NotFound();
      }
    }

    [HttpPost]
    public ActionResult TestIndboundConnection()
    {
      Console.WriteLine("--> Indbound POST # Track Service");

      return Ok("Indbound test of from Platforms Controller");
    }
  }
}
