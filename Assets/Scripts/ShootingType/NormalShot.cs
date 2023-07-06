using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class NormalShot : IShootingType
	{
		private ObjectPool bulletPool;
		private Quaternion rotation;

		public NormalShot(ObjectPool bulletPool)
		{
			this.bulletPool = bulletPool;
			rotation = Quaternion.identity;
		}

		public void Shoot(Vector3 playerPosition)
		{
			bulletPool.GetNextObject(playerPosition + new Vector3(0, 0, 1.5f), rotation);
		}
	}
}

