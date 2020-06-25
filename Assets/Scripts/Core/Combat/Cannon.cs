using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject chatPlayer;
    public GameObject cannonBall;
    public GameObject pivotPoint;

    public float horizontalAngleIncrement= .15f;
    public float verticalAngleIncrement = .15f;

    public bool isMounted = true;

    void Update()
    {
        if (!isMounted) return;

        if (Input.GetMouseButtonDown(0))
        {
            FireCannonBall();
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -horizontalAngleIncrement, 0);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, horizontalAngleIncrement, 0);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            if (pivotPoint.transform.rotation.eulerAngles.z <= 2) return;

            pivotPoint.transform.Rotate(0, 0, -verticalAngleIncrement);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            if (pivotPoint.transform.rotation.eulerAngles.z >= 30) 
            {
                return;
            }
            pivotPoint.transform.Rotate(0, 0, verticalAngleIncrement);
        }
    }


    public void FireProjectile(GameObject projectile)
    {
        StartCoroutine(FireCoroutine(projectile));
    }

    public void FireCannonBall()
    {
        StartCoroutine(FireCoroutine(cannonBall));
    }

    public IEnumerator FireCoroutine(GameObject projectile)
    {
        yield return new WaitForSeconds(0);

        Transform pSpawnPoint = GetComponentInChildren<ProjectileSpawnPoint>().transform;

        Instantiate(projectile, pSpawnPoint.position, pSpawnPoint.rotation);

        print("cannon fired");
    }


}
