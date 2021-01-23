using Code.Utilities.ScreenWrap;
using UnityEngine;

namespace Code.Cowboy
{
    public class CowboyModel
    {
        private readonly GameObject gameObject;

        public CowboyModel(SWRigidbody2D rigidbody2D, Vector3 position, Quaternion rotation)
        {
            rigidbody2D.SetPositionAndRotation(position, rotation);
            gameObject = rigidbody2D.gameObject;
        }

        public void Die()
        {
            Object.Destroy(gameObject);
        }
    }
}
