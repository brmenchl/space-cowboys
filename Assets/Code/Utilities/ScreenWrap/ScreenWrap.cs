namespace Code.Utilities.ScreenWrap {
  using UnityEngine;

  public class ScreenWrap : MonoBehaviour {
    private Vector3 screenBottomLeft;
    private Vector3 screenTopRight;

    public void Start() {
      if (Camera.main is null) return;
      screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
      screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    public void Update() {
      var newPosition = transform.position;
      if (transform.position.x > screenTopRight.x) newPosition.x = screenBottomLeft.x;

      if (transform.position.x < screenBottomLeft.x) newPosition.x = screenTopRight.x;

      if (transform.position.y > screenTopRight.y) newPosition.y = screenBottomLeft.y;

      if (transform.position.y < screenBottomLeft.y) newPosition.y = screenTopRight.y;

      transform.position = newPosition;
    }
  }
}