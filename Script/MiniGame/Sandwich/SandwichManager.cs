using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveLoadManager))]
public class SandwichManager : MiniGameBase
{
    public int CurrentIndex = 0;
    public InSandwich[] InSandwiches;
    public List<Ingredient> AllIngredients;

    public bool IsBread;
    public bool IsPlastic;
    public bool IsHamCheese;
    public string BadEnd = "TSWBad";

    public override void OnValidate()
    {
        base.OnValidate();
    }

    private void IncreasedIndex()
    {
        CurrentIndex++;
        Score += 10;

        if(IsBread && !IsPlastic && CurrentIndex > 1)
        {
            Debug.Log("IsBread");
            SaveScoreMinigame();
        } 
        else if(IsBread && IsPlastic)
        {
            var _sceneController = new SceneController();
            _sceneController.ChangeScene(BadEnd);
        }

    }

    public bool Check(Ingredient _ingredient)
    {
        foreach (InSandwich _inSandWiches in InSandwiches)
        {
            if (_inSandWiches.ID == _ingredient.ID)
            {
                _inSandWiches.Wopper();
                AllIngredients.Remove(_ingredient);
                IncreasedIndex();
                return true;
            }
        }
        return false;
    }

}
