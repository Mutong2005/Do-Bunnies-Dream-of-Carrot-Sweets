using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            else
            SceneManager.LoadScene(0); //will load menu if implemented :( 
        }
    }

}
