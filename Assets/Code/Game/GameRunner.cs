using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game
{
  public class GameRunner : IInitializable
  {
    private readonly ShipFacade.Factory shipFactory;

    public GameRunner(ShipFacade.Factory shipFactory)
    {
      this.shipFactory = shipFactory;
    }

    public void Initialize()
    {
      var ship = shipFactory.Create();
      ship.SetPosition(new Vector3(0, 0, 0));
    }
  }
}
