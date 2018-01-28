using UnityEngine;
using System.Collections;

namespace NewtonVR
{
    public class Laser : MonoBehaviour
    {
        public LineRenderer laserLineRenderer;
        public float laserWidth = 0.01f;
        public float laserMaxLength = 5f;
        public Vector3 targetPosition;
        public Vector3 direction;
        public NVRButtons LaserButton = NVRButtons.Trigger;

        void Start()
        {
        }

        public void enabledLaser(Transform BeamStart, int controllerIndex)
        {
            this.updateLaserLocation(BeamStart, controllerIndex);
            laserLineRenderer.enabled = true;
        }

        public void disabledLaser(int controllerIndex)
        {
            laserLineRenderer.enabled = false;
        }

        void updateLaserLocation(Transform initLaserTransform, int controllerIndex)
        {
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
                if (raycastHit.collider.gameObject.GetComponent<IRobot>() != null)
                {
                    raycastHit.collider.gameObject.GetComponent<IRobot>().TriggerAction();
                }
                else
                {
                    Debug.Log("You are hitting : " + raycastHit.collider);
                }
                endPosition = raycastHit.point;
            }

            laserLineRenderer.SetPosition(0, targetPosition);
            laserLineRenderer.SetPosition(1, endPosition);
        }
    }
}