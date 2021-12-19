using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Code.Players {
  public class PlayerStreams : IDisposable {
    private readonly PlayerState playerState;
    private readonly CancellationTokenSource token;
    public ReadOnlyAsyncReactiveProperty<List<Player>> countStream;

    public PlayerStreams(PlayerState playerState) {
      this.playerState = playerState;
      token = new CancellationTokenSource();
      countStream = UniTaskAsyncEnumerable
        .EveryValueChanged(playerState, s => s.players.Count)
        .Select(_ => playerState.players).ToReadOnlyAsyncReactiveProperty(token.Token);
    }

    public void Dispose() {
      token.Cancel();
    }
  }
}