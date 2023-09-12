using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    //Asignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsPlayer;

    //Stats
    [Range(0f,1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;

    //Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouche = true;

    int collisions;
    PhysicMaterial phisics_mat;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if(collisions > maxCollisions) Explode();

        //Countdown lifetime
        maxLifetime -= Time.deltaTime;
        if(maxLifetime<= 0) Explode();
    }

    private void OnCollisionEnter(Collision other)
    {
        //Count up collisions
        collisions++;

        //Explode if bullet hits a player direcly and explodeOnTouch is activated
        if(other.collider.CompareTag("Player") && explodeOnTouche) Explode();
    }

    private void Explode()
    {
        //Instantiate explosion
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
    
        //Check for player?
        Collider [] player = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
        for (int i = 0; i<player.Length; i++)
        {
            //
        }

        //Add a delay to make sure that everything works fine
        Invoke("Delay",0.05f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void Setup()
    {
        //Create a new Physic material
        phisics_mat = new PhysicMaterial();
        phisics_mat.bounciness = bounciness;
        phisics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        phisics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assign material to collider
        GetComponent<SphereCollider>().material = phisics_mat;

        //Set gravity
        rb.useGravity = useGravity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
