using UnityEngine;
using UnityExtension;
using System.Collections;

public class GlobeGenerator : MonoBehaviourSingleton<GlobeGenerator>
{
    private static readonly char[] globe_color_char = { 'R', 'G', 'B', 'Y' };

    GameObject[,] globePrefabs = new GameObject[4,11];
    bool didSetGlobePrefabs = false;

    void Start()
    {
        // base.IsSingleton();
    }

    public void InitGlobeGenerator()
    {
        if(didSetGlobePrefabs) return;
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 11; ++j)
            {
                globePrefabs[i, j] = (GameObject)Resources.Load("Prefab/Globe/" + globe_color_char[i] + "_" + j);
                if (j == 10) globePrefabs[i, j] = (GameObject)Resources.Load("Prefab/Globe/" + globe_color_char[i] + "_n");
            }
        }
        didSetGlobePrefabs = true;
    }

    public Vector3 GlobePosition(int width, int height)
    {
        if (width < 0 || width >= 9) print("Error: GlobeGenerator.GlobePosition() out of range!(width=" + width + ")");
        if (height < 0 || height >= 9) print("Error: GlobeGenerator.GlobePosition() out of range!(height=" + height + ")");
        return new Vector3(-2.4f + 0.6f * width, 1.8f - 0.6f * height, 0.0f);
    }

    public GameObject GlobeGenerate(GlobeColor color, int value, int width, int height)
    {
        if (width < 0 || width >= 9) print("Error: GlobeGenerator.GlobeGenerate() out of range!(width=" + width + ")");
        if (height < 0 || height >= 9) print("Error: GlobeGenerator.GlobeGenerate() out of range!(height=" + height + ")");
        if (value < 0 || value >= 10) print("Error: GlobeGenerator.GlobeGenerate() out of range!(value=" + value + ")");
        return Instantiate(globePrefabs[(int)color, value], GlobePosition(width, height), Quaternion.identity) as GameObject;
    }

}
