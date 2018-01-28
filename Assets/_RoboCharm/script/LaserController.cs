using UnityEngine;

namespace NewtonVR
{
    public class LaserController : MonoBehaviour
    {
        public Transform BeamStart;
        public NVRButtons LaserButton = NVRButtons.Grip;

        private Laser laser;
        //NVRHand sometimes gets destroyed. Pull it each time.
        private NVRHand nvrHand
        {
            get
            {
                NVRHand hand = GetComponent<NVRHand>();
                if (hand == null)
                {
                    Debug.LogError("NVRHand is missing!");
                }
                return hand;
            }
        }

        private int controllerIndex = 0;
        public bool held = false;

        private Vector3? validTeleportPosition;

        private void Start()
        {
            laser = NVRPlayer.Instance.GetComponentInChildren<Laser>();
            if (laser == null)
            {
                Debug.LogError("Laser is Missing");
            }

            if (BeamStart == null)
                BeamStart = this.transform;

            controllerIndex = System.Convert.ToInt32(nvrHand.IsLeft);
        }

        private void FixedUpdate()
        {
            if (laser != null)
            {            
                if (nvrHand.Inputs[LaserButton].IsPressed)
                {
                    Debug.LogError("Laser is pressed");
                    laser.enabledLaser();
                    held = true;
                }
                else if (held)
                {
                    Debug.LogError("Laser is release");

                    laser.disabledLaser();
                    held = false;
                }
            }
        }
    }
}
