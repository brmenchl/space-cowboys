using Code.Input;
using Cysharp.Threading.Tasks;
using External.Option;

namespace Code.Players {
  public class Player {
    public readonly ControlScheme controlScheme;

    public AsyncReactiveProperty<Option<IControllable>> controllable =
      new AsyncReactiveProperty<Option<IControllable>>(Option.None<IControllable>());

    public AsyncReactiveProperty<float> health = new AsyncReactiveProperty<float>(100); // TODO: settings

    public Player(ControlScheme controlScheme) => this.controlScheme = controlScheme;
  }
}