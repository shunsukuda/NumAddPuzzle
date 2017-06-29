using UnityEngine;
using UnityExtension;
using System.Collections;

public class GlobeObject : MonoBehaviour
{
    private SpriteRenderer sprite;
    private CircleCollider2D colider;
    private static readonly Color touchedColor = new Color32(128, 128, 128, 255);
    void Start()
    {
        sprite = gameObject.SafeGetComponent<SpriteRenderer>();
        colider = gameObject.SafeGetComponent<CircleCollider2D>();
    }

    public bool IsTouch(Vector2 pos)
    {
        if(colider.OverlapPoint(pos)) return true;
        else return false;
    }

    public void DoTouch()
    {
        sprite.color = touchedColor;
    }
}
