using UnityEngine;
using Zenject;

namespace Code.Ship
{
  public class ShipView : MonoBehaviour
  {
    public ShipFacade Facade { get; private set; }

    [Inject]
    public void Inject(ShipFacade facade)
    {
      Facade = facade;
    }
  }
}
