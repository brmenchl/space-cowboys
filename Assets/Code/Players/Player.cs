using Code.Input;
using Cysharp.Threading.Tasks;
using External.Option;
using UnityEngine;
using Zenject;

namespace Code.Players {
  public class Player {
    public readonly ControlScheme controlScheme;
    public readonly Color theme;
    public readonly Settings settings;
    public readonly AsyncReactiveProperty<Option<IControllable>> controllable =
      new AsyncReactiveProperty<Option<IControllable>>(Option.None<IControllable>());
    public readonly AsyncReactiveProperty<float> health = new AsyncReactiveProperty<float>(100); // TODO: settings

    public Player(ControlScheme controlScheme, Color theme, Settings settings) {
      this.controlScheme = controlScheme;
      this.theme = theme;
      this.settings = settings;
    public class Factory : PlaceholderFactory<ControlScheme, Color, Player> { }

    [Serializable]
    public class Settings {
      public float startingHealth;
    }
  }
}