using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenTKSandbox
{
	public class SandboxWindow : GameWindow
	{
		private Stopwatch continuousWatch = new Stopwatch();
		private Stopwatch watch = new Stopwatch();
		private float globalTimer = 0.0f;
		private float deltaTime = 0.0f;
		private float spikeDetect = 0.0f;

		public SandboxWindow() : base(800, 600, new GraphicsMode(GraphicsMode.Default.ColorFormat, 24, 0, 16), "OpenTK Sandbox", GameWindowFlags.Default)
		{
			this.VSync = VSyncMode.Adaptive;
		}
		
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(
				this.ClientRectangle.X, 
				this.ClientRectangle.Y, 
				this.ClientRectangle.Width, 
				this.ClientRectangle.Height);

			Matrix4 projection = Matrix4.CreateOrthographicOffCenter(
				0.0f, this.ClientSize.Width, 
				this.ClientSize.Height, 0.0f, 
				0.0f, 1.0f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projection);
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			this.deltaTime = (float)this.watch.Elapsed.TotalSeconds;
			this.continuousWatch.Start();
			this.watch.Restart();
			this.globalTimer += this.deltaTime;

			// Simulate updating something
			float dummyValue = 1.0f;
			Random rnd = new Random(1);
			for (int i = 0; i < 10000; i++)
			{
				dummyValue *= (0.5f + (float)rnd.NextDouble());
			}

			if (this.Keyboard[Key.Escape])
				this.Exit();
		}
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			this.spikeDetect += (float)Math.Max(0.0f, this.deltaTime - 0.017f) / 0.016f;
			this.spikeDetect *= 0.95f;

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			
			// Do some busy work on the GPU
			for (int i = 0; i < 500; i++)
			{
				GL.Color3(
					0.25f + 0.25f * (float)(i % 217) / 217.0f, 
					0.25f + 0.25f * (float)(i % 91) / 91.0f, 
					0.25f + 0.25f * (float)(i % 151) / 151.0f);
				DrawRect(10 + i % 690, 300 + i / 690, 100, 100);
			}

			float spikeWarning = Math.Min(Math.Max(0.0f, this.spikeDetect), 1.0f);
			GL.Color3(1.0f, 1.0f - spikeWarning, 1.0f - spikeWarning);
			DrawRect(-10 - 100 + (300.0f * this.globalTimer) % (this.ClientSize.Width + 20 + 200), 10, 100, 100);
			DrawRect(-10 - 100 + (300.0f * this.globalTimer) % (this.ClientSize.Width + 20 + 200), 120, 100, 100);
			DrawRect(-10 - 100 + (300.0f * this.globalTimer) % (this.ClientSize.Width + 20 + 200), 230, 100, 100);
			DrawRect(-10 - 100 + (300.0f * this.globalTimer) % (this.ClientSize.Width + 20 + 200), 340, 100, 100);
			DrawRect(-10 - 100 + (300.0f * (float)this.continuousWatch.Elapsed.TotalSeconds) % (this.ClientSize.Width + 20 + 200), 450, 100, 100);

			this.SwapBuffers();
		}

		private static void DrawRect(float x, float y, float width, float height)
		{
			GL.Begin(PrimitiveType.Quads);

			GL.Vertex2(x, y);
			GL.Vertex2(x, y + height);
			GL.Vertex2(x + width, y + height);
			GL.Vertex2(x + width, y);

			GL.End();
		}
	}
}
