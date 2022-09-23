using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite hover, highlight;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hover = Resources.Load<Sprite>("Sprites/Hover");
        highlight = Resources.Load<Sprite>("Sprites/Highlight");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == 7)
            spriteRenderer.sprite = null;
        if (gameObject.layer == 8)
            spriteRenderer.sprite = hover;
        if (gameObject.layer == 9)
            spriteRenderer.sprite = highlight;
    }
}
