using System;
using Code.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Player
{
  [RequireComponent(typeof(PlayerInput))]
  public class Pawn : MonoBehaviour
  {
    private PossessableFacade possessable;
    private bool hasStarted;
    private PlayerInput playerInput;

    private void Start()
    {
      playerInput = gameObject.GetComponent<PlayerInput>();
      playerInput.onActionTriggered += OnActionTriggered;
      EstablishPossessionFromParent();
      hasStarted = true;
    }

    private void OnTransformParentChanged()
    {
      // OnTransformParentChanged runs before MonoBehaviour injection,
      // so we need to make sure we establish possession at Start at the earliest
      if (hasStarted) EstablishPossessionFromParent();
    }

    public void Possess(GameObject toPossess)
    {
      transform.parent = toPossess.transform;
    }

    public void SetControlScheme(string controlScheme)
    {
      playerInput = gameObject.GetComponent<PlayerInput>();
      playerInput.SwitchCurrentControlScheme(controlScheme);
    }

    private void EstablishPossessionFromParent()
    {
      var newPossessable = transform.parent.GetComponent<PossessableFacade>();
      if (newPossessable == null || newPossessable.IsPossessed)
      {
        throw new Exception($"Cannot Possess gameObject {transform.parent.name}");
      }

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

    public class Factory : PlaceholderFactory<Pawn>
    {
    }
  }
}
