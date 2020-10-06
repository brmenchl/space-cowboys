using UnityEngine;
using Zenject;

public class GameRunner : IInitializable
{
  ShipFacade.Factory shipFactory;

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
