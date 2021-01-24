using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyFacade : IPossessable {
    private readonly InputHandler inputHandler;

    public CowboyFacade(InputHandler inputHandler) =>
      this.inputHandler = inputHandler;

    public void Possess(Pawn pawn) => inputHandler.Possess(pawn);

    public void Depossess() => inputHandler.Depossess();

    public class Factory : PlaceholderFactory<Vector3, Quaternion, CowboyFacade> {
    }
  }
}