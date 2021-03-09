using System;
using Code.Input;
using Code.Option;
using Code.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Cowboy {
  public class CowboyModel {
    private readonly GameObject gameObject;
    private readonly Lasso.Lasso.Factory lassoFactory;
    private bool hasFiredLasso;
    private Option<Lasso.Lasso> lasso;
    private Option<IBoardable> reelSubject;

    public CowboyModel(
      Lasso.Lasso.Factory lassoFactory,
      Transform transform,
      Vector3 position,
      Quaternion rotation
    ) {
      this.lassoFactory = lassoFactory;
      transform.position = position;
      transform.rotation = rotation;
      gameObject = transform.gameObject;
    }

    public event Action<IControllable> OnBoarded;

    public void Destroy() => Object.Destroy(gameObject);

    public void TryBoard(IControllable other) {
      lasso.MatchSome(l => Object.Destroy(l.gameObject));
      /*if (reelSubject.Exists(rs => other == rs))*/
      OnBoarded?.Invoke(other);
    }

    public async UniTaskVoid FireLasso() {
      if (!hasFiredLasso) {
        hasFiredLasso = true;
        lasso = lassoFactory.Create().ToOption();
        await UniTask.Delay(1000);
        hasFiredLasso = false;
      }
    }
  }
}