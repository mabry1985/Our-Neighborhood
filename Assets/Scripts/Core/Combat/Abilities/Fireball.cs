using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float speed = 3f;
    public float maxLifeTime = 5f;

    public float minDamage = 15f;
    public float maxDamage = 25f;

    private void Awake()
    {
        Destroy(this.gameObject, maxLifeTime);
        Physics.IgnoreLayerCollision(9, 9);
    }

    private void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision col)
    {
        float randomDamage = Random.Range(minDamage, maxDamage);
        Destroy(this.gameObject);
        Health targetHealth = col.transform.root.GetComponent<Health>();
        if (targetHealth == null) return;
        
        targetHealth.TakeDamage(randomDamage);
        
    }
}
