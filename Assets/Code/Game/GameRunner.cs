using Code.Cowboy;
using Code.Player;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game {
  public class GameRunner : IInitializable {
    private readonly CowboyFacade.Factory cowboyFactory;
    private readonly Pawn.Factory pawnFactory;
    private readonly ShipFacade.Factory shipFactory;

    public GameRunner(Pawn.Factory pawnFactory, ShipFacade.Factory shipFactory, CowboyFacade.Factory cowboyFactory) {
      this.pawnFactory = pawnFactory;
      this.shipFactory = shipFactory;
      this.cowboyFactory = cowboyFactory;
    }

    public void Initialize() {
      var cowboy = cowboyFactory.Create(Vector3.zero, Quaternion.identity);
      var player1 = pawnFactory.Create("WASDKeyboard");
      player1.Possess(cowboy);

      // var ship = shipFactory.Create(new Vector3(0, 0, 0), Quaternion.AngleAxis(90, Vector3.forward));
      // var player1 = pawnFactory.Create("WASDKeyboard");
      // player1.Possess(ship);

      var ship2 = shipFactory.Create(new Vector3(3, 0, 0), Quaternion.identity);
      var player2 = pawnFactory.Create("ArrowsKeyboard");
      player2.Possess(ship2);
    }
  }
}