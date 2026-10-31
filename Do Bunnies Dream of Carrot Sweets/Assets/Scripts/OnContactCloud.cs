using UnityEngine;

public class OnContactCloud : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundEffectManager.Play("Cloud");
        }
    }

}
