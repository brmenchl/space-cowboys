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
    public readonly InputState inputState = new InputState();
    private PlayerInput playerInput;
    private IPossessable possessable;

    private void Start()
    {
      playerInput = gameObject.GetComponent<PlayerInput>();
      playerInput.onActionTriggered += OnActionTriggered;
    }

    public void Possess(IPossessable newPossessable)
    {
      if (newPossessable == null || newPossessable.IsPossessed) throw new Exception("Cannot possess gameObject");

      possessable?.Depossess();
      newPossessable.Possess(this);
      possessable = newPossessable;
    }

    public void SetControlScheme(string controlScheme)
    {
      playerInput = gameObject.GetComponent<PlayerInput>();
      playerInput.SwitchCurrentControlScheme(controlScheme);
    }

    public void OnPossessableDestroy()
    {
      possessable = null;
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
      if (possessable == null) return;

      switch (context.action.name)
      {
        case "Movement":
          inputState.Movement = context.ReadValue<Vector2>();
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
