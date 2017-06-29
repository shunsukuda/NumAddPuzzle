using UnityEngine;
using UnityEngine.UI;
using UnityExtension;

public class TextState : MonoBehaviour
{
    Text text;
    PuzzleManager puzzle_manager;

    void Start()
    {
        if(!Application.isEditor) Destroy(gameObject);
        text = this.SafeGetComponent<Text>();
        puzzle_manager = GameObject.FindGameObjectWithTag("PuzzleManager").SafeGetComponent<PuzzleManager>();
    }

    void Update()
    {
        string str = "";
        switch (puzzle_manager.GetState())
        {
            case PuzzleState.Setup:
                str = "Setup";
                break;
            case PuzzleState.Load:
                str = "Load";
                break;
            case PuzzleState.Wait:
                str = "Wait";
                break;
            case PuzzleState.Touch:
                str = "Touch";
                break;
            case PuzzleState.End:
                str = "End";
                break;
            case PuzzleState.GameOver:
                str = "GameOver";
                break;
        }
        text.text = str;
    }
}