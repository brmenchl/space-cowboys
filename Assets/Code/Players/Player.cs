using System;
using System.Threading;
using Code.Input;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using External.Option;
using UnityEngine;
using Zenject;

namespace Code.Players {
  public class Player : IDisposable {
    public readonly ControlScheme controlScheme;
    public readonly Color theme;
    public readonly Settings settings;
    public readonly AsyncReactiveProperty<Option<IControllable>> controllable =
      new AsyncReactiveProperty<Option<IControllable>>(Option.None<IControllable>());
    public readonly AsyncReactiveProperty<float> health;
    public readonly ReadOnlyAsyncReactiveProperty<float> healthPercentStream;
    private readonly CancellationTokenSource token = new CancellationTokenSource();

    public Player(ControlScheme controlScheme, Color theme, Settings settings) {
      this.controlScheme = controlScheme;
      this.theme = theme;
      this.settings = settings;
      health = new AsyncReactiveProperty<float>(settings.startingHealth);
      healthPercentStream = health.Select(h => h / settings.startingHealth).ToReadOnlyAsyncReactiveProperty(token.Token);
    }

    public void Dispose() {
      token.Cancel();
    }

    public class Factory : PlaceholderFactory<ControlScheme, Color, Player> { }

    [Serializable]
    public class Settings {
      public float startingHealth;
    }
  }
}