using Code.Input;
using Zenject;

namespace Code.Player {
  public class PlayerFacade {

    public PlayerFacade(PlayerRegistry playerRegistry) {
      playerRegistry.AddPlayer(this);
    }

    public class Factory : PlaceholderFactory<ControlScheme, IControllable, PlayerFacade> {
    }
  }
}