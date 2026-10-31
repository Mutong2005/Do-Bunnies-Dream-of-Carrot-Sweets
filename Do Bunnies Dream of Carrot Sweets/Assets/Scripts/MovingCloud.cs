using UnityEngine;
using System.Collections;


public class MovingCould : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public float moveSpeed = 1.5f;
    private bool isMovingToEnd;
    
    private Vector3 nextPosition;

    void Start()
    {
        transform.position = StartPoint.position;
        nextPosition = EndPoint.position;
        isMovingToEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPosition) < 0.01f)
        {
            // Toggle the direction flag
            isMovingToEnd = !isMovingToEnd;

            // Assign the new destination based on the flag
            if (isMovingToEnd)
            {
                nextPosition = EndPoint.position;
            }
            else
            {
                nextPosition = StartPoint.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundEffectManager.Play("Cloud");
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
