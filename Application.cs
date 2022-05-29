using OpenTK.Input;
using OpenTK;
using System;

namespace Template
{
	class Application
	{
		RayTracer rayTracer;
		public Surface screen;
		private float lastWheelPrecise;
		private float usedWheelPrecise;
		// initialize
		public void Init()
		{
			rayTracer = new RayTracer();
			screen = rayTracer.screen;
		}
		// tick: renders one frame
		public void Tick()
		{
			
			rayTracer.Render();
		}

		public void HandleInput(KeyboardState keyboard, MouseState mouse)
		{
			float angle = 0.04f;
			if (keyboard.IsAnyKeyDown) {
				if (keyboard.IsKeyDown(Key.W)) {
					rayTracer.camera.Position += rayTracer.camera.LookAt * 0.1f;
                } 
				if (keyboard.IsKeyDown(Key.A)) {
					rayTracer.camera.Position += Vector3.Cross(rayTracer.camera.UpDirection, rayTracer.camera.LookAt) * 0.1f;
				} 
				if (keyboard.IsKeyDown(Key.S)) {
					rayTracer.camera.Position -= rayTracer.camera.LookAt * 0.1f;
				} 
				if (keyboard.IsKeyDown(Key.D)) {
					rayTracer.camera.Position -= Vector3.Cross(rayTracer.camera.UpDirection, rayTracer.camera.LookAt) * 0.1f;
				}
				if (keyboard.IsKeyDown(Key.Q)) {
					rayTracer.camera.LookAt = new Vector3(
						(float)(rayTracer.camera.LookAt.X * Math.Cos(angle) + rayTracer.camera.LookAt.Z * Math.Sin(angle)),
						rayTracer.camera.LookAt.Y,
						(float)(-rayTracer.camera.LookAt.X * (float)Math.Sin(angle) + rayTracer.camera.LookAt.Z * Math.Cos(angle)));
				}
				if (keyboard.IsKeyDown(Key.E)) {
					rayTracer.camera.LookAt = new Vector3(
						(float)(rayTracer.camera.LookAt.X * Math.Cos(angle) - rayTracer.camera.LookAt.Z * Math.Sin(angle)),
						rayTracer.camera.LookAt.Y,
						(float)(rayTracer.camera.LookAt.X * (float)Math.Sin(angle) + rayTracer.camera.LookAt.Z * Math.Cos(angle)));
				}
			}
			if (lastWheelPrecise != mouse.WheelPrecise) {
				usedWheelPrecise += Math.Min(mouse.WheelPrecise - lastWheelPrecise, 5);
				usedWheelPrecise = Math.Min(79, Math.Max(-70, usedWheelPrecise));
				rayTracer.camera.FOV = 80-usedWheelPrecise;
				lastWheelPrecise = mouse.WheelPrecise;
            }
		}
	}
}