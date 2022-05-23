using System;
using OpenTK;

namespace Template {
    class Light {
        private Vector3 location;
        private Color intensity;
        public Light(Vector3 location, Color intensity)
        {
            this.location = location;
            this.intensity = intensity;
        }

        public Vector3 Location { get { return location; } }
        public Color Intensity { get { return intensity; } }
    }
}
