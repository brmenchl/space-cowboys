using Code.Utilities.ScreenWrap;
using UnityEngine;

namespace Code.Ship
{
  public class ShipModel
  {
    private readonly GameObject gameObject;

    public ShipModel(ScreenWrappingRigidbody2D rigidbody2D, Vector3 position, Quaternion rotation)
    {
      rigidbody2D.SetPositionAndRotation(position, rotation);
      gameObject = rigidbody2D.gameObject;
    }

    public void Die()
    {
      Object.Destroy(gameObject);
    }
  }
}
