using UnityEngine;

namespace UnityExtension
{
    public static class UnityTouch
    {
        static readonly bool IsAndroid = Application.platform == RuntimePlatform.Android;
        static readonly bool IsIOS = Application.platform == RuntimePlatform.IPhonePlayer;
        static readonly bool IsPC = !IsAndroid && !IsIOS;
        static Vector3 beforePosition;

        public static UnityTouchPhase GetPhase()
        {
            if(IsPC)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    beforePosition = Input.mousePosition;
                    return UnityTouchPhase.Began;
                }
                else if(Input.GetMouseButton(0)) return UnityTouchPhase.Moved;
                else if(Input.GetMouseButtonUp(0)) return UnityTouchPhase.Ended;
            }
            else
            {
                if(Input.touchCount > 0) return (UnityTouchPhase)((int)Input.GetTouch(0).phase);
            }
            return UnityTouchPhase.None;
        }

        public static Vector3 GetPosition()
        { 
            if (IsPC)
            {
                if (GetPhase() != UnityTouchPhase.None) return Input.mousePosition;
            }
            else
            {
                if(Input.touchCount > 0) return Input.GetTouch(0).position;
            }
            return Vector3.zero;
        }

        public static Vector3 GetDeltaPosition()
        { 
            if (IsPC)
            {
                UnityTouchPhase phase = GetPhase();
                if(phase != UnityTouchPhase.None)
                {
                    Vector3 now = Input.mousePosition;
                    Vector3 delta = now - beforePosition;
                    beforePosition = now;
                    return delta;
                }
            } 
            else
            {
                if (Input.touchCount > 0) return Input.GetTouch(0).deltaPosition;
            }
            return Vector3.zero;
        }
    }

    public enum UnityTouchPhase
    {
        None       = -1,
        Began      = 0,
        Moved      = 1,
        Stationary = 2,
        Ended      = 3,
        Canceled   = 4
    }
}
