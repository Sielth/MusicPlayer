using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackService.Application.TrackLogic.CQRS.Commands;
using TrackService.Application.TrackLogic.CQRS.Notifications;
using TrackService.Application.TrackLogic.CQRS.Queries;
using TrackService.Shared.DTOs.TrackDTOs;

namespace TrackService.API.Controllers
{
  [Route("api/[controller]")]
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

    [HttpPost]
    public async Task<ActionResult<ReadTrackDTO>> CreateTrack(CreateTrackDTO track)
    {
      try
      {
        var request = _mapper.Map<CreateTrackCommand>(track);
        var response = await _mediator.Send(request);
        var trackAdded = _mapper.Map<ReadTrackDTO>(response);

        await _mediator.Publish(new TrackAddedNotification { Track = trackAdded });

        return CreatedAtRoute(nameof(GetTrack), new { Id = trackAdded.Id }, trackAdded);
      }
      catch (ArgumentNullException)
      {
        return BadRequest();
      }
      catch (OperationCanceledException ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadTrackDTO>>> GetTracks()
    {
      var response = await _mediator.Send(new GetTracksQuery());
      return _mapper.Map<IEnumerable<ReadTrackDTO>>(response).ToList();
    }

    [HttpGet("{mood}")]
    public async Task<ActionResult<IEnumerable<ReadTrackDTO>>> GetTracksByMood(string mood)
    {
      var response = await _mediator.Send(new GetTracksByMoodQuery { Mood = mood });
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
  }
}
