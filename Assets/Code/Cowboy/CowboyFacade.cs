using System;
using Code.Input;
using Code.Player;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using External.Option;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyFacade : IControllable, IDisposable {
    private readonly CowboyModel model;
    private readonly MoveHandler moveHandler;
    private readonly ShootHandler shootHandler;
    private readonly BoardEjectService boardEjectService;
    private Option<int> playerId = Option.None<int>();
    private Option<IDisposable> inputStreamDisposable;
    private Option<IDisposable> lassoDisposable;

    public CowboyFacade(
      CowboyModel model,
      MoveHandler moveHandler,
      ShootHandler shootHandler,
      BoardEjectService boardEjectService) {
      this.model = model;
      model.OnBoarded += OnBoardedBinding;
      this.moveHandler = moveHandler;
      this.shootHandler = shootHandler;
      this.boardEjectService = boardEjectService;
    }

    public void ApplyEjectForce() => moveHandler.Eject();

    public void UpdateController(int playerId, IUniTaskAsyncEnumerable<ControllerInputState> inputStream) {
      this.playerId = playerId.ToOption();
      ClearController();
      inputStreamDisposable = inputStream.Subscribe(state => {
        moveHandler.Turn(state.movement.x);
        if (state.primary) shootHandler.Shoot();
      }).ToOption();
      lassoDisposable = inputStream
        .Select(s => s.alt)
        .DistinctUntilChanged()
        .Where(a => a)
        .Skip(1)
        .Subscribe(_ => model.FireLasso().Forget())
        .ToOption();
    }

    public ControllableType Type => ControllableType.Cowboy;
    public Vector2 Position => model.Transform.position;

    public void ClearController() {
      inputStreamDisposable.MatchSome(d => d.Dispose());
      inputStreamDisposable = Option.None<IDisposable>();
      lassoDisposable.MatchSome(d => d.Dispose());
      lassoDisposable = Option.None<IDisposable>();
    }

    public void Destroy() => model.Destroy();

    private void OnBoardedBinding(IControllable controllable) => boardEjectService.Board(playerId, controllable);

    public void Dispose() => ClearController();

    public class Factory : PlaceholderFactory<Vector3, Quaternion, CowboyFacade> {
    }
  }
}