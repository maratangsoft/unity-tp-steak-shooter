using System;
using System.Collections.Generic;
using UnityEngine;

namespace Maratangsoft.SteakShooter
{
	public class ObjectPool : MonoBehaviour
	{
		[SerializeField] private GameObject[] prefabsToPool;

		private List<GameObject> pooledObjects;
		private int totalNumOfPrefabTypes = 0;
		private bool isInitialized = false;

		public void ExpandPool(int numOfPrefabTypes, int poolingCount)
		{
			if (totalNumOfPrefabTypes + numOfPrefabTypes > prefabsToPool.Length)
			{
				throw new IndexOutOfRangeException(
					"parameter numOfObjectKind should not be larger than the number of prefabs you attached on Prefabs To Pool array in inspector."
				);
			}

			if (pooledObjects == null) 
			{
				pooledObjects = new List<GameObject>(poolingCount);
			}

			for (int i = 0; i < poolingCount; i++)
			{
				int randomIndex = UnityEngine.Random.Range(totalNumOfPrefabTypes, numOfPrefabTypes);
				GameObject newObj = CreateNewObject(randomIndex);
				pooledObjects.Add(newObj);
			}
			totalNumOfPrefabTypes += numOfPrefabTypes;
			isInitialized = true;
		}

		private GameObject CreateNewObject(int index)
		{
			GameObject newObj = Instantiate(prefabsToPool[index]);
			newObj.SetActive(false);
			newObj.transform.SetParent(transform);
			return newObj;
		}

		public GameObject GetObject(Vector3 position, Quaternion rotation)
		{
			if (!isInitialized) throw new Exception("You should call ExpandPool() method first.");

			GameObject obj = null;
			while (true)
			{
				int index = UnityEngine.Random.Range(0, pooledObjects.Count);
				if (!pooledObjects[index].activeInHierarchy)
				{
					obj = pooledObjects[index];
					break;
				}
			}
			if (obj == null)
			{
				int randomIndex = UnityEngine.Random.Range(0, prefabsToPool.Length);
				obj = CreateNewObject(randomIndex);
			}
			obj.transform.SetPositionAndRotation(position, rotation);
			obj.SetActive(true);

			return obj;
		}

		public void ReturnObject(GameObject obj)
		{
			obj.SetActive(false);
		}

		public void ReturnAll()
		{
			foreach (GameObject obj in pooledObjects)
			{
				obj.SetActive(false);
			}
		}
	}
}