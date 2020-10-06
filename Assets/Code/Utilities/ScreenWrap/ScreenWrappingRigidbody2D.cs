using UnityEngine;
using System;
using System.Collections.Generic;

public class ScreenWrappingRigidbody2D : MonoBehaviour
{
  [SerializeField] private GameObject clonePrefab;
  private Vector2 screenBounds;
  private Dictionary<ClonePlacement, GameObject> clones = new Dictionary<ClonePlacement, GameObject>();
  private RotationManager rotationManager;
  private Rigidbody2D rb;

  #region MonoBehaviour API
  private void Start()
  {
    screenBounds = Camera.main.ScreenBounds();

    rb = GetComponent<Rigidbody2D>();

    rotationManager = new RotationManager();
    rotationManager.CreateRotator(transform);

    CreateClones();
  }

  private void Update()
  {
    if (Application.IsPlaying(gameObject))
    {
      UpdateClonesArrangement();
      TransformClones();
    }
  }
  #endregion

  #region "RigidBody2D" API
  public Vector3 Up { get => rotationManager.Transform.up; }

  public Quaternion Rotation { get => rotationManager.Transform.rotation; }

  public void AddForce(Vector2 force)
  {
    rb.AddForce(force);
  }

  public void AddTorque(float torque)
  {
    rotationManager.AddTorque(torque);
  }
  #endregion

  #region Clone Placement
  private void CreateCenter()
  {
    GameObject.Instantiate(clonePrefab, transform);
  }

  private void CreateClones()
  {
    foreach (ClonePlacement clonePlacement in Enum.GetValues(typeof(ClonePlacement)))
    {
      var clone = GameObject.Instantiate(clonePrefab, transform);
      TransformClone(transform, clone.transform, clonePlacement);
      clones[clonePlacement] = clone;
    }
  }

  private void UpdateClonesArrangement()
  {
    foreach (ClonePlacement clonePlacement in Enum.GetValues(typeof(ClonePlacement)))
    {
      var clone = clones[clonePlacement];

      if (clonePlacement != ClonePlacement.Center && IsInCenter(clone.transform))
      {
        var prevCenterObject = clones[ClonePlacement.Center];
        clones[ClonePlacement.Center] = clone;
        clones[clonePlacement] = prevCenterObject;
        transform.position = clones[ClonePlacement.Center].transform.position;
        break;
      }
    }
  }

  private void TransformClones()
  {
    foreach (var entry in clones)
    {
      TransformClone(transform, entry.Value.transform, entry.Key);
    }
  }

  private void TransformClone(Transform centerTransform, Transform cloneTransform, ClonePlacement clone)
  {
    var clonePosition = centerTransform.position;
    switch (clone)
    {
      case ClonePlacement.TopLeft:
        clonePosition.x = centerTransform.position.x - screenBounds.x;
        clonePosition.y = centerTransform.position.y + screenBounds.y;
        break;

      case ClonePlacement.Left:
        clonePosition.x = centerTransform.position.x - screenBounds.x;
        break;

      case ClonePlacement.BottomLeft:
        clonePosition.x = centerTransform.position.x - screenBounds.x;
        clonePosition.y = centerTransform.position.y - screenBounds.y;
        break;

      case ClonePlacement.Top:
        clonePosition.y = centerTransform.position.y + screenBounds.y;
        break;

      case ClonePlacement.Bottom:
        clonePosition.y = centerTransform.position.y - screenBounds.y;
        break;

      case ClonePlacement.TopRight:
        clonePosition.x = centerTransform.position.x + screenBounds.x;
        clonePosition.y = centerTransform.position.y + screenBounds.y;
        break;

      case ClonePlacement.Right:
        clonePosition.x = centerTransform.position.x + screenBounds.x;
        break;

      case ClonePlacement.BottomRight:
        clonePosition.x = centerTransform.position.x + screenBounds.x;
        clonePosition.y = centerTransform.position.y - screenBounds.y;
        break;
    }

    cloneTransform.position = clonePosition;
    cloneTransform.rotation = rotationManager.Transform.rotation;
  }

  private bool IsInCenter(Transform transform) => (
    transform.position.x <= (screenBounds.x / 2) &&
    transform.position.x > -(screenBounds.x / 2) &&
    transform.position.y <= (screenBounds.y / 2) &&
    transform.position.y > -(screenBounds.y / 2)
  );

  private enum ClonePlacement
  {
    Center,
    TopLeft,
    Top,
    TopRight,
    Right,
    BottomRight,
    Bottom,
    BottomLeft,
    Left,
  }
  #endregion
}
