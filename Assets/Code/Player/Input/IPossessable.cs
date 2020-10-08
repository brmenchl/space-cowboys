using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Player.Input
{
  public interface IPossessable
  {
    bool IsPossessed { get; }

    void Possess(Pawn pawn);

    void Depossess();

    void Shoot();
  }
}
