using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Player.Input
{
  public class PossessableFacade : MonoBehaviour
  {
    private ShootHandler shootHandler;

    [Inject]
    public void Inject(InputState inputState, ShootHandler shootHandler)
    {
      InputState = inputState;
      this.shootHandler = shootHandler;
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
  }
}
