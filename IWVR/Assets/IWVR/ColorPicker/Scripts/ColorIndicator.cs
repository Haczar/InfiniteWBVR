using UnityEngine;

namespace IWVR
{
    public class ColorIndicator : MonoBehaviour
    {
        public UnityEngine.Color color;

        public GameObject marker;

        private void Start()
        {
            HSBColor colorThing = HSBColor.FromColor(GetComponent<Renderer>().sharedMaterial.GetColor("_Color"));

            transform.parent.BroadcastMessage("SetColor", color);

            float value = 255 - colorThing.b;

            color = UnityEngine.Color.HSVToRGB(colorThing.h, colorThing.s, value);

            UpdateMarkerColor();
        }

        void ApplyColor()
        {
            GetComponent<Renderer>().sharedMaterial.SetColor("_Color", color);

            transform.parent.BroadcastMessage("OnColorChange", color, SendMessageOptions.DontRequireReceiver);

            UpdateMarkerColor();
        }

        void SetHue(float hue)
        {
            float h, s, v;

            Color.RGBToHSV(color, out h, out s, out v);

            color = Color.HSVToRGB(hue, s, v);

            ApplyColor();
        }

        void SetSaturationBrightness(Vector2 sb)
        {
            float h, s, v, b;

            Color.RGBToHSV(color, out h, out s, out v);

            s = sb.x;
            b = sb.y;

            v = b;

            color = Color.HSVToRGB(h, s, v);

            ApplyColor();
        }

        private void UpdateMarkerColor()
        {
            marker.GetComponent<MarkerInteraction>().color = this.color;
        }
    }
}

