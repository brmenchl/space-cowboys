using System.Collections.Generic;
using Code.Player;

public class PlayerRegistry {
  public readonly List<PlayerFacade> players = new List<PlayerFacade>();

  public void AddPlayer(PlayerFacade player) {
    players.Add(player);
  }

  public void RemovePlayer(PlayerFacade player) {
    players.Remove(player);
  }
}