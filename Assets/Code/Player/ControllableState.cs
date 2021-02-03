using System;
using Code.Input;

namespace Code.Player {
  public class ControllableState {
    private IControllable controllable;

    public ControllableState(IControllable controllable) => this.controllable = controllable;

    public event Action<IControllable, IControllable> OnNewControllable;

    public void Control(IControllable controllable) {
      var oldControllable = this.controllable;
      this.controllable = controllable;
      OnNewControllable?.Invoke(oldControllable, this.controllable);
    }

    public void IfIsControlling(Action<IControllable> isControlling) {
      if (controllable != null) isControlling(controllable);
    }
  }
}