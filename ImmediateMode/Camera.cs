using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace ImmediateMode
{
    class Camera
    {

        private Vector3 eye = new Vector3(50, 150, 50);
        private Vector3 target = new Vector3(0, 10, 5);
        private Vector3 up = new Vector3(0, 1, 0);

        public void SetCamera()
        {
            Matrix4 camera = Matrix4.LookAt(eye, target, up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);
        }

        public void RotateRight()
        {
            eye = new Vector3(eye.X + 3, eye.Y + 3, eye.Z - 3);
            SetCamera();
        }

        public void RotateLeft()
        {
            eye = new Vector3(eye.X - 3, eye.Y - 3, eye.Z + 3);
            SetCamera();
        }

        public void RotateUp()
        {
            eye = new Vector3(eye.X, eye.Y + 10, eye.Z);
            SetCamera();
        }

        public void RotateDown()
        {
            eye = new Vector3(eye.X, eye.Y - 10, eye.Z);
            SetCamera();
        }
    }
}
