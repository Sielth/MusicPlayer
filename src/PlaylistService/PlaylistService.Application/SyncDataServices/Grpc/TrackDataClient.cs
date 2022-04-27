using AutoMapper;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PlaylistService.Core.Entities;
using System;
using System.Collections.Generic;
using TrackService;

namespace PlaylistService.Application.SyncDataServices.Grpc
{
  public class TrackDataClient : ITrackDataClient
  {
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public TrackDataClient(IMapper mapper, IConfiguration configuration)
    {
      _mapper = mapper;
      _configuration = configuration;
    }

    public IEnumerable<Track> ReturnAllTracks()
    {
      Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcTracks"]}");

      var channel = GrpcChannel.ForAddress(_configuration["GrpcTrack"]);
      var client = new GrpcTrack.GrpcTrackClient(channel);
      var request = new GetRequests();

      try
      {
        var reply = client.GetTracks(request);
        return _mapper.Map<IEnumerable<Track>>(reply.Track);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"--> Could not call GRPC Server {ex}");
        return null;
      }
    }
  }
}
