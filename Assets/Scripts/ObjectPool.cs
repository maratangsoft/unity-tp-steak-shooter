using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
	[SerializeField] private GameObject[] prefabsToPool;
	[SerializeField] private int initCount = 10;

	private List<GameObject> pooledObjects;

	private void Start()
	{
		pooledObjects = new List<GameObject>(initCount);
		Initialize();
	}

	private void Initialize() 
	{
		for (int i = 0; i < initCount; i++) 
		{
			int randomIndex = Random.Range(0, prefabsToPool.Length);
			GameObject newObj = CreateNewObject(randomIndex);
			pooledObjects.Add(newObj);
		}
	}

	private GameObject CreateNewObject(int index)
	{
		GameObject newObj = Instantiate(prefabsToPool[index]);
		newObj.SetActive(false);
		newObj.transform.SetParent(transform);
		return newObj;
	}

	public GameObject GetObject(Vector3 position)
	{
		GameObject obj = null;
		while (true)
		{
			int index = Random.Range(0, pooledObjects.Count);
			if (!pooledObjects[index].activeInHierarchy)
			{
				obj = pooledObjects[index];
				break;
			}
		}
		if (obj == null)
		{
			int randomIndex = Random.Range(0, prefabsToPool.Length);
			obj = CreateNewObject(randomIndex);
		}
		obj.transform.position = position;
		obj.gameObject.SetActive(true);

		return obj;
	}

	public void RetrieveObject(GameObject obj)
	{
		obj.gameObject.SetActive(false);
	}

	public void RetrieveAll()
	{
		foreach (GameObject obj in pooledObjects)
		{
			obj.gameObject.SetActive(false);
		}
	}
}
