using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenTKSandbox
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using (SandboxWindow window = new SandboxWindow())
			{
				window.Run();
			}
		}
	}
}
