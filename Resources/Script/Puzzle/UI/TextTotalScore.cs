using UnityEngine;
using UnityEngine.UI;
using UnityExtension;

public class TextTotalScore : MonoBehaviour
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
        text.text = "合計スコア：" + puzzle_manager.GetTotalScore().ToString();
    }
}