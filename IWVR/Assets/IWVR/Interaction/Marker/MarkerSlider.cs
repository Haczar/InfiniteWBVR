using UnityEngine;
using Valve.VR.InteractionSystem;

namespace IWVR
{
    [RequireComponent(typeof(Interactable))]
    public class MarkerSlider : MonoBehaviour
    {
        private void HandHoverUpdate(Hand hand)
        {
            if (hand.GetStandardInteractionButton())
            {
                proposedVal = getValueFromPos(hand.transform.position);

                if (proposedVal > 0)
                {
                    sizeSlider.value = proposedVal;
                }
            }
        }


        // Use this for initialization
        void Start()
        {
            sizeSlider = slider.GetComponent<UnityEngine.UI.Slider>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        float getValueFromPos(Vector3 pos)
        {
            if (pos.x > min.position.x && pos.x < max.position.x)
            {
                float genVal = (2* (pos.x - min.position.x))/max.position.x;

                return genVal;
            }
            else
            {
                return 0;
            }
        }

        UnityEngine.UI.Slider sizeSlider;

        private float proposedVal;

        public GameObject slider;
        public Transform min;
        public Transform max;
    }
}