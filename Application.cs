using OpenTK.Input;
using OpenTK;
using System;

namespace Template
{
	/// <summary>
	/// This class connects the logic from the input, the raytracer and the screen object together.
	/// </summary>
	class Application
	{
		RayTracer rayTracer;
		public Surface screen;
		private float lastWheelPrecise;
		private float usedWheelPrecise;
		/// <summary>
		/// Method called once to initialise the application
		/// </summary>
		public void Init()
		{
			rayTracer = new RayTracer();
			screen = rayTracer.screen;
		}
		/// <summary>
		/// Method that get's called once per frame used for setting the pixel values
		/// </summary>
		public void Tick()
		{
			rayTracer.Render();
		}
		/// <summary>
		/// Method that gets the input values and passes the used values to the raytracer
		/// </summary>
		/// <param name="keyboard">The current keyboard state</param>
		/// <param name="mouse">The current mouse state</param>
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
				//Keep the usedWheelprecies between 75 and -70, so the FOV will be between 5 and 150
				usedWheelPrecise = Math.Min(75, Math.Max(-70, usedWheelPrecise)); 
				rayTracer.camera.FOV = 80-usedWheelPrecise;
				lastWheelPrecise = mouse.WheelPrecise;
            }
		}
	}
}