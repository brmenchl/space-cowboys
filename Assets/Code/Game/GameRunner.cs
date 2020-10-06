using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game
{
  public class GameRunner : IInitializable
  {
    readonly ShipFacade.Factory shipFactory;

    public GameRunner(ShipFacade.Factory shipFactory)
    {
      this.shipFactory = shipFactory;
    }

    public void Initialize()
    {
      var ship = shipFactory.Create();
      ship.Transform.position = new Vector3(0, 0, 0);
    }
  }
}
