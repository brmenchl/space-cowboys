using System;
using Code.Input;
using Code.Option;
using Cysharp.Threading.Tasks;

namespace Code.Player {
  public class ControllableState {
    public AsyncReactiveProperty<Option<IControllable>> controllable;

    public ControllableState(IControllable controllable) {
      this.controllable = new AsyncReactiveProperty<Option<IControllable>>(controllable.ToOption());
    }

    public void Control(IControllable controllable) {
      var oldControllable = this.controllable;
      this.controllable.Value = controllable.ToOption();
    }

    public void IfIsControlling(Action<IControllable> f) =>
      controllable.Value.MatchSome(f);
  }
}