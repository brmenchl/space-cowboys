using System;
using Code.Input;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Code.Player {
  public class ControllableState {
    private Option<IControllable> controllable;

    public ControllableState(IControllable controllable) => this.controllable = Optional(controllable);

    public event Action<Option<IControllable>, Option<IControllable>> OnNewControllable;

    public void Control(IControllable controllable) {
      var oldControllable = this.controllable;
      this.controllable = Optional(controllable);
      OnNewControllable?.Invoke(oldControllable, this.controllable);
    }

    public void IfIsControlling(Action<IControllable> isControlling) => controllable.IfSome(isControlling);
  }
}