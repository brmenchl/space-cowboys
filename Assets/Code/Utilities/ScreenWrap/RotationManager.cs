namespace Code.Utilities.ScreenWrap {
  using UnityEngine;

  public class RotationManager {
    private Rigidbody2D rb;
    private GameObject rotator;

    public Transform Transform => rb.transform;

    public void CreateRotator(Transform parent, float inertia) {
      rotator = new GameObject("Rotator");
      rotator.transform.SetParent(parent);
      rb = rotator.AddComponent<Rigidbody2D>();
      rb.gravityScale = 0;
      rb.inertia = inertia;
    }

    public void AddTorque(float torque) => rb.AddTorque(torque);
  }
}