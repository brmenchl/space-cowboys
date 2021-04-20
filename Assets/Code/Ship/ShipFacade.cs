using System;
using Code.Input;
using Code.Player;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using External.Option;
using UnityEngine;
using Zenject;

namespace Code.Ship {
  public class ShipFacade : IControllable, IDisposable {
    private readonly ShipModel model;
    private readonly MoveHandler moveHandler;
    private readonly ShootHandler shootHandler;
    private readonly BoardEjectService boardEjectService;
    private Option<int> playerId = Option.None<int>();
    private Option<IDisposable> inputStreamDisposable;
    private Option<IDisposable> ejectDisposable;

    public ShipFacade(BoardEjectService boardEjectService,
      ShipModel model,
      MoveHandler moveHandler,
      ShootHandler shootHandler) {
      this.boardEjectService = boardEjectService;
      this.model = model;
      this.moveHandler = moveHandler;
      this.shootHandler = shootHandler;
      model.OnDestroyed += Eject;
    }

    private void Eject() => boardEjectService.Eject(playerId);
    public void Damage(float damage) => model.Damage(damage);

    public class Factory : PlaceholderFactory<Vector3, Quaternion, ShipFacade> {
    }

    public void UpdateController(int playerId, IUniTaskAsyncEnumerable<ControllerInputState> inputStream) {
      this.playerId = playerId.ToOption();
      inputStreamDisposable = inputStream.Subscribe(state => {
        moveHandler.Thrust(state.movement.y);
        moveHandler.Turn(state.movement.x);
        if (state.primary) shootHandler.Shoot();
      }).ToOption();
      ejectDisposable = inputStream
        .Select(s => s.alt)
        .DistinctUntilChanged()
        .Where(a => a)
        .Subscribe(_ => Eject())
        .ToOption();
    }

    public ControllableType Type => ControllableType.Vehicle;
    public Vector2 Position => model.transform.position;

    public void ClearController() {
      inputStreamDisposable.MatchSome(d => d.Dispose());
      inputStreamDisposable = Option.None<IDisposable>();
      ejectDisposable.MatchSome(d => d.Dispose());
      ejectDisposable = Option.None<IDisposable>();
    }

    public void Destroy() => model.Destroy();

    public void Dispose() => ClearController();
  }
}