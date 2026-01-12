using UnityEngine;

public class Collectible : MonoBehaviour
{
    //variabilele pentru efectele vizuale si sonore
    public GameObject visualEffect; 
    public AudioClip soundEffect;

    // Update is called once per frame
    void Update()
    {
        // Rotatia stelei
        //Time.deltaTime - pt ca stelele sa se roteasca uniform indiferent de fps
        transform.Rotate(15 * Time.deltaTime, 30 * Time.deltaTime, 0);
    }

    //functia apelata in momentul colectarii
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Main Camera" || other.tag == "MainCamera")
        {
            GameManager.instance.AddScore();

            if (visualEffect != null)
            {
                Instantiate(visualEffect, transform.position, Quaternion.identity);
            }

            if (soundEffect != null)
            {
                AudioSource.PlayClipAtPoint(soundEffect, transform.position);
            }

            Destroy(gameObject);
        }
    }
}