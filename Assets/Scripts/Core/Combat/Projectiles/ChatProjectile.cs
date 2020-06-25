using UnityEngine;

public class CannonProjectile : MonoBehaviour {

	public GameObject explosion;
    public LayerMask tankMask;
    // public AudioSource explosionAudio;
    public float minDamage = 10f;
    public float maxDamage = 20f;
    public float maxLifeTime = 30f;
    public float explosionRadius = 5f;
	[SerializeField] float speed;

    void OnCollisionEnter(Collision col)
    {
    	GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
		ContactPoint contact = col.contacts[0];
		Health targetHealth = col.gameObject.GetComponent<Health>();

        if(targetHealth) {
		  float damage = Random.Range(minDamage, maxDamage);
		  targetHealth.TakeDamage(damage);
		}
    	//Destroy(e, 1.5f);
    	//Destroy(this.gameObject);
    }

	void Start () 
	{
	  //Destroy(gameObject, maxLifeTime);
	  Physics.IgnoreLayerCollision(9, 9);
	}

    private void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

		float explosionDistance = explosionToTarget.magnitude;

		float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

		float damage = relativeDistance * maxDamage;
        
		damage = Mathf.Max(0f, damage);

		return damage;
    }
}