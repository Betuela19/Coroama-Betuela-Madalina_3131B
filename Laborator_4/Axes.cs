using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Laborator_4
{
    class Axes
    {
      
        private bool visibility;
        private float lineWidth;

        public Axes()
        {
            visibility = true;
            lineWidth = 2.0f;
        }

        public void DrawAxes()
        {
            if (visibility)
            {
                GL.LineWidth(lineWidth);

                //  axa Ox
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.Red);
                GL.Vertex3(3, 3, 3);
                GL.Vertex3(50, 3, 3);

                //  axa Oy 
                GL.Color3(Color.Yellow);
                GL.Vertex3(3, 3, 3);
                GL.Vertex3(3, 50, 3);

                // axa Oz 
                GL.Color3(Color.Blue);
                GL.Vertex3(3, 3, 3);
                GL.Vertex3(3, 3, 50);

                GL.End();
            }
        }

        public void ToggleVisibility()
        {
            visibility = !visibility;
        }
    }
}
