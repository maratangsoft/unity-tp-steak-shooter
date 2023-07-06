using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class TripleShot : IShootingType
	{
		private ObjectPool bulletPool;
		private Quaternion leftRotation;
		private Quaternion centerRotation;
		private Quaternion rightRotation;

		public TripleShot(ObjectPool bulletPool)
		{
			this.bulletPool = bulletPool;

			centerRotation = Quaternion.identity;
			leftRotation = Quaternion.Euler(0, -30, 0);
			rightRotation = Quaternion.Euler(0, 30, 0);
		}

		public void Shoot(Vector3 playerPosition)
		{
			bulletPool.GetNextObject(playerPosition + new Vector3(-1.5f, 0, 1.5f), leftRotation);
			bulletPool.GetNextObject(playerPosition + new Vector3(0, 0, 1.5f), centerRotation);
			bulletPool.GetNextObject(playerPosition + new Vector3(1.5f, 0, 1.5f), rightRotation);
		}
	}
}
