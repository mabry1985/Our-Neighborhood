using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentManager : MonoBehaviour
{
    public GameObject clouds;
    public float cloudSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveClouds();
    }

    private void MoveClouds(){
        clouds.transform.position += Vector3.forward * Time.deltaTime * cloudSpeed;
    }
}
