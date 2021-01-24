using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Player.Input {
  [RequireComponent(typeof(PlayerInput))]
  public class Pawn : MonoBehaviour {
    private ControlScheme controlScheme;
    public InputState inputState;
    private PlayerInput playerInput;

    private void Start() {
      playerInput = gameObject.GetComponent<PlayerInput>();
      playerInput.SwitchCurrentControlScheme(controlScheme.ToString());
      playerInput.onActionTriggered += OnActionTriggered;
    }

    [Inject]
    public void Inject(ControlScheme controlScheme, InputState inputState) {
      this.controlScheme = controlScheme;
      this.inputState = inputState;
    }

    private void OnActionTriggered(InputAction.CallbackContext context) {
      switch (context.action.name) {
        case "Movement":
          inputState.movement = context.ReadValue<Vector2>();
          break;
        case "Shoot":
          inputState.isShooting = context.ReadValue<float>() != 0;
          break;
      }
    }
  }
}