using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static string Difficulty;
    public static float EscalationFactor = 0.05f;
    public static int Score;
    public static int FishType;
    public static bool InGame = true;
    public static System.Random rnd = new System.Random();

    public static float upwardsSpeed = 1;
    public static float speed = 0.5f;
}
