using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class StreamViewCamera : MonoBehaviour
{
    public GameObject[] targetObjects;
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .5f;

    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;

    public Camera cam;

    private void Start() 
    {
        CreateTargetList();
        // GetComponent<Camera>().transform.rotation = new Quaternion(0, 40,269,0);
    }

    private void LateUpdate()
    {
        CreateTargetList();

        if (targets.Count == 0) return;

        Move();
        Zoom();
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);

    }

    private void CreateTargetList()
    {
        targetObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var target in targetObjects)
        {
            targets.Add(target.transform);
        }
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].transform.position;
        }

        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return bounds.center;

    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    } 
}