using UnityEngine;
using System.Collections;

public sealed class ObjectHolder : MonoBehaviour
{
    static ObjectHolder _singleInstance;

    public Material GreenCubeMat;
    public Material RedCubeMat;
    public Material BlueCubeMat;
    GameObject ZomBunny;
    GameObject ZomBear;

    public static ObjectHolder Instance
    {
        get
        {
            if (_singleInstance == null)
            {
                GameObject status = new GameObject("ObjectHolder");
                _singleInstance = status.AddComponent<ObjectHolder>();
            }
            return _singleInstance;
        }
    }

    void Start()
    {   
        var materials = Resources.Load("Prefabs/Materials") as GameObject;
//        materials = Instantiate(materials) as GameObject;

        var mat = materials.GetComponent<Materials>();

        GreenCubeMat = mat.GreenCubeMat;
        RedCubeMat = mat.RedCubeMat;
        BlueCubeMat = mat.BlueCubeMat;

        ZomBunny = Resources.Load("Prefabs/Enemy/ZomBunny") as GameObject;
        ZomBear = Resources.Load("Prefabs/Enemy/ZomBear") as GameObject;
    }

    public void EnemyInstance(Vector3 pos, int typeNum)
    {
        switch (typeNum)
        {
            case 0:
                Instantiate(ZomBunny, pos, Quaternion.identity);
                break;
            case 1:
                Instantiate(ZomBear, pos, Quaternion.identity);
                break;
        }
    }

    public Material setMat(string color)
    {
        switch (color)
        {
            case "Blue":
                return this.BlueCubeMat;
            case "Red":
                return RedCubeMat;
            default:
                return this.GreenCubeMat;
        }
    }
}
