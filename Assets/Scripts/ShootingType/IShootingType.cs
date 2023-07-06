using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public interface IShootingType
	{
		void Shoot(Vector3 playerPosition);
	}
}