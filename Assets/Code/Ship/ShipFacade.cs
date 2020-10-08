using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using UnityEngine;
using Zenject;

namespace Code.Ship
{
  public class ShipFacade : IPossessable
  {
    private readonly ScreenWrappingRigidbody2D rigidbody;
    private readonly ShootHandler shootHandler;

    public ShipFacade(InputState inputState, ScreenWrappingRigidbody2D rigidbody, ShootHandler shootHandler)
    {
      this.rigidbody = rigidbody;
      this.shootHandler = shootHandler;
      InputState = inputState;
    }

    public void SetPosition(Vector3 position)
    {
      rigidbody.SetPosition(position);
    }


    public InputState InputState { get; private set; }

    public bool IsPossessed => InputState.IsEnabled;

    public void Possess()
    {
      InputState.Enable();
    }

    public void Depossess()
    {
      InputState.Disable();
    }

    public void Shoot()
    {
      shootHandler.Shoot();
    }

    public class Factory : PlaceholderFactory<ShipFacade>
    {
    }
  }
}
