using UnityEngine;
using Valve.VR.InteractionSystem;

namespace IWVR
{
    [RequireComponent(typeof(Interactable))]
    public class Draggable : MonoBehaviour
    {
        private void HandHoverUpdate(Hand hand)
        {
            if (hand.GetStandardInteractionButton())
            {
                if (fixX ? true : checkX(hand.transform.position))
                {
                    if (fixY ? true : checkY(hand.transform.position))
                    {
                        SetThumbPosition(hand.transform.position);

                        SendMessage("OnDrag", Vector3.one - (transform.localPosition - minBound.localPosition) / transform.parent.GetComponent<BoxCollider>().size.x); //Hardcoded box collider size change back to norm later.
                    }
                }
            }
        }

        bool checkX(Vector3 pos)
        {
            if   (pos.x >= minBound.position.x && pos.x <= maxBound.position.x) { return true ; }
            else                                                                { return false; }
        }

        bool checkY(Vector3 pos)
        {
            if   (pos.y >= minBound.position.y && pos.y <= maxBound.position.y) { return true ; }
            else                                                                { return false; }
        }

        void SetThumbPosition(Vector3 newPosition)
        {
            Vector3 oriPosition = transform.position;

            transform.position = new Vector3(fixX ? oriPosition.x : newPosition.x, 
                                             fixY ? oriPosition.y : newPosition.y, 
                                                    oriPosition.z                 );
        }

        void SetDragPoint(Vector3 point)
        {
            point = (Vector3.one - point) * transform.parent.GetComponent<Collider>().bounds.size.x + transform.parent.GetComponent<Collider>().bounds.min;

            SetThumbPosition(point);
        }

        public bool fixX;
        public bool fixY;

        public Transform minBound;
        public Transform maxBound;
    }
}