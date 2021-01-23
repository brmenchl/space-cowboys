namespace Code.Ship {
  using UnityEngine;

  using Utilities.ScreenWrap;

  public class ShipModel {
    private readonly GameObject gameObject;

    public ShipModel(SWRigidbody2D rigidbody2D, Vector3 position, Quaternion rotation) {
      rigidbody2D.SetPositionAndRotation(position, rotation);
      gameObject = rigidbody2D.gameObject;
    }

    public void Die() => Object.Destroy(gameObject);
  }
}