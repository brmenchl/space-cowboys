using Code.Cowboy;
using Code.Player.Input;
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
      var player1 = pawnFactory.Create(ControlScheme.wasdKeyboard);
      cowboy.Possess(player1);

      // var ship1 = shipFactory.Create(new Vector3(0, 0, 0), Quaternion.AngleAxis(90, Vector3.forward));
      // var player1 = pawnFactory.Create(ControlScheme.wasdKeyboard");
      // ship1.Possess(player1);

      var ship2 = shipFactory.Create(new Vector3(3, 0, 0), Quaternion.identity);
      var player2 = pawnFactory.Create(ControlScheme.arrowsKeyboard);
      ship2.Possess(player2);
    }
  }
}