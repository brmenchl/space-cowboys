using UnityEngine;
using Zenject;

public class DamageableView : MonoBehaviour {
  private IDamageable damageable;

  [Inject]
  public void Inject(IDamageable damageable) {
    this.damageable = damageable;
  }

  public void Damage(float damage) => damageable.Damage(damage);
}