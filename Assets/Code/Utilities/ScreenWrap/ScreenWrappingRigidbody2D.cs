using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Utilities.ScreenWrap
{
  public class ScreenWrappingRigidbody2D : MonoBehaviour
  {
    [SerializeField] private GameObject clonePrefab;
    private Vector2 screenBounds;
    private readonly Dictionary<ClonePlacement, GameObject> clones = new Dictionary<ClonePlacement, GameObject>();
    private RotationManager rotationManager;
    private Rigidbody2D rb;

    #region MonoBehaviour API

    private void Awake()
    {
      screenBounds = Camera.main.ScreenBounds();

      rb = GetComponent<Rigidbody2D>();

      rotationManager = new RotationManager();
      rotationManager.CreateRotator(transform);

      CreateClones();
    }

    private void Update()
    {
      if (!Application.IsPlaying(gameObject)) return;
      UpdateClonesArrangement();
      TransformClones();
    }

    #endregion

    #region Facade

    public Vector3 Up => rotationManager.Transform.up;

    public Quaternion Rotation => rotationManager.Transform.rotation;

    public void AddForce(Vector2 force) => rb.AddForce(force);

    public void AddTorque(float torque) => rotationManager.AddTorque(torque);

    public void SetPosition(Vector3 position) => rb.transform.position = position;
    #endregion

    #region Clone Placement

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
      var basePosition = centerTransform.position;
      var clonePosition = basePosition;
      switch (clone)
      {
        case ClonePlacement.TopLeft:
          clonePosition.x = basePosition.x - screenBounds.x;
          clonePosition.y = basePosition.y + screenBounds.y;
          break;

        case ClonePlacement.Left:
          clonePosition.x = basePosition.x - screenBounds.x;
          break;

        case ClonePlacement.BottomLeft:
          clonePosition.x = basePosition.x - screenBounds.x;
          clonePosition.y = basePosition.y - screenBounds.y;
          break;

        case ClonePlacement.Top:
          clonePosition.y = basePosition.y + screenBounds.y;
          break;

        case ClonePlacement.Bottom:
          clonePosition.y = basePosition.y - screenBounds.y;
          break;

        case ClonePlacement.TopRight:
          clonePosition.x = basePosition.x + screenBounds.x;
          clonePosition.y = basePosition.y + screenBounds.y;
          break;

        case ClonePlacement.Right:
          clonePosition.x = basePosition.x + screenBounds.x;
          break;

        case ClonePlacement.BottomRight:
          clonePosition.x = basePosition.x + screenBounds.x;
          clonePosition.y = basePosition.y - screenBounds.y;
          break;
      }

      cloneTransform.position = clonePosition;
      cloneTransform.rotation = rotationManager.Transform.rotation;
    }

    private bool IsInCenter(Transform t)
    {
      var position = t.position;
      return position.x <= screenBounds.x / 2 &&
             position.x > -(screenBounds.x / 2) &&
             position.y <= screenBounds.y / 2 &&
             position.y > -(screenBounds.y / 2);
    }

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
}
