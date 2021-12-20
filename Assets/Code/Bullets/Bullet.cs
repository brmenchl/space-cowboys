using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using External.Option;
using UnityEngine;
using Zenject;

namespace Code.Bullets {
  public class Bullet : MonoBehaviour, IPoolable<Vector3, Quaternion, IMemoryPool>, IDisposable {
    private CancellationTokenSource disposeLifeTimeCancelToken;
    private IMemoryPool pool;
    [Inject] private Settings settings;
    private Transform t;

    private void Awake() {
      t = transform;
    }

    private void Update() => t.Translate(t.up * (settings.speed * Time.deltaTime), Space.World);

    private void OnTriggerEnter2D(Collider2D other) {
      other.gameObject.TryGetComponent<DamageableView>().MatchSome(damageableView => {
        damageableView.Damage(settings.damage);
        Dispose();
      });
    }

    public void Dispose() => pool.Despawn(this);

    public void OnSpawned(Vector3 pos, Quaternion rot, IMemoryPool pool) {
      this.pool = pool;
      t.SetPositionAndRotation(pos, rot);
      disposeLifeTimeCancelToken = new CancellationTokenSource();
      DisposeAfterLifeTime().Forget();
    }

    public void OnDespawned() {
      pool = null;
      t.position = Vector3.zero;
      disposeLifeTimeCancelToken.Cancel();
    }

    private async UniTaskVoid DisposeAfterLifeTime() {
      await UniTask.Delay(TimeSpan.FromSeconds(settings.lifeTime), cancellationToken: disposeLifeTimeCancelToken.Token);
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