using UnityEngine;
using UnityExtension;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField]
    GameObject puzzleManagerPrefab;

    void Start()
    {
        base.IsSingleton();
        puzzleManagerPrefab = Resources.Load("Prefab/Manager/PuzzleManager") as GameObject;
        Instantiate(puzzleManagerPrefab);
    }
}
