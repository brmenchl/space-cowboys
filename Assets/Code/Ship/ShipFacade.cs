using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Ship {
  public class ShipFacade : IPossessable {
    private readonly InputHandler inputHandler;
    private readonly ShipModel model;

    public ShipFacade(
      ShipModel model,
      InputHandler inputHandler
    ) {
      this.model = model;
      this.inputHandler = inputHandler;
    }

    public void Possess(Pawn pawn) => inputHandler.Possess(pawn);

    public void Depossess() =>
      inputHandler.Depossess();

    public void Damage(float damage) => model.Damage(damage);

    public class Factory : PlaceholderFactory<Vector3, Quaternion, ShipFacade> {
    }
  }
}