using UnityEngine;
using System.Collections;

public class Globe : MonoBehaviour
{
    GameObject gobject;
    GlobeColor color;
    int value;
    bool isTouch;

    public GameObject Object { get { return gobject; } }  
    public GlobeColor Color { get { return color; } }
    public int Value { get { return value; } }
    public bool IsTouch { get { return this.isTouch; } }

    public Globe(GameObject g, GlobeColor c, int v)
    {
        gobject = g;
        color = c;
        value = v;
        isTouch = false;
    }

    public void DoTouch() { isTouch = true; }

    public void Delete() { Destroy(gobject); }
}
