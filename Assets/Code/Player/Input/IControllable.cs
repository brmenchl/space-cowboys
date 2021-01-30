using UnityEngine;

namespace Code.Player.Input {
  public interface IControllable {
    Transform transform { get; }
    void Thrust(float amount);
    void Turn(float amount);

    void Shoot();
  }
}