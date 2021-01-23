namespace Code.Ship {
  using UnityEngine;

  using Zenject;

  public class ShipView : MonoBehaviour {
    public ShipFacade Facade { get; private set; }

    [Inject]
    public void Inject(ShipFacade facade) => Facade = facade;
  }
}