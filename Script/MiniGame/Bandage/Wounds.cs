using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wounds : MonoBehaviour
{
    public int ID;
    public void Deactivated() => Destroy(gameObject);
}
