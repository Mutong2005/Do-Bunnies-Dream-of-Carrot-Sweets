using System.Collections;
using UnityEngine;

public class SpawnedBubble : MonoBehaviour
{
    [HideInInspector] public Transform StartPoint;
    [HideInInspector] public Transform EndPoint;
    public float moveSpeed = 1.5f;
    public float BubbleTimeToFall = 3f;

    private SpriteRenderer spriteRenderer;
    private Vector3 nextPosition;
    private bool isFalling;
    private bool isDestroyed;

    private Animator bubbleAnimator;

    private void Awake()
    {
        bubbleAnimator = GetComponent<Animator>();
        
    }

    void Start()
    {
        transform.position = StartPoint.position;
        nextPosition = EndPoint.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        isFalling = false;
        isDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if (transform.position == nextPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            collision.gameObject.transform.parent = transform;
            bubbleAnimator.SetTrigger("isSquished");
            isFalling = true;
            StartCoroutine(BubblePopTimer());
            //StartCoroutine(ChangeRed());
        }
        else
        {
            // if(collision.gameObject.CompareTag("Player"))
            // {
            //     isDestroyed = true;
            //     SoundEffectManager.Play("Bubble");
            //     bubbleAnimator.SetTrigger("isPopping");
            //     Destroy(gameObject);
            // }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
            if(collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.transform.parent = null;
                isDestroyed = true;
                SoundEffectManager.Play("Bubble");
                bubbleAnimator.SetTrigger("isPopping");
                StartCoroutine(DestroyWithStyle());
                //yield return new WaitForSeconds(0.4f);
                //Destroy(gameObject);
            }
    }
    private IEnumerator DestroyWithStyle()
    {
        yield return new WaitForSeconds(0.2f);
        //bubbleAnimator.ResetTrigger("isPopping");
        Destroy(gameObject, bubbleAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    private IEnumerator BubblePopTimer()
    {   
        nextPosition = StartPoint.position;
        moveSpeed *= 0.2f;
        yield return new WaitForSeconds(BubbleTimeToFall - 0.3f);
        if (!isDestroyed){
            SoundEffectManager.Play("Bubble");
            bubbleAnimator.SetTrigger("isPopping");
        }
        yield return new WaitForSeconds(0.3f);
        if(!isDestroyed)
        Destroy(gameObject);
    }
    private IEnumerator ChangeRed()
    {
        Color startColor = Color.white;
        Color endColor = Color.red;
        float timeElapsed = 0f;

        while (timeElapsed < BubbleTimeToFall)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / BubbleTimeToFall);
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        spriteRenderer.color = endColor; // Ensure exact end color
    }
}
