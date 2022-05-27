using System;
using System.Diagnostics;

namespace Template {
    class FpsMonitor {
        public float value;
        public TimeSpan sample;
        private readonly Stopwatch sw;
        public int frames;
        public FpsMonitor()
        {
            this.sample = TimeSpan.FromSeconds(1);
            this.value = 0;
            this.frames = 0;
            this.sw = Stopwatch.StartNew();
        }
        public void Update()
        {
            this.frames++;
            if (sw.Elapsed > sample) {
                this.value = (float)(frames / sw.Elapsed.TotalSeconds);
                this.sw.Restart();
                this.frames = 0;
            }
        }
    }
}
