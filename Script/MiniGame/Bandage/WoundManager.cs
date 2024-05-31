using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundManager : MiniGameBase
{
    public Wounds[] AllWounds;
    public int CurrentIndex;

    [SerializeField] private GameObject Bandage;

    public override void OnValidate()
    {
        base.OnValidate();

        if (AllWounds.Length == 0)
            AllWounds = GetComponentsInChildren<Wounds>();
    }

    private void Start()
    {
        CurrentIndex = AllWounds.Length - 1;
    }

    public bool CheckWounds(int _id)
    {
        bool _isCorrect = _id == AllWounds[CurrentIndex].ID;
        if (_isCorrect)
        {
            AllWounds[CurrentIndex].Deactivated();
            CurrentIndex--;
        }

        if(CurrentIndex <= -1)
        {
            Bandage.SetActive(true);
            SaveScoreMinigame();
        }

        return _isCorrect;
    }

}
