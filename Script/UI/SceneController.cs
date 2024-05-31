using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController
{
    public void ChangeScene(string _name) => SceneManager.LoadScene(_name);
    public void ChangeScene(int _index) => SceneManager.LoadScene(_index);
}
