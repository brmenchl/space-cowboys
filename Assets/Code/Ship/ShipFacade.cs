using System;
using Code.Input;
using Code.Players;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using External.Option;
using UnityEngine;
using Zenject;

namespace Code.Ship {
  public class ShipFacade : IControllable, IDisposable {
    private readonly BoardEjectService boardEjectService;
    private readonly ShipModel model;
    private readonly MoveHandler moveHandler;
    private readonly ShootHandler shootHandler;
    private Option<IDisposable> ejectDisposable;
    private Option<IDisposable> inputStreamDisposable;
    private readonly SpriteRenderer renderer;

    public ShipFacade(BoardEjectService boardEjectService,
      ShipModel model,
      MoveHandler moveHandler,
      ShootHandler shootHandler,
      SpriteRenderer spriteRenderer) {
      this.boardEjectService = boardEjectService;
      this.model = model;
      this.moveHandler = moveHandler;
      this.shootHandler = shootHandler;
      model.OnDestroyed += Eject;
      renderer = spriteRenderer;
    }

    public Sprite Sprite => renderer.sprite;

    public void UpdateController(Color playerTheme, IUniTaskAsyncEnumerable<ControllerInputState> inputStream) {
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
      renderer.color = playerTheme;
    }

    public ControllableType Type => ControllableType.Vehicle;
    public Vector2 Position => model.transform.position;

    public void ClearController() {
      inputStreamDisposable.MatchSome(d => d.Dispose());
      inputStreamDisposable = Option.None<IDisposable>();
      ejectDisposable.MatchSome(d => d.Dispose());
      ejectDisposable = Option.None<IDisposable>();
      renderer.color = Color.white;
    }

    public void Destroy() => model.Destroy();

    public void Dispose() => ClearController();

    private void Eject() => boardEjectService.Eject(this);
    public void Damage(float damage) => model.Damage(damage);

    public class Factory : PlaceholderFactory<Vector3, Quaternion, ShipFacade> {
    }
  }
}