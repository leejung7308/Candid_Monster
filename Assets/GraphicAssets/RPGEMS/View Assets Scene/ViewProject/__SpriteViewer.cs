using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __SpriteViewer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public int i = 0;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            i += 1;
            if (i > sprites.Length)
            {
                i = 0;
            }
            this.spriteRenderer.sprite = sprites[i];


        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            i -= 1;
            if (i < 0)
            {
                i = 319;
            }
            this.spriteRenderer.sprite = sprites[i];


        }



    }

    public void E()
    {
        i += 1;
        if (i > sprites.Length)
        {
            i = 0;
        }
        this.spriteRenderer.sprite = sprites[i];
    }

    public void Q()
    {
        i -= 1;
        if (i < 0)
        {
            i = 319;
        }
        this.spriteRenderer.sprite = sprites[i];
    }
}
