using UnityEngine;
using System.Collections;
using Edwon.VR.Input;
using Edwon.VR.Gesture;

namespace Edwon.VR.Gesture
{
    public class GestureList : MonoBehaviour
    {

        VRGestureRig rig;
        IInput input;

        Transform playerHead;
        Transform playerHandL;
        Transform playerHandR;

        private string lastGesture;

        void Start()
        {
            rig = FindObjectOfType<VRGestureRig>();
            if (rig == null)
            {
                Debug.Log("there is no VRGestureRig in the scene, please add one");
            }

            playerHead = rig.head;
            playerHandR = rig.handRight;
            playerHandL = rig.handLeft;

            input = rig.GetInput(rig.mainHand);
        }

        void OnEnable()
        {
            Debug.Log("now working");
            GestureRecognizer.GestureDetectedEvent += OnGestureDetected;
        }

        void OnDisable()
        {
            Debug.Log("now not working");
            GestureRecognizer.GestureDetectedEvent -= OnGestureDetected;
        }

        public void resetGesture()
        {
            lastGesture = null;
        }

        public string getGesture()
        {
            return lastGesture;
        }

        void OnGestureDetected(string gestureName, double confidence, Handedness hand, bool isDouble)
        {
            string confidenceString = confidence.ToString().Substring(0, 4);
            //Debug.Log("detected gesture: " + gestureName + " with confidence: " + confidenceString);
            if (gestureName == "yes" || gestureName == "no")
            {
                lastGesture = gestureName;
            }
            return;

            Debug.Log(lastGesture);

            switch (gestureName)
            {
                case "Yes":
                    Debug.Log("Yes Works!");
                    break;
                case "No":
                    Debug.Log("No Works!");
                    break;
                case "Pat":
                    Debug.Log("Pat Works!");
                    break;
                case "Rude":
                    Debug.Log("Rude Works!");
                    break;
            }
        }
    }
}