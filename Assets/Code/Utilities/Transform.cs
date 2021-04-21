using UnityEngine;

namespace Code.Utilities {
  public static class TransformHelpers {
    public static Quaternion RandomRotation() => Quaternion.AngleAxis(Random.Range(-180, 180), Vector3.forward);

    public static Vector2 RandomPositionNear(Vector2 position, float distance) =>
      position + (Random.insideUnitCircle.normalized * distance);
  }
}