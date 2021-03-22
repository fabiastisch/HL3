using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    /**
     * Bomb explosion delay in sec.
     */
    public float delay;

    /**
     * Explosion Radius.
     */
    public float explosionRadius;

    /**
     * Explosion Force.
     */
    public float explosionForce;

    /**
     * Explosion Up Modifier, to throw other Objects upwards... like in unrealistic movies.
     */
    public float explosionUpModifier;

    /**
     * Reference to the InteractionLayerMask.
     */
    public LayerMask interactionLayer;

    /**
     * Reference to the Explosion Obj.
     */
    public GameObject explosionPrefab;

    /**
     * Reference to the explosion Effect
     */
    private GameObject exposionEffect;

    /**
     * Counter for Explosions.
     */
    public static int explosionCount = 0;

    // Start is called before the first frame update
    void Start() {
        Invoke(nameof(Explode), delay);
    }

    void Explode() {
        // Overlap Sphere creates a Sphere with a radius and a pos, and returns every Collider (on a given Layer) that touched that Sphere.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, interactionLayer);
        foreach (var c in hitColliders) {
            Rigidbody r = c.GetComponent<Rigidbody>();
            // Throw away hit objects
            r.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpModifier);
        }

        GetComponent<AudioSource>().Play();
        
        // Add Explosion Particles
        exposionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Make this Object invis.
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        Invoke(nameof(Kill), 3);
        explosionCount++;
    }

    void Kill() {
        Destroy(exposionEffect);
        Destroy(gameObject);
    }
}