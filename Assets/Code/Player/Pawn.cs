using Code.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
  [RequireComponent(typeof(PlayerInput))]
  public class Pawn : MonoBehaviour
  {
    private PossessableFacade possessable;
    private bool hasStarted;

    private void Start()
    {
      var playerInput = gameObject.GetComponent<PlayerInput>();
      playerInput.onActionTriggered += OnActionTriggered;
      EstablishPossession();
      hasStarted = true;
    }

    private void OnTransformParentChanged()
    {
      // OnTransformParentChanged runs before MonoBehaviour injection,
      // so we need to make sure we establish possession at Start at the earliest
      if (hasStarted) EstablishPossession();
    }

    private void EstablishPossession()
    {
      var newPossessable = transform.parent.GetComponent<PossessableFacade>();
      if (newPossessable == null || newPossessable.IsPossessed) return;

      if (possessable != null)
      {
        possessable.Depossess();
      }

      newPossessable.Possess();
      possessable = newPossessable;
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
      if (possessable == null) return;

      switch (context.action.name)
      {
        case "Movement":
          possessable.InputState.Movement = context.ReadValue<Vector2>();
          break;
        case "Shoot":
          possessable.Shoot();
          break;
      }
    }
  }
}
