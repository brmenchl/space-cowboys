using System;
using Code.Ship;

namespace Code.Cowboy
{
  public class HealthHandler
  {
    private readonly CowboyModel model;

    private float health;

    public HealthHandler(CowboyModel model)
    {
      this.model = model;
      health = 50; //settings.startingHealth;
    }

    public void Damage(float damage)
    {
      health -= damage;

      if (health <= 0f) model.Die();
    }

    // [Serializable]
    // public class Settings
    // {
    //   public float startingHealth;
    // }
  }
}
