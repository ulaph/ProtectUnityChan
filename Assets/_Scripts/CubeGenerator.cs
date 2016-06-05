using UnityEngine;
using System.Collections;

public class CubeGenerator : MonoBehaviour
{
    static readonly int GenerateInterval = 10;

    void Start()
    {
        StartCoroutine(setCubeRegularly());
    }

    IEnumerator setCubeRegularly()
    {
        var cube = Resources.Load("Prefabs/Cube/Cube") as GameObject;
        
        while(true)
        {
            var cubeNum = Random.Range(10, 20);
            for (int i = 0; i < cubeNum; i++)
            {
                Instantiate(cube, new Vector3(Random.Range(-48, 48), Random.Range(10, 15), Random.Range(-48, 48)), Quaternion.identity);
            }
            yield return new WaitForSeconds(GenerateInterval);
        }
    }
}
