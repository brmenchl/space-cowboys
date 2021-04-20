using Code.Input;
using External.Option;

namespace Code.Player {
  public class Player {
    public readonly ControlScheme controlScheme;
    public float health = 100f;
    public Option<IControllable> controllable = Option.None<IControllable>();

    public Player(ControlScheme controlScheme) => this.controlScheme = controlScheme;
  }
}