using System;
using UnityEngine;
using Zenject;

namespace Code.Lasso {
  public class Lasso : MonoBehaviour {
    private DistanceJoint2D distanceJoint2D;
    private bool isReeling;
    private LassoEnds lassoEnds;
    private LassoTip lassoTip;
    private LassoTip.Factory lassoTipFactory;
    private Settings settings;

    public void Start() => Fire();

    public void FixedUpdate() {
      if (isReeling) distanceJoint2D.distance -= settings.reelForce * Time.fixedDeltaTime;
    }

    [Inject]
    public void Inject(
      Settings settings,
      LassoTip.Factory lassoTipFactory,
      LassoEnds lassoEnds,
      DistanceJoint2D distanceJoint2D
    ) {
      this.settings = settings;
      this.lassoTipFactory = lassoTipFactory;
      this.lassoEnds = lassoEnds;
      this.distanceJoint2D = distanceJoint2D;
      lassoEnds.start = transform;
    }

    private void Fire() {
      lassoTip = lassoTipFactory.Create(transform.position, Quaternion.identity);
      lassoTip.OnHooked += Hook;
      lassoTip.OnHookTimeout += Dispose;
      lassoEnds.end = lassoTip.transform;
    }

    private void Hook(Rigidbody2D reelSubject) {
      Unsubscribe();
      Destroy(lassoTip.gameObject);
      var anchorPoint = new GameObject();
      var anchorRB = anchorPoint.AddComponent<Rigidbody2D>();
      anchorRB.gravityScale = 0;
      anchorRB.bodyType = RigidbodyType2D.Kinematic;
      anchorPoint.transform.SetParent(reelSubject.transform);
      lassoEnds.end = anchorPoint.transform;
      distanceJoint2D.connectedBody = anchorRB;
      isReeling = true;
    }

    private void Dispose() {
      Unsubscribe();
      Destroy(this);
    }

    private void Unsubscribe() {
      lassoTip.OnHooked -= Hook;
      lassoTip.OnHookTimeout -= Dispose;
    }

    [Serializable]
    public class Settings {
      public float reelForce;
    }

    // public class Factory : PlaceholderFactory<Transform, Lasso> {
    // }
  }
}