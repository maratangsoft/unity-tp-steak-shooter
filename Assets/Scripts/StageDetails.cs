

namespace Maratangsoft.SteakShooter
{
	public class StageDetails
	{
		private float spawnInterval;
		private int numOfAnimalTypesToAdd;

		public float SpawnInterval { get => spawnInterval; set => spawnInterval = value; }
		public int NumOfAnimalTypesToAdd { get => numOfAnimalTypesToAdd; set => numOfAnimalTypesToAdd = value; }

		public StageDetails(float spawnInterval, int numOfAnimalTypesToAdd)
		{
			this.spawnInterval = spawnInterval;
			this.numOfAnimalTypesToAdd = numOfAnimalTypesToAdd;
		}
	}
}