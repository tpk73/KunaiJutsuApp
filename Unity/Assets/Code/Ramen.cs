using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramen : MonoBehaviour
{

    [SerializeField] private ParticleSystem applyParticle;

    private BoxCollider2D myCollider2D;
    private SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        myCollider2D = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Kunai"))
        {
            myCollider2D.enabled = false;
            sp.enabled = false;
            transform.parent = null;
            SoundManager.Instance.playObjectKilledButton();

            applyParticle.Play();
            Destroy(gameObject, 2f);

        }
    }
}
