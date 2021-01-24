using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Player.Input {
  [RequireComponent(typeof(PlayerInput))]
  public class Pawn : MonoBehaviour {
    private string controlScheme;
    public InputState inputState;
    private PlayerInput playerInput;

    private void Start() {
      playerInput = gameObject.GetComponent<PlayerInput>();
      playerInput.SwitchCurrentControlScheme(controlScheme);
      playerInput.onActionTriggered += OnActionTriggered;
    }

    [Inject]
    public void Inject(string controlScheme, InputState inputState) {
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

    public class Factory : PlaceholderFactory<string, Pawn> {
    }
  }
}