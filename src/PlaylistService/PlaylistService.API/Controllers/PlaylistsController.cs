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
    public async Task<ActionResult<ReadPlaylistDTO>> CreatePlaylist(int userId, CreatePlaylistDTO playlist)
    {
      try
      {
        var request = _mapper.Map<CreatePlaylistCommand>(playlist);

        request.UserId = userId;

        var response = await _mediator.Send(request);

        return _mapper.Map<ReadPlaylistDTO>(response);
      }
      catch (KeyNotFoundException)
      {
        return NotFound();
      }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadPlaylistDTO>>> GetPlaylistsForUser(int userId)
    {
      try
      {
        var response = await _mediator.Send(new GetPlaylistsForUserQuery { UserId = userId });

        return _mapper.Map<IEnumerable<ReadPlaylistDTO>>(response).ToList();
      }
      catch (KeyNotFoundException)
      {
        return NotFound();
      }
    }

    [HttpGet("{**playlistId}", Name = "GetPlaylistForUser")]
    public async Task<ActionResult<ReadPlaylistDTO>> GetPlaylistForUser(int userId, string playlistId)
    {
      try
      {
        var response = await _mediator.Send(new GetPlaylistForUserQuery { UserId = userId, Id = playlistId });

        return _mapper.Map<ReadPlaylistDTO>(response);
      }
      catch (KeyNotFoundException)
      {
        return NotFound();
      }
    }

    [HttpPut("{**playlistId}")]
    public async Task<ActionResult<ReadPlaylistDTO>> AddTrackToPlaylist(int userId, string playlistId, string trackId)
    {
      try
      {
        var response = await _mediator.Send(new AddTrackToPlaylistCommand { Id = playlistId, UserId = userId, TrackId = trackId });
        return _mapper.Map<ReadPlaylistDTO>(response);
      }
      catch (KeyNotFoundException)
      {
        return NotFound();
      }
    }
  }
}
