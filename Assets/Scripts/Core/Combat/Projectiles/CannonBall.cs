using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float thrust = 25.0f;
    public float maxLifeTime = 25.0f;
    public GameObject explosion;

    public float minDamage = 15.0f;
    public float maxDamage = 25.0f;
    public float explosionRadius = 20f;

    private void Start()
    {
        //Destroy(this.gameObject, maxLifeTime);
        Physics.IgnoreLayerCollision(9, 9);
        GetComponent<Rigidbody>().AddForce(transform.forward * thrust);
    }

    private void Update()
    {
        //transform.Translate(0, 0, Time.deltaTime * speed);
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     float randomDamage = Random.Range(minDamage, maxDamage);
    //     Destroy(this.gameObject);
    //     Health targetHealth = col.transform.root.GetComponent<Health>();
    //     if (targetHealth == null) return;

    //     targetHealth.TakeDamage(randomDamage);

    // }

    void OnCollisionEnter(Collision col)
    {
        Explosion();
    }

    private void Explosion()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Health targetHealth = nearbyObject.GetComponent<Health>();

            if (targetHealth)
            {
                float damage = CalculateDamage(nearbyObject.transform.position);
                targetHealth.TakeDamage(damage);
            }
            print(nearbyObject.name);
        }

        GameObject e = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        ParticleSystem particle = e.GetComponent<ParticleSystem>();
        // explosionAudio.Play();
        particle.Play();
        e.SetActive(true);
        Destroy(e, 1.5f);
        Destroy(this.gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
        float damage = relativeDistance * maxDamage;
        damage = Mathf.Max(0f, damage);
        print(damage);

        return damage;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
