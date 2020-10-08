using Code.Player;
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
    private readonly InputHandler inputHandler;

    public ShipFacade(InputHandler inputHandler, ScreenWrappingRigidbody2D rigidbody, ShootHandler shootHandler)
    {
      this.rigidbody = rigidbody;
      this.shootHandler = shootHandler;
      this.inputHandler = inputHandler;
    }

    public void SetPosition(Vector3 position)
    {
      rigidbody.SetPosition(position);
    }

    public bool IsPossessed => inputHandler.HasLinkedInputState;

    public void Possess(Pawn pawn)
    {
      inputHandler.LinkInputState(pawn.InputState);
    }

    public void Depossess()
    {
      inputHandler.ClearInputState();
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
