using UnityEngine;
using UnityEngine.SceneManagement;
using UnityExtension;

public class PuzzleManager : MonoBehaviourSingleton<PuzzleManager>
{
    [SerializeField]
    GameObject globeManagerPrefab;
    [SerializeField]
    GameObject puzzleUiPrefab;
    [SerializeField]
    GlobeManager globeManager;

    [SerializeField]
    PuzzleState state;
    [SerializeField]
    int life; // 体力
    [SerializeField]
    int stageLevel; // ステージレベル(目標値の生成使用)
    [SerializeField]
    int stageExp; // レベル計算用
    [SerializeField]
    int currentScore; // 現在のスコア
    [SerializeField]
    int totalScore; // 合計スコア
    [SerializeField]
    int targetScore; // 1回の目標値

    public int GetLife() { return life; }
    public int GetStageLevel() { return stageLevel; }
    public int GetCurrentScore() { return currentScore; }
    public int GetTotalScore() { return totalScore; }
    public int GetTargetScore() { return targetScore; }
    public PuzzleState GetState() { return state; }

    void Start()
    {
        base.IsSingleton();
        globeManagerPrefab = Resources.Load("Prefab/Manager/GlobeManager") as GameObject;
        puzzleUiPrefab = Resources.Load("Prefab/UI/PuzzleUI") as GameObject;
        Instantiate(globeManagerPrefab);
        Instantiate(puzzleUiPrefab);
        globeManager = GameObject.FindGameObjectWithTag("GlobeManager").SafeGetComponent<GlobeManager>();
        state = PuzzleState.Setup;
    }

    void Update()
    {
        switch (state)
        {
            case PuzzleState.Setup:
                life = DataObject.GetLife();
                stageLevel = 1;
                stageExp = 0;
                ChangeState();
                break;
            case PuzzleState.Load:
                currentScore = 0;
                globeManager.NewGlobeGenerate();
                ChangeState();
                break;
            case PuzzleState.Wait:
            case PuzzleState.Touch:
                globeManager.CheckTouch();
                break;
            case PuzzleState.End:
                globeManager.GlobeDelete();
                CheckScore();
                SetTarget();
                ChangeState();
                break;
            case PuzzleState.GameOver:
                if (UnityTouch.GetPhase() == UnityTouchPhase.Began) SceneManager.LoadScene("title");
                break;
        }
    }

    public void ChangeState()
    {
        switch (state)
        {
            case PuzzleState.Setup:
                state = PuzzleState.Wait;
                break;
            case PuzzleState.Load:
                state = PuzzleState.Wait;
                break;
            case PuzzleState.Wait:
                state = PuzzleState.Touch;
                break;
            case PuzzleState.Touch:
                state = PuzzleState.End;
                break;
            case PuzzleState.End:
                state = PuzzleState.Load;
                break;
        }
    }

    public void ChangeState(PuzzleState ps)
    {
        state = ps;
    }

    public void SetTarget()
    {
         targetScore = Random.Range(stageLevel*10, (stageLevel+1)*10); 
    }

    void CheckScore()
    {
        float scoreRate = 0.0f;
        int exp = 0;
        int damage = (int)Mathf.Abs(targetScore - currentScore);
        switch(damage)
        {
            case 0:
                scoreRate = 1.50f;
                exp = 5;
                break;
            case 1:
                scoreRate = 1.25f;
                exp = 4;
                break;
            case 2:
                scoreRate = 1.00f;
                exp = 3;
                break;
            case 3:
                scoreRate = 0.75f;
                exp = 4;
                break;
            case 4:
                scoreRate = 0.50f;
                exp = 2;
                break;
            case 5:
                scoreRate = 0.25f;
                exp = 1;
                break;
            default:
                scoreRate = 0.10f;
                exp = 0;
                break;
        }
        life -= damage;
        IncExp(exp);
        totalScore += (int)(targetScore * scoreRate * DataObject.GetScoreRate());
        if(life <= 0) state = PuzzleState.GameOver;
    }

    void IncExp(int exp)
    {
        stageExp += exp;
        if(state != PuzzleState.End) return;
        // nごとにレベルアップ
        if(stageExp != 0 && stageExp%20 == 0) ++stageLevel;
    }

    public void IncCurrentScore(int score)
    {
        currentScore += score;
    }
}

public enum PuzzleState
{
    Setup = -1, // 初期読み込み
    Load = 0, // 読み込み中
    Wait = 1, // 待ち状態
    Touch = 2, // タッチ中
    End = 3, // 盤面処理
    GameOver = 10 // ゲームオーバー
}
