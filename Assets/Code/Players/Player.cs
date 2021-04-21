using Code.Input;
using External.Option;

namespace Code.Players {
  public class Player {
    public readonly ControlScheme controlScheme;
    public Option<IControllable> controllable = Option.None<IControllable>();
    public float health = 100f;

    public Player(ControlScheme controlScheme) => this.controlScheme = controlScheme;
  }
}