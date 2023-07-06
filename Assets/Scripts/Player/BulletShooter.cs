using System.Collections;
using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class BulletShooter : MonoBehaviour
	{
		[SerializeField] private ObjectPool bulletPool;
		[SerializeField] private PowerUp shootingType;

		// configuration
		private GameManager gameManager;
		private int numOfBulletTypes = 1;
		private int initPoolingCount = 12;

		// shooting type
		private NormalShot singleShot;
		private TripleShot tripleShot;

		private Coroutine powerUpCoroutine;

		public ObjectPool BulletPool { get => bulletPool; }

		void Start()
		{
			gameManager = GameManager.Instance;
			bulletPool.SetPool(numOfBulletTypes, initPoolingCount);
			singleShot = new NormalShot(bulletPool);
			tripleShot = new TripleShot(bulletPool);
		}

		public void Shoot()
		{
			switch (shootingType)
			{
				case SteakShooter.PowerUp.NORMAL:
					singleShot.Shoot(transform.position);
					break;

				case SteakShooter.PowerUp.TRIPLE_SHOT:
					tripleShot.Shoot(transform.position);
					break;
			}
		}

		public void PowerUp(PowerUp TypeToChange)
		{
			// turn off present power up effect to use new one
			TurnOffPowerUp();

			float powerUpDuration = gameManager.PowerUpDuration;

			switch (TypeToChange)
			{
				case SteakShooter.PowerUp.TRIPLE_SHOT:
					shootingType = SteakShooter.PowerUp.TRIPLE_SHOT;
					powerUpCoroutine = StartCoroutine(TripleShotDuration(powerUpDuration));
					break;
			}
		}

		private IEnumerator TripleShotDuration(float duration)
		{
			yield return new WaitForSeconds(duration);
			TurnOffPowerUp();
		}

		public void TurnOffPowerUp()
		{
			shootingType = SteakShooter.PowerUp.NORMAL;
			if (powerUpCoroutine != null)
			{
				StopCoroutine(powerUpCoroutine);
				powerUpCoroutine = null;
			}
		}
	}
}
