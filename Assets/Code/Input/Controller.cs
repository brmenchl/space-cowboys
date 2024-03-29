using External.Option;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Input {
  [RequireComponent(typeof(PlayerInput))]
  public class Controller : MonoBehaviour {
    private ControlScheme controlScheme;
    private InputService inputService;
    private PlayerInput playerInput;

    private void Start() {
      playerInput = gameObject.TryGetComponent<PlayerInput>();
      playerInput.SwitchCurrentControlScheme(controlScheme.ToString(), Keyboard.current);
      playerInput.onActionTriggered += OnActionTriggered;
    }

    [Inject]
    public void Inject(ControlScheme controlScheme, InputService inputService) {
      this.controlScheme = controlScheme;
      this.inputService = inputService;
    }

    private void OnActionTriggered(InputAction.CallbackContext context) {
      switch (context.action.name) {
        case "Movement":
          inputService.SetMovementState(controlScheme, context.ReadValue<Vector2>());
          break;
        case "Primary":
          inputService.SetPrimaryButtonState(controlScheme, context.ReadValue<float>() != 0);
          break;
        case "Alt":
          inputService.SetAltButtonState(controlScheme, context.ReadValue<float>() != 0);
          break;
      }
    }

    public class Factory : PlaceholderFactory<ControlScheme, Controller> {
    }
  }
}