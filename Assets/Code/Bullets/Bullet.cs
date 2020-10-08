using System;
using System.Threading;
using Code.Ship;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Bullets
{
  public class Bullet : MonoBehaviour, IPoolable<IMemoryPool>
  {
    private CancellationTokenSource disposeLifeTimeCancelToken;
    private IMemoryPool pool;
    private Settings settings;

    private void Update()
    {
      transform.Translate(transform.up * (settings.speed * Time.deltaTime), Space.World);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
      var shipView = other.attachedRigidbody.GetComponent<ShipView>();

      if (shipView == null) return;

      shipView.Facade.Damage(settings.damage);
      pool.Despawn(this);
    }

    public void OnDespawned()
    {
      pool = null;
      transform.position = Vector3.zero;
      disposeLifeTimeCancelToken.Cancel();
    }

    public void OnSpawned(IMemoryPool pool)
    {
      this.pool = pool;
      disposeLifeTimeCancelToken = new CancellationTokenSource();
      DisposeAfterLifeTime().Forget();
    }

    [Inject]
    private void Inject(Settings settings)
    {
      this.settings = settings;
    }

    private async UniTaskVoid DisposeAfterLifeTime()
    {
      await UniTask.Delay(TimeSpan.FromSeconds(settings.lifeTime)).WithCancellation(disposeLifeTimeCancelToken.Token);
      pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<Bullet>
    {
    }

    [Serializable]
    public class Settings
    {
      public float speed;
      public float lifeTime;
      public float damage;
    }
  }
}
