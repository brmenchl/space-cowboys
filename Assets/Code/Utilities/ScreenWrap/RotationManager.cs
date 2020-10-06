using UnityEngine;

namespace Code.Utilities.ScreenWrap
{
  public class RotationManager
  {
    private GameObject rotator;
    private Rigidbody2D rb;

    public void CreateRotator(Transform parent)
    {
      rotator = new GameObject("Rotator");
      rotator.transform.SetParent(parent);
      rb = rotator.AddComponent<Rigidbody2D>();
      rb.gravityScale = 0;
    }

    public void AddTorque(float torque)
    {
      rb.AddTorque(torque);
    }

    public Transform Transform => rb.transform;
  }
}
