using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    public bool enabled = false;
    public LineRenderer laserLineRenderer;
    public float laserWidth = 0.01f;
    public float laserMaxLength = 5f;
    public Vector3 targetPosition;
    public Vector3 direction;

    void Start()
    {
        this.updateLaserLocation();
    }

    public void enabledLaser()
    {
        this.updateLaserLocation();
        laserLineRenderer.enabled = true;
    }

    public void disabledLaser()
    {
        laserLineRenderer.enabled = false;
    }

    void updateLaserLocation()
    {
        Transform initLaserTransform = this.GetComponentInParent<Transform>();
        this.targetPosition = initLaserTransform.position;
        this.direction = initLaserTransform.rotation * Vector3.forward;

        laserLineRenderer.startWidth = laserLineRenderer.endWidth = this.laserWidth;

        ShootLaserFromTargetPosition(this.targetPosition, this.direction, this.laserMaxLength);
    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
        }

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
    }
}