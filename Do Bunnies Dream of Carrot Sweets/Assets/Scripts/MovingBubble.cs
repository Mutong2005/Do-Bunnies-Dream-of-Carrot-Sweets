using UnityEngine;
using System.Collections;


public class MovingBubble : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public float moveSpeed = 1.5f;
    public float BubbleTimeToFall = 3f;

    private Vector3 nextPosition;

    void Start()
    {
        transform.position = StartPoint.position;
        nextPosition = EndPoint.position;
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
        }
    }

    IEnumerator BubblePopTimer()
    {
        yield return new WaitForSeconds(BubbleTimeToFall);
        transform.position = StartPoint.position;
        nextPosition = EndPoint.position;
    }
}
