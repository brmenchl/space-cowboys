using Code.Input;
using Zenject;

namespace Code.Player {
  public class PlayerFacade {
    public class Factory : PlaceholderFactory<ControlScheme, IControllable, PlayerFacade> {
    }
  }
}