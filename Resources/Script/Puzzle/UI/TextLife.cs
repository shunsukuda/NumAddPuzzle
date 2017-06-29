using UnityEngine;
using UnityEngine.UI;
using UnityExtension;

public class TextLife : MonoBehaviour
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
        text.text = "ライフ：" + puzzle_manager.GetLife().ToString();
    }
}