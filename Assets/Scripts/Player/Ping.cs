using UnityEngine;
using System.Collections;

public class Ping : MonoBehaviour
{
    public Transform player;
    public Transform ping;
    
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Awake() {
        StartCoroutine(TriggerPing());
    }

    // void Start()
    // {
    //     player = transform.parent.transform;
    //     posOffset = transform.position;
    // }

    // void Update()
    // {

    //     tempPos = posOffset;
    //     tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

    //     transform.position = tempPos;
    // }

    public IEnumerator TriggerPing()
    {
        transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        transform.gameObject.SetActive(false);

    }
}
