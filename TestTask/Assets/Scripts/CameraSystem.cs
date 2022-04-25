using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    #region Singleton
    public static CameraSystem instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private Transform camController;
    
    [SerializeField] private float rotationSpeed;

    private Vector3 center;
    private Bounds camBounds;
    private Vector3 velocity;
    [SerializeField] private float smoothTime;

    private float minZoom = 120f;
    private float maxZoom = 30f;
    private float greatestDistance;
    private float targetZoom;
    [SerializeField] private float zoomLimiter;
    [SerializeField] private Camera cam;

    private void LateUpdate()
    {
        center = GetCenter();
        camController.position = Vector3.SmoothDamp(camController.position, center, ref velocity, smoothTime); //move the camera controller to the center point smoothly

        camController.RotateAround(Vector3.up, rotationSpeed * Time.deltaTime); //rotate the cam controller over the Y-axis

        targetZoom = Mathf.Lerp(maxZoom, minZoom, greatestDistance / zoomLimiter); //calculate the target field of view value
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetZoom, Time.deltaTime); //smoothly change the field ov view to the target value
    }

    Vector3 GetCenter()
    {
        if (PlaceholderSystem.instance.placeholders.Count == 1) //if there is no more than 1 placeholder
        {
            return PlaceholderSystem.instance.placeholders[0].position; //set the camera focus point to it
        }

        camBounds = new Bounds(PlaceholderSystem.instance.placeholders[0].position, Vector3.zero); //reset the bounds
        for (int i = 0; i < PlaceholderSystem.instance.placeholders.Count; i++) //for every placeholder
        {
            camBounds.Encapsulate(PlaceholderSystem.instance.placeholders[i].position); //resize the bounds to include the placeholder
        }
        greatestDistance = camBounds.size.x;
        return camBounds.center; //return the center point vector3 value
    }
}
