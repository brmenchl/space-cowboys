using UnityEngine;
using Zenject;

namespace Code.Ship
{
  public class ShipFacade
  {
    [Inject] public Transform Transform { get; private set; }


    public class Factory : PlaceholderFactory<ShipFacade>
    {
    }
  }
}
