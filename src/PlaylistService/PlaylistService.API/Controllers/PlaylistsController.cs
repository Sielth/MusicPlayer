using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlaylistService.Application.PlaylistLogic.CQRS.Commands;
using PlaylistService.Application.PlaylistLogic.CQRS.Queries;
using PlaylistService.Shared.DTOs.PlaylistDTOs;
using PlaylistService.Shared.DTOs.TrackDTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistService.API.Controllers
{
  [Route("api/p/{userId}/[controller]")]
  [ApiController]
  public class PlaylistsController : ControllerBase
  {
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PlaylistsController(IMediator mediator, IMapper mapper)
    {
      _mediator = mediator;
      _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<PlaylistReadDTO>> CreatePlaylist(int userId, PlaylistCreateDTO playlist)
    {
      try
      {
        var request = _mapper.Map<CreatePlaylistCommand>(playlist);

        request.UserId = userId;

        var response = await _mediator.Send(request);

        return _mapper.Map<PlaylistReadDTO>(response);
      }
      catch (KeyNotFoundException)
      {
        return NotFound();
      }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlaylistReadDTO>>> GetPlaylistsForUser(int userId)
    {
      try
      {
        var response = await _mediator.Send(new GetPlaylistsForUserQuery { UserId = userId });

        return _mapper.Map<IEnumerable<PlaylistReadDTO>>(response).ToList();
      }
      catch (KeyNotFoundException)
      {
        return NotFound();
      }
    }

    [HttpGet("{**playlistId}", Name = "GetPlaylist")]
    public async Task<ActionResult<PlaylistReadDTO>> GetPlaylistForUser(int userId, string playlistId)
    {
      try
      {
        var response = await _mediator.Send(new GetPlaylistForUserQuery { UserId = userId, Id = playlistId });

        return _mapper.Map<PlaylistReadDTO>(response);
      }
      catch (KeyNotFoundException)
      {
        return NotFound();
      }
    }

    [HttpPut("{**playlistId}")]
    public async Task<ActionResult<PlaylistReadDTO>> AddTrackToPlaylist(int userId, string playlistId, TrackReadDTO track)
    {
      try
      {
        var response = await _mediator.Send(new AddTrackToPlaylistCommand { Id = playlistId, UserId = userId, Track = track});
        return _mapper.Map<PlaylistReadDTO>(response);
      }
      catch (KeyNotFoundException)
      {
        return NotFound();
      }
    }
  }
}
