using System;
using System.Threading;
using Code.Ship;
using Cysharp.Threading.Tasks;
using LanguageExt;
using UnityEngine;
using Zenject;

namespace Code.Bullets {
  using static Prelude;

  public class Bullet : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable {
    private CancellationTokenSource disposeLifeTimeCancelToken;
    private IMemoryPool pool;
    private Settings settings;

    [Inject]
    public void Inject(Settings settings) => this.settings = settings;

    private void Update() => transform.Translate(transform.up * (settings.speed * Time.deltaTime), Space.World);

    private void OnTriggerEnter2D(Collider2D other) =>
      Optional(other.attachedRigidbody.GetComponent<ShipView>()).IfSome(view => {
        view.Facade.Damage(settings.damage);
        pool.Despawn(this);
        Dispose();
      });

    public void Dispose() => pool.Despawn(this);

    public void OnSpawned(IMemoryPool pool) {
      this.pool = pool;
      disposeLifeTimeCancelToken = new CancellationTokenSource();
      DisposeAfterLifeTime().Forget();
    }

    public void OnDespawned() {
      pool = null;
      transform.position = Vector3.zero;
      disposeLifeTimeCancelToken.Cancel();
    }

    private async UniTaskVoid DisposeAfterLifeTime() {
      await UniTask.Delay(TimeSpan.FromSeconds(settings.lifeTime)).WithCancellation(disposeLifeTimeCancelToken.Token);
      pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<Bullet> {
    }

    [Serializable]
    public class Settings {
      public float speed;
      public float lifeTime;
      public float damage;
    }
  }
}