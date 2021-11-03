using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Laborator_4
{
    class Window3D : GameWindow
    {
        private KeyboardState previousKeyboard;
        private Randomizer random;
        Color DEFAULT_BKG_COLOR = Color.LightBlue;

        //declarare obiect de tip cub si axele de coordonate
        private Camera cam;
        private CUB cub;
        private Axes axe;

        public Window3D() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            random = new Randomizer();
            cub = new CUB(random, @"./../../coordonateCub.txt");
            cam = new Camera();
            axe = new Axes();

            displayHelp();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(DEFAULT_BKG_COLOR);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // setare viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // setare perspective 
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 250);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // setarea camerei
            Matrix4 eye = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref eye);
        }

        
        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //LOGIC CODE

            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();
            
            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }
            
            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                displayHelp();
            }
            
            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                GL.ClearColor(random.GetRandomColor());
            }
            
            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                GL.ClearColor(DEFAULT_BKG_COLOR);
            }

            if (currentKeyboard[Key.I] && !previousKeyboard[Key.I])
            {
                cub.SchimbareCuloareFata1();
            }

            if (currentKeyboard[Key.J] && !previousKeyboard[Key.J])
            {
                cub.SchimbareCuloareFata2();
            }

            if (currentKeyboard[Key.K] && !previousKeyboard[Key.K])
            {
                cub.SchimbareCuloareFata3();
            }

            if (currentKeyboard[Key.L] && !previousKeyboard[Key.L])
            {
                cub.SchimbareCuloareFata4();
            }

            if (currentKeyboard[Key.M] && !previousKeyboard[Key.M])
            {
                cub.SchimbareCuloareFata5();
            }

            if (currentKeyboard[Key.N] && !previousKeyboard[Key.N])
            {
                cub.SchimbareCuloareFata6();
            }

            if (currentKeyboard[Key.C] && !previousKeyboard[Key.C])
            {
                cub.ToggleVisibility();
            }

            if (currentKeyboard[Key.G] && !previousKeyboard[Key.G])
            {
                cub.AfisareRBGValues();
            }

           

            if (currentKeyboard[Key.W])
            {
                cam.RotateDown();
            }

            if (currentKeyboard[Key.S])
            {
                cam.RotateUp();
            }

            if (currentKeyboard[Key.A])
            {
                cam.RotateLeft();
            }

            if (currentKeyboard[Key.D])
            {
                cam.RotateRight();
            }

            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
            {
                axe.ToggleVisibility();
            }

            previousKeyboard = currentKeyboard;
            // sfarsit cod logic 
        }

       
        // laborator 4 - punctul 2
   
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // RENDER CODE 
            //Desenare cub
            cub.Draw();

            //Desenare axe
            axe.DrawAxes();

            SwapBuffers();
        }

        
        //MENIU
        private void displayHelp()
        {
            Console.WriteLine("              MENIU");
            Console.WriteLine("H - meniu de ajutor ");
            Console.WriteLine("ESC - parasire aplicatie");
            Console.WriteLine("B - schimbare culoare de fundal");
            Console.WriteLine("R - resetare culoare de fundal");
            Console.WriteLine("C - visibility pentru cub");
            Console.WriteLine("V - visibility pentru axe");
            Console.WriteLine("G - afisare valori RGB pentru fiecare fata a cubului");
            Console.WriteLine("I, J, K, L, M, N - schimbarea culorilor fetelor cubului");
            Console.WriteLine("W, A, S, D - optiuni pentru rotire a camerei 3D");
        }
    }
}
