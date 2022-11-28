using System;
using UnityEngine;

namespace CodeEcs.Components {
  public struct DestroyAfterTime {
    public DateTime destroyTime;
    internal GameObject gameObject;

    public void SetLifetime(GameObject gameObject, float lifeTime) {
      this.gameObject = gameObject;
      var now = DateTime.UtcNow;
      destroyTime = now.AddSeconds(lifeTime);
    }
  }
}