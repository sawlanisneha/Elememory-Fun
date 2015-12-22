using UnityEngine;
using System.Collections;

public class CardFlipper : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public AnimationCurve scaleCurve2;
    public float duration = 1f;
    public Sprite front;
    public Sprite back;

    void Start()
    {
        //StartCoroutine(Wait());
    }


    public void FlipCard(Sprite startImage,Sprite endImage)
    {
        StopCoroutine(Flip(startImage, endImage));
        StartCoroutine(Flip(startImage, endImage));

    }

    IEnumerator Flip(Sprite startImage, Sprite endImage)
    {
        spriteRenderer.sprite = startImage;
        float time = 0f;
        while (time <= 1f)
        {
            float scale = scaleCurve2.Evaluate(time);
            time = time + Time.deltaTime / duration;

            Vector3 localScale = spriteRenderer.transform.localScale;
            localScale.x = scale * 14f;
            spriteRenderer.transform.localScale = localScale;

            if(time>=0.5f)
            {
                spriteRenderer.sprite = endImage;

            }

            yield return new WaitForFixedUpdate();

            
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log(Time.time);
        FlipCard(back,front);
        Debug.Log(Time.time);
        yield return new WaitForSeconds(1f);
    }
}
