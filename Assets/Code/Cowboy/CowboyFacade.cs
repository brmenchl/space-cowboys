using System;
using Code.Input;
using Code.Players;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using External.Option;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyFacade : IControllable, IDisposable {
    private readonly BoardEjectService boardEjectService;
    private readonly CowboyModel model;
    private readonly MoveHandler moveHandler;
    private readonly ShootHandler shootHandler;
    private Option<IDisposable> inputStreamDisposable;
    private Option<IDisposable> lassoDisposable;
    private readonly SpriteRenderer renderer;

    public CowboyFacade(
      CowboyModel model,
      MoveHandler moveHandler,
      ShootHandler shootHandler,
      BoardEjectService boardEjectService,
      SpriteRenderer spriteRenderer) {
      this.model = model;
      model.OnBoarded += OnBoardedBinding;
      this.moveHandler = moveHandler;
      this.shootHandler = shootHandler;
      this.boardEjectService = boardEjectService;
      renderer = spriteRenderer;
    }

    public Sprite Sprite => renderer.sprite;

    public void UpdateController(Color playerTheme, IUniTaskAsyncEnumerable<ControllerInputState> inputStream) {
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
      renderer.color = playerTheme;
    }

    public ControllableType Type => ControllableType.Cowboy;
    public Vector2 Position => model.Transform.position;

    public void ClearController() {
      inputStreamDisposable.MatchSome(d => d.Dispose());
      inputStreamDisposable = Option.None<IDisposable>();
      lassoDisposable.MatchSome(d => d.Dispose());
      lassoDisposable = Option.None<IDisposable>();
      renderer.color = Color.white;
    }

    public void Destroy() => model.Destroy();

    public void Dispose() => ClearController();

    public void ApplyEjectForce() => moveHandler.Eject();

    private void OnBoardedBinding(IControllable controllable) => boardEjectService.Board(this, controllable);

    public class Factory : PlaceholderFactory<Vector3, Quaternion, CowboyFacade> {
    }
  }
}