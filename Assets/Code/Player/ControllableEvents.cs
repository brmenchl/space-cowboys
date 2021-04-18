using System;
using Code.Input;
using UnityEngine;

namespace Code.Player {
  public interface IPlayerDamageable {
    event Action<float> OnDamaged;
  }

  public interface IEjectable {
    public void Eject();
    event Action<Vector2> OnEjected;
  }

  public interface IBoardable {
    event Action<IControllable> OnBoarded;
  }
}