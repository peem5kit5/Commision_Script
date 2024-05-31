using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(SaveLoadManager))]
public class RecycleManager : MiniGameBase
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Trashes and Bins")]
    public Trashes[] Trashes;
    public Bin[] Bins;

    [Header("Times And Points")]
    public int CurrentTrashesCount;
    public float CurrentTime;
    public float MaxTime;
    public int CurrentPoint = 0;

    public bool Start;

    private Dictionary<string, Bin> binDict = new Dictionary<string, Bin>();

    public override void OnValidate()
    {
        base.OnValidate();

        if (Trashes.Length == 0)
            Trashes = FindObjectsOfType<Trashes>();

        if (Bins.Length == 0)
            Bins = FindObjectsOfType<Bin>();

        if (!scoreText)
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        if (!timeText)
            timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Start)
        {
            CurrentTime -= Time.deltaTime;
            timeText.text = Mathf.RoundToInt(CurrentTime).ToString(); ;

            if (CurrentTime <= 0)
            {
                var _sceneController = new SceneController();
                _sceneController.ChangeScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void StartPicking() => Start = true;

    public override void Init()
    {
        base.Init();

        binDict = GetBinDict();
        CurrentTrashesCount = Trashes.Length - 1;

        scoreText.text = "0";
        timeText.text = MaxTime.ToString();

        CurrentTime = MaxTime;
    }

    private Dictionary<string, Bin> GetBinDict()
    {
        Dictionary<string, Bin> _binDict = new Dictionary<string, Bin>();

        foreach (Bin _bin in Bins)
            _binDict.Add(_bin.BinID, _bin);

        return _binDict;
    }

    public bool CheckTrash(Bin _bin, Trashes _trashID)
    {
        if (_bin.BinID != _trashID.TrashID) 
            return false;
        else
        {
            CurrentPoint += 10;
            scoreText.text = CurrentPoint.ToString();
            CurrentTrashesCount--;

            if (CurrentTrashesCount <= -1)
            {
                Score += CurrentPoint;
                SaveScoreMinigame();
            }

            return true;
        }
            
    }
}
