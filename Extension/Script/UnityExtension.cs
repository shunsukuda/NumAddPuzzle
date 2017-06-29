using UnityEngine;

namespace UnityExtension
{
    public static class UnityExtension
    {
        public static T SafeGetComponent<T>(this GameObject self) where T : Component
        {
            return self.GetComponent<T>() ?? self.AddComponent<T>();
        }

        public static T SafeGetComponent<T>(this Component self) where T : Component
        {
            return self.GetComponent<T>() ?? self.gameObject.AddComponent<T>();
        }
    }
}