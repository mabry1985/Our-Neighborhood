using UnityEngine;
using System.Collections;

public class Ping : MonoBehaviour
{
    public float time;

    private void OnEnable() {
        StartCoroutine(TriggerPing());
    }


    public IEnumerator TriggerPing()
    {
        transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        transform.gameObject.SetActive(false);

    }
}
