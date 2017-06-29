using UnityEngine;
using UnityEngine.UI;
using UnityExtension;

public class TextTargetScore : MonoBehaviour
{
    Text text;
    PuzzleManager puzzle_manager;

    void Start()
    {
        text = this.SafeGetComponent<Text>();
        puzzle_manager = GameObject.FindGameObjectWithTag("PuzzleManager").SafeGetComponent<PuzzleManager>();
    }

    void Update()
    {
        text.text = "目標スコア：" + +puzzle_manager.GetCurrentScore() + "/" + puzzle_manager.GetTargetScore().ToString();
    }
}