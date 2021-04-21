using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Code.Players {
  public class PlayerStreams {
    private readonly PlayerState playerState;

    public PlayerStreams(PlayerState playerState) => this.playerState = playerState;

    public IUniTaskAsyncEnumerable<List<Player>> CountStream =>
      UniTaskAsyncEnumerable
        .EveryValueChanged(playerState, s => s.players.Count)
        .Select(_ => playerState.players);
  }
}