using Code.Ship;
using External.Option;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyView : MonoBehaviour {
    private CowboyModel model;

    public void OnCollisionEnter2D(Collision2D other) =>
      other.gameObject
        .TryGetComponent<ShipView>()
        .MatchSome(controllable => model.TryBoard(controllable.Facade));

    [Inject]
    private void Inject(CowboyModel model) => this.model = model;
  }
}