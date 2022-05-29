using System;
using OpenTK;

namespace Template{
    class Camera {
        private Vector3 position, lookAt, upDirection, rightDirection;
        public Vector3 topLeft, topRight, bottomLeft, bottomRight;
        private Vector3 u, v;
        private float distance;
        private readonly float ratio;

        public float FOV {
            set {
                this.distance = this.ratio / (float)Math.Tan(value / 360f * Math.PI);
                UpdateScreen();
            }
        }
        public Vector3 Position { 
            get { return position; } 
            set { 
                position = value;
                UpdateScreen();
            } 
        }
        public Vector3 LookAt
        {
            get { return lookAt; }
            set
            {
                lookAt = value.Normalized();
                
                this.upDirection = Vector3.Cross(new Vector3(1, 0, 0), this.lookAt).Normalized();
                this.upDirection = Vector3.UnitY;
                this.rightDirection = Vector3.Cross(this.lookAt, this.upDirection).Normalized();
                UpdateScreen();
            }
        }
        public Vector3 UpDirection
        {
            get { return upDirection; }
        }
        public Camera(Vector3 position, Vector3 lookAt, float fov = 90f)
        {
            this.position = position;
            this.lookAt = lookAt.Normalized();
            this.upDirection = Vector3.Cross(new Vector3(0, 0, 1), this.lookAt).Normalized();
            this.rightDirection = Vector3.Cross(this.lookAt, this.upDirection).Normalized();
            this.ratio = 1f;
            FOV = fov;

            UpdateScreen();
        }

        public void UpdateScreen()
        {
            Vector3 center = position + distance * lookAt;
            this.topLeft = center + upDirection - ratio * rightDirection;
            this.topRight = center + upDirection + ratio * rightDirection;
            this.bottomLeft = center - upDirection - ratio * rightDirection;
            this.bottomRight = center - upDirection + ratio * rightDirection;

            this.u = topRight - topLeft;
            this.v = bottomLeft - topLeft;
        }

        public Ray Ray(float a, float b)
        {
            return new Ray(this.position, this.topLeft + a * u + b * v - this.position);
        }
    }
}
