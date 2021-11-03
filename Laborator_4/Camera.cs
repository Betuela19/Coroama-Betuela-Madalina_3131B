using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace Laborator_4
{
    class Camera
    {
        private Vector3 eye = new Vector3(50, 150, 50);
        private Vector3 target = new Vector3(0, 10, 5);
        private Vector3 up = new Vector3(0, 1, 0);

        //setarea camerei
        public void SetCamera()
        {
            Matrix4 camera = Matrix4.LookAt(eye, target, up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);
        }

        //rotire la dreapta
        public void RotateRight()
        {
            eye = new Vector3(eye.X + 3, eye.Y + 3, eye.Z - 3);
            SetCamera();
        }
        
        //rotire la stanga
        public void RotateLeft()
        {
            eye = new Vector3(eye.X - 3, eye.Y - 3, eye.Z + 3);
            SetCamera();
        }

        //rotire in sus
        public void RotateUp()
        {
            eye = new Vector3(eye.X, eye.Y + 10, eye.Z);
            SetCamera();
        }
        
        //rotire in jos
        public void RotateDown()
        {
            eye = new Vector3(eye.X, eye.Y - 10, eye.Z);
            SetCamera();
        }
    }
}
