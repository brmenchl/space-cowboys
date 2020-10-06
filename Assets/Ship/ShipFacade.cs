using UnityEngine;
using Zenject;

public class ShipFacade
{

  [Inject]
  public Transform Transform { get; private set; }


  public class Factory : PlaceholderFactory<ShipFacade>
  {
  }
}
