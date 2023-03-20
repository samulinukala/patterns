using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pawn : MonoBehaviour
{
    public int value { get; private set; }
    private void Start()
    {
        value = UnityEngine.Random.Range(1, 5);
    }
}