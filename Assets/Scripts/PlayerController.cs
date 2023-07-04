using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 25.0f;
    private float xRange = 17f;

    private ObjectPool bulletPool;

	private void Start()
	{
        bulletPool = GameObject.Find("Bullet Object Pool").GetComponent<ObjectPool>();
	}

	void Update()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            bulletPool.GetObject(transform.position);
        }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }
}
