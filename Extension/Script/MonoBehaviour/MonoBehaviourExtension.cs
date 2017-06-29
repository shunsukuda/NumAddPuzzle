using UnityEngine;
using System.Collections;

namespace UnityExtension
{
    public class MonoBehaviourExtension : MonoBehaviour
    {
        protected IEnumerator Wait(float time = 0.0f)
        {
            if(time <= 0.0f) yield return null;
            else yield return new WaitForSeconds(time); 
        }
    }
}
