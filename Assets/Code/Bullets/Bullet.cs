using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Bullets
{
  public class Bullet : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
  {
    private IMemoryPool pool;

    private Settings settings;

    [Inject]
    private void Inject(Settings settings)
    {
      this.settings = settings;
    }

    private void Update()
    {
      transform.Translate(transform.up * (settings.speed * Time.deltaTime), Space.World);
    }

    public void Dispose()
    {
      pool.Despawn(this);
    }

    public void OnDespawned()
    {
      pool = null;
      transform.position = Vector3.zero;
    }

    public async void OnSpawned(IMemoryPool pool)
    {
      this.pool = pool;
      await UniTask.Delay(TimeSpan.FromSeconds(settings.lifeTime));
      Dispose();
    }

    public class Factory : PlaceholderFactory<Bullet>
    {
    }

    [Serializable]
    public class Settings
    {
      public float speed;
      public float lifeTime;
    }
  }
}
