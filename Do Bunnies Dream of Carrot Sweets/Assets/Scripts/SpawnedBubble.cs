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

    void Start()
    {
        transform.position = StartPoint.position;
        nextPosition = EndPoint.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        isFalling = false;
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
            isFalling = true;
            StartCoroutine(BubblePopTimer());
            StartCoroutine(ChangeRed());
        }
    }

    private IEnumerator BubblePopTimer()
    {   
        nextPosition = StartPoint.position;
        moveSpeed *= 0.2f;
        yield return new WaitForSeconds(BubbleTimeToFall);
        SoundEffectManager.Play("Bubble");
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
