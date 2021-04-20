using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Input {
  public class InputService {
    private readonly Controller.Factory controllerFactory;
    private readonly InputState inputState;

    public InputService(InputState inputState, Controller.Factory controllerFactory) {
      this.inputState = inputState;
      this.controllerFactory = controllerFactory;
    }

    public void AddController(ControlScheme controlScheme) {
      ValidateControlScheme(controlScheme);
      inputState.inputs.Add(controlScheme, new ControllerInputStream());
      controllerFactory.Create(controlScheme);
    }

    public void SetMovementState(ControlScheme controlScheme, Vector2 value) =>
      inputState.inputs[controlScheme].movement = value;

    public void SetPrimaryButtonState(ControlScheme controlScheme, bool value) =>
      inputState.inputs[controlScheme].primary = value;

    public void SetAltButtonState(ControlScheme controlScheme, bool value) =>
      inputState.inputs[controlScheme].alt = value;

    public IUniTaskAsyncEnumerable<ControllerInputState> GetInputStream(ControlScheme controlScheme) =>
      inputState.inputs[controlScheme].state;

    private void ValidateControlScheme(ControlScheme controlScheme) {
      if (inputState.inputs.ContainsKey(controlScheme))
        throw new Exception($"Control Scheme ${controlScheme} already registered");
    }
  }
}