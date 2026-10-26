using UnityEngine;
using System.Collections;


public class MovingBubble : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public float moveSpeed = 1.5f;
    public float BubbleTimeToFall = 3f;
    private SpriteRenderer spriteRenderer;

    private Vector3 nextPosition;

    void Start()
    {
        transform.position = StartPoint.position;
        nextPosition = EndPoint.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if (transform.position == nextPosition)
        {
            transform.position = StartPoint.position;
            nextPosition = EndPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BubblePopTimer());
            StartCoroutine(ChangeRed());
        }
    }

    private IEnumerator BubblePopTimer()
    {
        //spriteRenderer.color = Color.yellow;
        //yield return new WaitForSeconds(BubbleTimeToFall/3f);
        //spriteRenderer.color = Color.orange;
        //yield return new WaitForSeconds(BubbleTimeToFall / 3f);
        //spriteRenderer.color = Color.red;
        //yield return new WaitForSeconds(BubbleTimeToFall / 3f);
        yield return new WaitForSeconds(BubbleTimeToFall);
        transform.position = StartPoint.position;
        nextPosition = EndPoint.position;
        spriteRenderer.color = Color.white;
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
