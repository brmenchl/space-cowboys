using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using External.Option;

namespace Code.Players {
  public class PlayerStreams : IDisposable {
    private readonly PlayerState playerState;
    private readonly CancellationTokenSource cts = new CancellationTokenSource();

    public readonly ReadOnlyAsyncReactiveProperty<IEnumerable<Guid>> countStream;

    public PlayerStreams(PlayerState playerState) {
      this.playerState = playerState;
      countStream = UniTaskAsyncEnumerable
        .EveryValueChanged(playerState, s => s.players.Count)
        .Select(_ => playerState.players.Select(x => x.id))
        .ToReadOnlyAsyncReactiveProperty(cts.Token);
    }

    public ReadOnlyAsyncReactiveProperty<Health> GetHealthStreamById(Guid playerId) {
      var player = playerState.GetPlayerById(playerId);
      return player.health.Select(h => new Health(h, player.maxHealth)).ToReadOnlyAsyncReactiveProperty(cts.Token);
    }

    public ReadOnlyAsyncReactiveProperty<Option<IControllable>> GetControllableStreamById(Guid playerId) {
      var player = playerState.GetPlayerById(playerId);
      return player.controllable.ToReadOnlyAsyncReactiveProperty(cts.Token);
    }

    public void Dispose() => cts.Cancel();
  }
}