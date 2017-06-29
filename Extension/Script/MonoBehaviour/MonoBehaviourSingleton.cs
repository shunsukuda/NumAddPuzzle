using UnityEngine;

namespace UnityExtension
{
    public class MonoBehaviourSingleton<T> : MonoBehaviourExtension where T : MonoBehaviourExtension
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        Debug.LogError(typeof(T) + "is nothing");
                    }
                }
                return instance;
            }
        }

        protected void IsSingleton()
        {
            if (this != Instance)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}