using Code.Player;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game
{
  public class GameRunner : IInitializable
  {
    private readonly ShipFacade.Factory shipFactory;
    private readonly Pawn.Factory pawnFactory;

    public GameRunner(ShipFacade.Factory shipFactory, Pawn.Factory pawnFactory)
    {
      this.shipFactory = shipFactory;
      this.pawnFactory = pawnFactory;
    }

    public void Initialize()
    {
      var ship = shipFactory.Create();
      ship.SetPosition(new Vector3(0, 0, 0));
      var player1 = pawnFactory.Create();
      player1.SetControlScheme("WASDKeyboard");
      player1.Possess(ship.GameObject);

      var ship2 = shipFactory.Create();
      ship2.SetPosition(new Vector3(5, 0, 0));
      var player2 = pawnFactory.Create();
      player2.SetControlScheme("ArrowsKeyboard");
      player2.Possess(ship2.GameObject);
    }
  }
}
