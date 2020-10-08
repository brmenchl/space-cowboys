using Code.Utilities.ScreenWrap;
using UnityEngine;

namespace Code.Ship
{
  public class ShipModel
  {
    private readonly GameObject gameObject;

    public ShipModel(ScreenWrappingRigidbody2D rigidbody2D)
    {
      gameObject = rigidbody2D.gameObject;
    }

    public void Die()
    {
      Object.Destroy(gameObject);
    }
  }
}
