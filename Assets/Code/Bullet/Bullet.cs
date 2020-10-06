using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Bullet
{
  public class Bullet : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
  {
    private IMemoryPool pool;
    [SerializeField] private float lifetime = 1;
    [SerializeField] private float speed = 10;

    private void Update()
    {
      transform.Translate(transform.up * (speed * Time.deltaTime), Space.World);
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
      await UniTask.Delay(TimeSpan.FromSeconds(lifetime));
      Dispose();
    }

    public class Factory : PlaceholderFactory<Bullet>
    {
    }
  }
}
