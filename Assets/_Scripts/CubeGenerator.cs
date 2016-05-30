using UnityEngine;
using System.Collections;

public class CubeGenerator : MonoBehaviour
{

    GameObject cube;

    void Start()
    {
        cube = Resources.Load("Prefabs/Cube/Cube") as GameObject;
        StartCoroutine(setCubeRegularly());
    }

    IEnumerator setCubeRegularly()
    {
        var cubeNum = Random.Range(10, 20);
        for (int i = 0; i < cubeNum; i++)
        {
            Instantiate(cube, new Vector3(Random.Range(-48, 48), Random.Range(10, 15), Random.Range(-48, 48)), Quaternion.identity);
        }
        yield return new WaitForSeconds(10);
        StartCoroutine(setCubeRegularly());
    }
}
