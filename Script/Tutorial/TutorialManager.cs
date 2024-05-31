using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SaveLoadManager))]
public class TutorialManager : MiniGameBase
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int Index = 0;
    public int MaxScore = 16;

    public override void OnValidate()
    {
        base.OnValidate();

        if (!scoreText)
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    public override void Init()
    {
        base.Init();

        PlayerData _playerData = SaveLoadManager.LoadPlayerData();
        scoreText.text = _playerData.SpecialPointData.ToString();
    }

    public void IncreasedPoint()
    {
        Score += 10;
        scoreText.text = Score.ToString();
        Index++;

        if (Index == MaxScore)
            SaveScoreMinigame();
    }
}
