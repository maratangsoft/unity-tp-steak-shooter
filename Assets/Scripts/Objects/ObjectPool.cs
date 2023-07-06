using System;
using System.Collections.Generic;
using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class ObjectPool : MonoBehaviour
	{
		[SerializeField] private GameObject[] prefabsToPool;

		private List<GameObject> pooledObjects;
		private int numOfPrefabTypes;
		private bool isInitialized = false;
		private int currentIndex = -1;

		public void SetPool(int numOfPrefabTypes, int poolingCount)
		{
			if (numOfPrefabTypes > prefabsToPool.Length)
			{
				throw new IndexOutOfRangeException(
					"parameter numOfObjectKind should not be larger than the number of prefabs you attached on Prefabs To Pool array in inspector."
				);
			}
			this.numOfPrefabTypes = numOfPrefabTypes;

			if (pooledObjects == null)
			{
				pooledObjects = new List<GameObject>(poolingCount);
			}

			for (int i = 0; i < poolingCount; i++)
			{
				GameObject newObj = CreateRandomObject();
				pooledObjects.Add(newObj);
			}
			isInitialized = true;
		}

		private GameObject CreateRandomObject()
		{
			int randomIndex = UnityEngine.Random.Range(0, numOfPrefabTypes);
			GameObject newObj = Instantiate(prefabsToPool[randomIndex]);
			newObj.SetActive(false);
			newObj.transform.SetParent(transform);
			return newObj;
		}

		public GameObject GetNextObject(Vector3 position, Quaternion rotation)
		{
			if (!isInitialized) throw new Exception("You should call SetPool() method first.");

			currentIndex++;
			if (currentIndex >= pooledObjects.Count) currentIndex = 0;

			GameObject obj;
			if (!pooledObjects[currentIndex].activeInHierarchy)
			{
				obj = pooledObjects[currentIndex];
			}
			else
			{
				obj = CreateRandomObject();
				pooledObjects.Add(obj);
			}
			obj.transform.SetPositionAndRotation(position, rotation);
			obj.SetActive(true);
			obj.transform.SetParent(null);

			return obj;
		}

		public GameObject GetRandomObject(Vector3 position, Quaternion rotation)
		{
			if (!isInitialized) throw new Exception("You should call SetPool() method first.");

			GameObject obj;
			while (true)
			{
				int index = UnityEngine.Random.Range(0, pooledObjects.Count);
				if (!pooledObjects[index].activeInHierarchy)
				{
					obj = pooledObjects[index];
					currentIndex = index;
					break;
				}
			}
			if (obj == null)
			{
				obj = CreateRandomObject();
				pooledObjects.Add(obj);
				currentIndex = pooledObjects.Count - 1;
			}
			
			obj.transform.SetPositionAndRotation(position, rotation);
			obj.SetActive(true);
			obj.transform.SetParent(null);

			return obj;
		}

		public void ReturnObject(GameObject obj)
		{
			obj.SetActive(false);
			obj.transform.SetParent(transform);
		}

		public void DestroyAll()
		{
			foreach (GameObject obj in pooledObjects)
			{
				Destroy(obj);
			}
			currentIndex = -1;
			pooledObjects.Clear();
			isInitialized = false;
		}
	}
}