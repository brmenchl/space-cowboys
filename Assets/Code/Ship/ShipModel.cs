using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Ship {
  public class ShipModel {
    public readonly Transform transform;
    private readonly AsyncReactiveProperty<float> health;
    private readonly CancellationTokenSource token = new CancellationTokenSource();

    public ShipModel(Transform transform, Vector3 position, Quaternion rotation, Settings settings) {
      this.transform = transform;
      transform.position = position;
      transform.rotation = rotation;
      health = new AsyncReactiveProperty<float>(settings.startingHealth);
      health.Subscribe(HandleChangeHealth, token.Token);
      healthPercentStream = health.Select(h => h / settings.startingHealth).ToReadOnlyAsyncReactiveProperty(token.Token);
    }

    public event Action OnDestroyed;

    public ReadOnlyAsyncReactiveProperty<float> healthPercentStream;

    public void Damage(float damage) {
      health.Value -= damage;
    }

    private void HandleChangeHealth(float value) {
      if (health.Value <= 0f) Destroy();
    }

    public void Destroy() {
      OnDestroyed?.Invoke();
      token.Cancel();
      Object.Destroy(transform.gameObject);
    }

    [Serializable]
    public class Settings {
      public float startingHealth;
    }
  }
}