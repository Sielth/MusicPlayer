using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlaylistService.Application.CQRS.Commands;
using PlaylistService.Application.CQRS.Queries;
using PlaylistService.Application.CQRS.Responses;
using PlaylistService.Shared.DTOs.PlaylistDTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistService.API.Controllers
{
    [Route("api/p/{userId}/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PlaylistController(IMediator mediator, IMapper mapper)
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

        [HttpGet("{playlistId}", Name = "GetPlaylist")]
        public async Task<ActionResult<PlaylistReadDTO>> GetPlaylist(int userId, int playlistId)
        {
            try
            {
                var response = await _mediator.Send(new GetPlaylistQuery { UserId = userId, Id = playlistId });

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
    }
}
