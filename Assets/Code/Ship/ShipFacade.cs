using Code.Player;
using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Ship {
  public class ShipFacade : IPossessable {
    private readonly HealthHandler healthHandler;
    private readonly InputHandler inputHandler;

    public ShipFacade(
      InputHandler inputHandler,
      HealthHandler healthHandler
    ) {
      this.healthHandler = healthHandler;
      this.inputHandler = inputHandler;
    }

    public void Possess(Pawn pawn) => inputHandler.Possess(pawn);

    public void Depossess() =>
      inputHandler.Depossess();

    public void Damage(float damage) => healthHandler.Damage(damage);

    public class Factory : PlaceholderFactory<Vector3, Quaternion, ShipFacade> {
    }
  }
}