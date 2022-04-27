using PlaylistService.Core.Entities;
using System.Collections.Generic;

namespace PlaylistService.Application.SyncDataServices.Grpc
{
  public interface ITrackDataClient
  {
    IEnumerable<Track> ReturnAllTracks();
  }
}
