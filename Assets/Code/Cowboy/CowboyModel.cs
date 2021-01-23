namespace Code.Cowboy {
  using UnityEngine;

  using Utilities.ScreenWrap;

  public class CowboyModel {
    private readonly GameObject gameObject;

    public CowboyModel(SWRigidbody2D rigidbody2D, Vector3 position, Quaternion rotation) {
      rigidbody2D.SetPositionAndRotation(position, rotation);
      gameObject = rigidbody2D.gameObject;
    }

    public void Die() => Object.Destroy(gameObject);
  }
}