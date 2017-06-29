using UnityEngine;
using UnityExtension;
using System.Collections;
using System.Collections.Generic;

public enum GlobeColor { Red, Green, Blue, Yellow };

public class GlobeManager : MonoBehaviourSingleton<GlobeManager>
{
    [SerializeField]
    Camera main_camera;
    [SerializeField]
    GameObject generatorPrefab;
    [SerializeField]
    PuzzleManager puzzleManager;
    [SerializeField]
    GlobeGenerator generator;

    Globe[,] globeArray = new Globe[9, 9];
    Stack<Vector2> touchedStack = new Stack<Vector2>();
    Stack<Vector2> deletedStack = new Stack<Vector2>();

    public int GetGlobeSum()
    {
        int sum = 0;
        for(int i = 0; i < 9; ++i)
        {
            for(int j = 0; j < 9; ++j)
            {
                sum += globeArray[i, j].Value;
            }
        }
        return sum;
    }
    public int GetGlobeAverage() { return (int)(GetGlobeSum() / 81); }

    void Start ()
    {
        // base.IsSingleton();
        main_camera = Camera.main.SafeGetComponent<Camera>();
        puzzleManager = GameObject.FindGameObjectWithTag("PuzzleManager").SafeGetComponent<PuzzleManager>();

        generatorPrefab = Resources.Load("Prefab/Manager/GlobeGenerator") as GameObject;
        Instantiate(generatorPrefab);
        generator = GameObject.FindGameObjectWithTag("GlobeGenerator").SafeGetComponent<GlobeGenerator>();
        generator.InitGlobeGenerator();
        InitGlobeArray();
        puzzleManager.SetTarget();
	}

    void GlobeGenerate(int x, int y)
    {
        GlobeColor color = (GlobeColor)Random.Range(0, 4);
        int value = Random.Range(1, 10);
        globeArray[x, y] = new Globe(generator.GlobeGenerate(color, value, x, y), color, value);
    }

    void InitGlobeArray()
    {
        for (int i = 0; i < 9; ++i)
        {
            for (int j = 0; j < 9; ++j) GlobeGenerate(i, j);
        }
    }

    public void CheckTouch()
    {
        Vector3 p_pos = UnityTouch.GetPosition(); // ピクセル座標
        Vector2 u_pos = main_camera.ScreenToWorldPoint(p_pos); // Unity座標

        UnityTouchPhase phase = UnityTouch.GetPhase();
        if (phase == UnityTouchPhase.None) return;
        else if (phase == UnityTouchPhase.Began)
        {
            if (Mathf.Abs(u_pos.x) >= 2.7f || Mathf.Abs(u_pos.y) >= 3.3f)
            {
                puzzleManager.ChangeState(PuzzleState.Wait);
                return;
            }
            puzzleManager.ChangeState();
        }
        else if (phase == UnityTouchPhase.Ended)
        {
            puzzleManager.ChangeState();
            return;
        }
        
        if (puzzleManager.GetState() == PuzzleState.Touch)
        {
            if(Mathf.Abs(u_pos.x) >=2.7f || Mathf.Abs(u_pos.y) >= 3.3f)
            {
                puzzleManager.ChangeState();
                return;
            }
        }

        for(int i = 0; i < 9; ++i)
        {
            for(int j = 0; j < 9; ++j)
            {
                GlobeObject g_obj = globeArray[i, j].Object.SafeGetComponent<GlobeObject>();
                if(g_obj.IsTouch(u_pos))
                {
                    // 既にタッチしたGlobeの処理
                    if (globeArray[i, j].IsTouch)
                    {
                        // 直前に触れたGlobeは処理しない
                        if (touchedStack.Count != 0 && touchedStack.Peek() == new Vector2(i, j)) return;
                        else
                        {
                            puzzleManager.ChangeState();
                            return;
                        }
                    }
                    g_obj.DoTouch(); // Globeの色を変える
                    globeArray[i, j].DoTouch(); // IsTouch = true
                    touchedStack.Push(new Vector2(i, j));
                }
            }
        }
    }

    public void GlobeDelete()
    {
        if(puzzleManager.GetState() != PuzzleState.End) return;

        int score = 0;
        Vector2 pos = new Vector2();
        while(touchedStack.Count != 0)
        {
            pos = touchedStack.Pop();
            score = globeArray[(int)pos.x, (int)pos.y].Value;
            puzzleManager.IncCurrentScore(score);
            globeArray[(int)pos.x, (int)pos.y].Delete();
            deletedStack.Push(pos);
        }
    }

    public void NewGlobeGenerate()
    {
        Vector2 pos = new Vector2();

        while(deletedStack.Count != 0)
        {
            pos = deletedStack.Pop();
            GlobeGenerate((int)pos.x, (int)pos.y);
        }
    }
}
