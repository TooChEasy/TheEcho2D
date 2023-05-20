using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float bufferZone;
    [SerializeField] private Vector2 boundsMin;
    [SerializeField] private Vector2 boundsMax;
    // [SerializeField] private GameObject player;
    // [SerializeField] private bool drawDebugLines;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        //Bounds environmentBounds = CalculateEnvironmentBounds();
        Vector3 targetPosition = target.position + new Vector3(offset.x, offset.y, transform.position.z);

        //Vector2 maxPlayerPos = CalculateMaxPlayerPosition(environmentBounds);
        //Vector2 minPlayerPos = CalculateMinPlayerPosition(environmentBounds);

        //targetPosition.x = Mathf.Clamp(targetPosition.x, environmentBounds.min.x - bufferZone, environmentBounds.max.x - bufferZone);
        //targetPosition.y = Mathf.Clamp(targetPosition.y, environmentBounds.min.y - bufferZone, environmentBounds.max.y - bufferZone);

        targetPosition.x = Mathf.Clamp(targetPosition.x, boundsMin.x, boundsMax.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, boundsMin.y, boundsMax.y);
        //player.transform.position.x = Mathf.Clamp(player.transform.position.x, boundsMin.x, boundsMax.x);
        //player.transform.position.x = Mathf.Clamp(player.transform.position.y, boundsMin.y, boundsMax.y);
    }
    //private Bounds CalculateEnvironmentBounds()
    //{
    //    GameObject[] environmentObjects = GameObject.FindGameObjectsWithTag("Ground");

    //    Bounds environmentBounds = new Bounds(Vector3.zero, Vector3.zero);
    //    foreach (GameObject obj in environmentObjects)
    //    {
    //        Renderer renderer = obj.GetComponent<Renderer>();
    //        if (renderer != null)
    //        {
    //            environmentBounds.Encapsulate(renderer.bounds);
    //        }
    //    }

    //    return environmentBounds;
    //}
    //private Vector2 CalculateMinPlayerPosition(Bounds environmentBounds)
    //{
    //    // Calculate the minimum player position based on the camera's view frustum
    //    Vector2 minPlayerPos = new Vector2(
    //        environmentBounds.min.x - _camera.ViewportToWorldPoint(new Vector3(bufferZone, 0, 0)).x,
    //        environmentBounds.min.y - _camera.ViewportToWorldPoint(new Vector3(0, bufferZone, 0)).y);

    //    return minPlayerPos;
    //}
    //private Vector2 CalculateMaxPlayerPosition(Bounds environmentBounds)
    //{
    //    // Calculate the maximum player position based on the camera's view frustum
    //    Vector2 maxPlayerPos = new Vector2(
    //        environmentBounds.max.x - _camera.ViewportToWorldPoint(new Vector3(1 - bufferZone, 0, 0)).x,
    //        environmentBounds.max.y - _camera.ViewportToWorldPoint(new Vector3(0, 1 - bufferZone, 0)).y);

    //    return maxPlayerPos;
    //}
}
