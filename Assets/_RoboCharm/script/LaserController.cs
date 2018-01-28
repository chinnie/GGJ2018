using UnityEngine;

namespace NewtonVR
{
    public class LaserController : MonoBehaviour
    {

        public Transform BeamStart;
        private Laser laser;
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
        private bool held = false;

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
                if (nvrHand.Inputs[laser.LaserButton].IsPressed)
                {
                    //Debug.LogError("Laser Controller " + controllerIndex.ToString() + "is pressed");
                    laser.enabledLaser(BeamStart, controllerIndex);
                    held = true;
                }
                else if (held)
                {
                    //Debug.LogError("Laser Controller is release" + controllerIndex.ToString());

                    laser.disabledLaser(controllerIndex);
                    held = false;
                }
            }
        }
    }
}
