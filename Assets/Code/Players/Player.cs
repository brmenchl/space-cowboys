using Code.Input;
using Cysharp.Threading.Tasks;
using External.Option;
using UnityEngine;

namespace Code.Players {
  public class Player {
    public readonly ControlScheme controlScheme;
    public readonly Color theme;
    public readonly AsyncReactiveProperty<Option<IControllable>> controllable =
      new AsyncReactiveProperty<Option<IControllable>>(Option.None<IControllable>());
    public readonly AsyncReactiveProperty<float> health = new AsyncReactiveProperty<float>(100); // TODO: settings

    public Player(ControlScheme controlScheme, Color theme) {
      this.controlScheme = controlScheme;
      this.theme = theme;
    }
  }
}