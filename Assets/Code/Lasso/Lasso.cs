using System;
using System.Threading;
using Code.Option;
using Code.Ship;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Lasso {
  public class Lasso : MonoBehaviour {
    public enum LassoState {
      None,
      Firing,
      Reeling
    }

    private CancellationTokenSource cancellationTokenSource;
    private DistanceJoint2D distanceJoint2D;
    private LassoEnds lassoEnds;
    private Settings settings;
    private Transform tr;

    public LassoState state { get; private set; } = LassoState.None;

    private void Update() {
      switch (state) {
        case LassoState.Firing:
          tr.Translate(tr.up * (settings.speed * Time.deltaTime), Space.World);
          break;
        case LassoState.Reeling:
          distanceJoint2D.distance -= settings.speed * Time.deltaTime;
          break;
        case LassoState.None:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void OnDestroy() => Destroy(distanceJoint2D);

    private void OnTriggerEnter2D(Collider2D other) {
      if (isHookable(other)) {
        cancellationTokenSource.Cancel();
        distanceJoint2D.connectedBody = other.attachedRigidbody;
        distanceJoint2D.enableCollision = true;
        distanceJoint2D.enabled = true;
        state = LassoState.Reeling;
        var otherTr = other.transform;
        lassoEnds.end = otherTr;
        tr.parent = otherTr;
      }
    }

    [Inject]
    private void Inject(Settings settings, LassoEnds lassoEnds) {
      this.settings = settings;
      this.lassoEnds = lassoEnds;
      tr = transform;
      this.lassoEnds.start = tr.parent;
      this.lassoEnds.end = tr;
      Fire().Forget();
    }

    private async UniTaskVoid Fire() {
      cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
      distanceJoint2D = tr.parent.gameObject.AddComponent<DistanceJoint2D>();
      distanceJoint2D.enabled = false;
      state = LassoState.Firing;
      await UniTask.Delay(TimeSpan.FromSeconds(settings.lifeTime)).WithCancellation(cancellationTokenSource.Token);
      Destroy(gameObject);
    }

    private static bool isHookable(Component other) => other.gameObject.TryGetComponent<ShipView>().isSome;

    [Serializable]
    public class Settings {
      public float speed;
      public float lifeTime;
    }

    public class Factory : PlaceholderFactory<Lasso> {
    }
  }
}