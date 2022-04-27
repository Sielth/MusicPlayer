using AutoMapper;
using Grpc.Core;
using System.Threading.Tasks;
using TrackService.Application.Repo;

namespace TrackService.Application.SyncDataServices.Grpc
{
  public class GrpcTrackService : GrpcTrack.GrpcTrackBase
  {
    private readonly ITrackRepo _repository;
    private readonly IMapper _mapper;

    public GrpcTrackService(IMapper mapper, ITrackRepo repository)
    {
      _mapper = mapper;
      _repository = repository;
    }

    public async override Task<TrackResponse> GetTracks(GetRequests request, ServerCallContext context)
    {
      var response = new TrackResponse();
      var tracks = await _repository.GetTracks();

      foreach (var track in tracks)
      {
        response.Track.Add(_mapper.Map<GrpcTrackModel>(track));
      }

      return response;
    }
  }
}
