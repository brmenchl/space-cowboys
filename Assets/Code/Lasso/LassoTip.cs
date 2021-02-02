using System;
using Code.Ship;
using Cysharp.Threading.Tasks;
using LanguageExt;
using UnityEngine;
using Zenject;

namespace Code.Lasso {
  using static Prelude;

  public class LassoTip : MonoBehaviour {
    private Settings settings;

    private void Update() => transform.Translate(transform.up * (settings.speed * Time.deltaTime), Space.World);

    private void OnTriggerEnter2D(Collider2D other) =>
      Optional(other.gameObject.GetComponent<ShipView>()).IfSome(view => {
        OnHooked?.Invoke(other.attachedRigidbody);
      });

    public event Action<Rigidbody2D> OnHooked;
    public event Action OnHookTimeout;

    [Inject]
    public void Inject(Vector2 pos, Quaternion rot, Settings settings) {
      this.settings = settings;
      transform.SetPositionAndRotation(pos, rot);
      LifetimeTimeout().Forget();
    }

    private async UniTaskVoid LifetimeTimeout() {
      await UniTask.Delay(TimeSpan.FromSeconds(settings.lifeTime))
        .WithCancellation(this.GetCancellationTokenOnDestroy());
      OnHookTimeout?.Invoke();
    }

    public class Factory : PlaceholderFactory<Vector2, Quaternion, LassoTip> {
    }

    [Serializable]
    public class Settings {
      public float speed;
      public float lifeTime;
    }
  }
}