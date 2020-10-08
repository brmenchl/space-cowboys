using Code.Player;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game
{
  public class GameRunner : IInitializable
  {
    private readonly Pawn.Factory pawnFactory;
    private readonly ShipFacade.Factory shipFactory;

    public GameRunner(ShipFacade.Factory shipFactory, Pawn.Factory pawnFactory)
    {
      this.shipFactory = shipFactory;
      this.pawnFactory = pawnFactory;
    }

    public void Initialize()
    {
      var ship = shipFactory.Create(new Vector3(0, 0, 0), Quaternion.AngleAxis(90, Vector3.forward));
      var player1 = pawnFactory.Create("WASDKeyboard");
      player1.Possess(ship);

      var ship2 = shipFactory.Create(new Vector3(3, 0, 0), Quaternion.identity);
      var player2 = pawnFactory.Create("ArrowsKeyboard");
      player2.Possess(ship2);
    }
  }
}
