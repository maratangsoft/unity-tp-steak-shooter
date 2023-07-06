

namespace Maratangsoft.SteakShooter
{
	public class StageDetails
	{
		private float spawnInterval;
		private int numOfAnimalTypes;

		public float SpawnInterval { get => spawnInterval; set => spawnInterval = value; }
		public int NumOfAnimalTypes { get => numOfAnimalTypes; set => numOfAnimalTypes = value; }

		public StageDetails(float spawnInterval, int numOfAnimalTypes)
		{
			this.spawnInterval = spawnInterval;
			this.numOfAnimalTypes = numOfAnimalTypes;
		}
	}
}