using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using UnityEngine;
using Zenject;

namespace Code.Ship
{
  public class ShipFacade
  {
    private readonly ScreenWrappingRigidbody2D rigidbody;

    public ShipFacade(ScreenWrappingRigidbody2D rigidbody)
    {
      this.rigidbody = rigidbody;
    }

    public void SetPosition(Vector3 position)
    {
      rigidbody.SetPosition(position);
    }

    public GameObject GameObject => rigidbody.gameObject;

    public class Factory : PlaceholderFactory<ShipFacade>
    {
    }
  }
}
