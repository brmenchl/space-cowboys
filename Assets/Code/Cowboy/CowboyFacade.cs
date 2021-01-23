using Code.Player;
using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Cowboy
{
    public class CowboyFacade : IPossessable
    {
        private readonly HealthHandler healthHandler;
        private readonly InputHandler inputHandler;
        private readonly ShootHandler shootHandler;

        public CowboyFacade(
            InputHandler inputHandler,
            ShootHandler shootHandler,
            HealthHandler healthHandler
        )
        {
            this.shootHandler = shootHandler;
            this.healthHandler = healthHandler;
            this.inputHandler = inputHandler;
        }

        public bool IsPossessed => inputHandler.IsPossessed;

        public void Possess(Pawn pawn)
        {
            inputHandler.Possess(pawn);
        }

        public void Depossess()
        {
            inputHandler.Depossess();
        }

        public void Shoot()
        {
            shootHandler.Shoot();
        }

        public void Damage(float damage)
        {
            healthHandler.Damage(damage);
        }

        public class Factory : PlaceholderFactory<Vector3, Quaternion, CowboyFacade>
        {
        }
    }
}
