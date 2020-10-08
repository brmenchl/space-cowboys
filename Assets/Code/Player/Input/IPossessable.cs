using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Player.Input
{
  public interface IPossessable
  {
    InputState InputState { get; }

    bool IsPossessed { get; }

    void Possess();

    void Depossess();

    void Shoot();
  }
}
