using UnityEngine;
using System.Collections;

namespace UnityExtension
{
    public static class ExRandom
    {
        public static float Gaussian(float mu = 0.0f, float sigma = 1.0f, bool useSin = true)
        {
            float x = Random.value;
            float y = Random.value;
            float result = 0.0f;
            if (useSin) result = Mathf.Sqrt(-2.0f * Mathf.Log(x)) * Mathf.Sin(2.0f * Mathf.PI * y);
            else result = Mathf.Sqrt(-2.0f * Mathf.Log(x)) * Mathf.Cos(2.0f * Mathf.PI * y);
            return result * sigma + mu;
        }
    }
}