using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 30f;
    private float bottomBound = -15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 이 오브젝트의 z좌표가 topBound를 넘으면
        if (transform.position.z > topBound)
        {
            // 이 오브젝트를 메모리에서 제거해라
            Destroy(gameObject);
        }
        // 이 오브젝트의 z좌표가 bottomBound를 넘으면
        else if (transform.position.z < bottomBound)
        {
            // 게임오버 문구 표시
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }

    }
}
