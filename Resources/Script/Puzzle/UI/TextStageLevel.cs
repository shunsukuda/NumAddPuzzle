using UnityEngine;
using UnityEngine.UI;
using UnityExtension;

public class TextStageLevel : MonoBehaviourExtension
{
    [SerializeField]
    Text text;
    [SerializeField]
    PuzzleManager puzzleManager;

    void Start()
    {
        text = this.SafeGetComponent<Text>();
        puzzleManager = GameObject.FindGameObjectWithTag("PuzzleManager").SafeGetComponent<PuzzleManager>();
    }

    void Update()
    {
        text.text = "レベル：" + puzzleManager.GetStageLevel().ToString();
    }
}