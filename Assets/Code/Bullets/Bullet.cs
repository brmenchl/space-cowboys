using System;
using System.Threading;
using Code.Ship;
using Cysharp.Threading.Tasks;
using LanguageExt;
using UnityEngine;
using Zenject;

namespace Code.Bullets {
  using static Prelude;

  public class Bullet : MonoBehaviour, IPoolable<Vector3, Quaternion, IMemoryPool>, IDisposable {
    private CancellationTokenSource disposeLifeTimeCancelToken;
    private Option<IMemoryPool> pool;
    private Settings settings;

    private void Update() => transform.Translate(transform.up * (settings.speed * Time.deltaTime), Space.World);

    private void OnTriggerEnter2D(Collider2D other) =>
      Optional(other.gameObject.GetComponent<ShipView>()).IfSome(view => {
        view.Facade.Damage(settings.damage);
        Dispose();
      });

    public void Dispose() => ifSome(pool, p => p.Despawn(this));

    public void OnSpawned(Vector3 pos, Quaternion rot, IMemoryPool pool) {
      this.pool = Optional(pool);
      var trans = transform;
      trans.position = pos;
      trans.rotation = rot;
      disposeLifeTimeCancelToken = new CancellationTokenSource();
      DisposeAfterLifeTime().Forget();
    }

    public void OnDespawned() {
      pool = None;
      transform.position = Vector3.zero;
      disposeLifeTimeCancelToken.Cancel();
    }

    [Inject]
    public void Inject(Settings settings) => this.settings = settings;

    private async UniTaskVoid DisposeAfterLifeTime() {
      await UniTask.Delay(TimeSpan.FromSeconds(settings.lifeTime)).WithCancellation(disposeLifeTimeCancelToken.Token);
      Dispose();
    }

    public class Factory : PlaceholderFactory<Vector3, Quaternion, Bullet> {
    }

    [Serializable]
    public class Settings {
      public float speed;
      public float lifeTime;
      public float damage;
    }
  }
}