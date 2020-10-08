using Code.Player;
using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using UnityEngine;
using Zenject;

namespace Code.Ship
{
  public class ShipFacade : IPossessable
  {
    private readonly HealthHandler healthHandler;
    private readonly InputHandler inputHandler;
    private readonly ScreenWrappingRigidbody2D rigidbody;
    private readonly ShootHandler shootHandler;

    public ShipFacade(InputHandler inputHandler, ScreenWrappingRigidbody2D rigidbody, ShootHandler shootHandler,
      HealthHandler healthHandler)
    {
      this.rigidbody = rigidbody;
      this.shootHandler = shootHandler;
      this.healthHandler = healthHandler;
      this.inputHandler = inputHandler;
    }

    public bool IsPossessed => inputHandler.IsPossessed;

    public void Possess(Pawn pawn)
    {
      inputHandler.Possess(pawn);
    }

    public void Depossess()
    {
      inputHandler.Depossess();
    }

    public void Shoot()
    {
      shootHandler.Shoot();
    }

    public void SetPosition(Vector3 position)
    {
      rigidbody.SetPosition(position);
    }

    public void Damage(float damage)
    {
      healthHandler.Damage(damage);
    }

    public class Factory : PlaceholderFactory<ShipFacade>
    {
    }
  }
}
