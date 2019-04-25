using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static string Difficulty;
    public static readonly float EscalationFactor = 0.05f;
    public static int Score;
    public static int FishType;
    public static bool InGame;
    public static System.Random rnd = new System.Random();

    public static float UpwardsSpeed = 1;
    public static float Speed = 0.5f;

    public static float CustomFishSpeed; // 0 - 10
    public static float CustomPlayerSpeed;  // 0 - 10
    public static float CustomBubblesSpeed;  // 0 - 10
    public static float CustomMaxOxygen; // 5 - 100
    public static float CustomOxygenFactor; // 0 - 100
    public static float CustomProbabilityGoodFish; // 0 - 100
}
