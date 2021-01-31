using UnityEngine;

namespace Code.Cowboy {
  public class CowboyModel {
    private readonly GameObject gameObject;

    public CowboyModel(Transform transform, Vector3 position, Quaternion rotation) {
      transform.position = position;
      transform.rotation = rotation;
      gameObject = transform.gameObject;
    }

    public void Destroy() => Object.Destroy(gameObject);
  }
}