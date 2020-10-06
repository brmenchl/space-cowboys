using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Player.Input
{
  [RequireComponent(typeof(PlayerInput))]
  public class PlayerInputHandler : MonoBehaviour
  {
    private SignalBus signalBus;
    private InputState inputState;

    [Inject]
    public void Inject(SignalBus signalBus, InputState inputState)
    {
      this.signalBus = signalBus;
      this.inputState = inputState;
    }


    private void Start()
    {
      var playerInput = gameObject.GetComponent<PlayerInput>();
      playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
      switch (context.action.name)
      {
        case "Movement":
          inputState.MovementInputState = context.ReadValue<Vector2>();
          break;
        case "Shoot":
          signalBus.Fire<ShootSignal>();
          break;
      }
    }
  }
}
