using OpenTK;
namespace Template{
    class Camera {
        public Vector3 position, lookAt, upDirection, rightDirection;
        private Vector3 topLeft, topRight, bottomLeft, bottomRight;
        private Vector3 u, v;
        private float distance, ratio;


        public Camera()
        {
            this.position = new Vector3(0, 8, -35);
            this.lookAt = new Vector3(0, -2, 10).Normalized();
            this.upDirection = Vector3.UnitY;
            this.rightDirection = Vector3.Cross(this.lookAt, this.upDirection);
            this.distance = 4f;
            this.ratio = 1f;


            Vector3 center = position + distance * lookAt;
            this.topLeft = center + upDirection - ratio * rightDirection;
            this.topRight = center + upDirection + ratio * rightDirection;
            this.bottomLeft = center - upDirection - ratio * rightDirection;
            this.bottomRight = center - upDirection + ratio * rightDirection;

            this.u = topLeft - topRight;
            this.v = bottomRight - topRight;
        }
        public Camera(Vector3 position, Vector3 lookAt, Vector3 upDirection)
        {
            this.position = position;
            this.lookAt = lookAt;
            this.upDirection = upDirection;
        }

        public Ray ray(float a, float b)
        {
            return new Ray(this.position, this.topRight + a * u + b * v - this.position);
        }
    }
}
