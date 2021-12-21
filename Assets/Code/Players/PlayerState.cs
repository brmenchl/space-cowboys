using System;
using System.Collections.Generic;
using System.Linq;
using External.Option;

namespace Code.Players {
  public class PlayerState {
    public readonly List<Player> players =
      new List<Player>(new List<Player>());

    public Option<Player> GetPlayerBy(Func<Player, bool> predicate) =>
      players.FirstOrNone(predicate);

    public Player GetPlayerById(Guid id) =>
      players.First(p => p.id == id);
  }
}