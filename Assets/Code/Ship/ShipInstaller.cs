using Code.Bullets;
using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using UnityEngine;
using Zenject;

namespace Code.Ship
{
    public class ShipInstaller : Installer<ShipInstaller>
    {
        private GameObject bulletPrefab;
        private Vector3 position;
        private Quaternion rotation;

        [Inject]
        private void Inject(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
        {
            this.bulletPrefab = bulletPrefab;
            this.position = position;
            this.rotation = rotation;
        }

        public override void InstallBindings()
        {
            Container.Bind<ShipFacade>().AsSingle();
            Container.Bind<ShipModel>().AsSingle().WithArguments(position, rotation).NonLazy();
            Container.Bind<SWRigidbody2D>().FromComponentOnRoot();

            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<MoveHandler>().AsSingle();
            Container.Bind<ShootHandler>().AsSingle().NonLazy();
            Container.Bind<HealthHandler>().AsSingle().NonLazy();

            Container.BindFactory<Bullet, Bullet.Factory>()
                .FromMonoPoolableMemoryPool(x =>
                    x
                        .WithInitialSize(10)
                        .FromComponentInNewPrefab(bulletPrefab)
                        .UnderTransformGroup("Bullets")
                );
        }
    }
}
