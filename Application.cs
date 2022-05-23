namespace Template
{
	class Application
	{
		RayTracer rayTracer;
		public Surface screen;
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
	}
}