using UnityEngine;

namespace NewtonVR
{
    public class LaserController : MonoBehaviour
    {
        public Transform BeamStart;

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
        private bool held = false;

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
            //if (laser != null)
            //{
            //    if (nvrHand.Inputs[laser.TeleportButton].IsPressed)
            //    {
            //        //Show Arc Teleport Preview
            //        validTeleportPosition = teleporter.UpdateArcTeleport(BeamStart, controllerIndex);
            //        held = true;
            //    }
            //    else if (held == true)
            //    {
            //        //Was held on the last frame. Kill teleport preview
            //        teleporter.HideArcTeleport(controllerIndex);
            //        held = false;

            //        if (validTeleportPosition != null)
            //        {
            //            teleporter.TeleportPlayer((Vector3)validTeleportPosition);
            //        }
            //    }
            //}
        }

        private void OnDestroy()
        {
            //if (laser != null)
            //{
            //    laser.HideArcTeleport(controllerIndex);
            //}
        }
    }
}
