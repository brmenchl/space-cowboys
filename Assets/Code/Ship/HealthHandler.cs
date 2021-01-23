using System;

namespace Code.Ship
{
    public class HealthHandler
    {
        private readonly ShipModel model;

        private float health;

        public HealthHandler(ShipModel model, Settings settings)
        {
            this.model = model;
            health = settings.startingHealth;
        }

        public void Damage(float damage)
        {
            health -= damage;

            if (health <= 0f) model.Die();
        }

        [Serializable]
        public class Settings
        {
            public float startingHealth;
        }
    }
}
