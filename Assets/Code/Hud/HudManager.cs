using System.Collections.Generic;
using Code.Player;

public class HudManager {
  private PlayerRegistry playerRegistry;
  private List<PlayerFacade> players;

  public void Inject(PlayerRegistry playerRegistry) {
    this.playerRegistry = playerRegistry;
  }

  private void Start() {
    players = playerRegistry.players;
  }

  private void Update() {

  }
}