using UnityEngine;
using UnityExtension;
using System.Collections;

public static class DataObject
{
    static int life;
    static float scoreRate;

    public static int GetLife() { return life; }
    public static float GetScoreRate() { return scoreRate; }
    public static void SetLife(int value) { life = value; }
    public static void SetScoreRate(float value) { scoreRate = value; }
}
