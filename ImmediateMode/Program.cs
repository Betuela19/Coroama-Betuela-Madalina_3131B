
using ImmediateMode;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using System.IO;


namespace OpenTK_immediate_mode
{
    class ImmediateMode : GameWindow
    {
       
        private const int XYZ_SIZE = 75;
  
        // declarare culori implicite pentru triunghi
    
        private Color color1 = Color.GreenYellow;
        private Color color2 = Color.ForestGreen;
        private Color color3 = Color.PeachPuff;

        private readonly float[] coordonate = new float[10];

        // rotire camera cu ajutorul mouse-ului
    
        private Camera cam;

        public ImmediateMode() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";

            cam = new Camera();
        }

        /**Setare mediu OpenGL și încarcarea resurselor (dacă e necesar) - de exemplu culoarea de
           fundal a ferestrei 3D.
           Atenție! Acest cod se execută înainte de desenarea efectivă a scenei 3D. */
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.PaleGoldenrod);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            //citire fisier
            using (StreamReader sr = new StreamReader("coordonate.txt"))
            {
                int i = 0;
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    var num = linie.Split();
                    foreach (var nr in num)
                    {
                        coordonate[i++] = float.Parse(nr);
                    }
                }
            }

        }

        /**Inițierea afișării și setarea viewport-ului grafic. Metoda este invocată la redimensionarea
           ferestrei. Va fi invocată o dată și imediat după metoda ONLOAD()!
           Viewport-ul va fi dimensionat conform mărimii ferestrei active (cele 2 obiecte pot avea și mărimi 
           diferite). */
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1,300);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            cam.SetCamera();
        }

        /** Secțiunea pentru "game logic"/"business logic". Tot ce se execută în această secțiune va fi randat
            automat pe ecran în pasul următor - control utilizator, actualizarea poziției obiectelor, etc. */
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard[Key.Escape])
            {
                Exit();
            }

            // la apasarea unui set de taste se va modifica culoarea unui triunghi
    
            if (keyboard[Key.S])
            {
                color1 = Color.Red;
                color2 = Color.MediumVioletRed;
                color3 = Color.WhiteSmoke;
            }

            if (keyboard[Key.Space])
            {
                color1 = Color.Fuchsia;
                color2 = Color.Pink;
                color3 = Color.Purple;
            }

            //EGC#03-punct 8 
            //se modifica unghiul camerei cu ajutorul mouse-ului 
         
            if (mouse[MouseButton.Left] && mouse.X < 250)
            {
                cam.RotateLeft();
            }

            if (mouse[MouseButton.Left] && mouse.X > 250)
            {
                cam.RotateRight();
            }

            if (mouse[MouseButton.Middle])
            {
                cam.RotateDown();
            }

            if (mouse[MouseButton.Right])
            {
                cam.RotateUp();
            }
        }


        /** Secțiunea pentru randarea scenei 3D. Controlată de modulul logic din metoda ONUPDATEFRAME().
            Parametrul de intrare "e" conține informatii de timing pentru randare. */
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);




            DrawAxes();

            DrawObjects();



            // Se lucrează în modul DOUBLE BUFFERED - câtă vreme se afișează o imagine randată, o alta se randează în background apoi cele 2 sunt schimbate...
            SwapBuffers();
        }

        private void DrawAxes()
        {

            //GL.LineWidth(3.0f);
             //Ox
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);
            GL.End();

            //Oy
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0); ;
            GL.End();

            //Oz
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
            GL.End();
        }

        private void DrawObjects()
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(color1);
            GL.Vertex3(coordonate[0], coordonate[1], coordonate[2]);

            GL.Color3(color2);
            GL.Vertex3(coordonate[3], coordonate[4], coordonate[5]);

            GL.Color3(color3);
            GL.Vertex3(coordonate[6], coordonate[7], coordonate[8]);

            GL.End();
        }


        [STAThread]
        static void Main(string[] args)
        {

            /**Utilizarea cuvântului-cheie "using" va permite dealocarea memoriei o dată ce obiectul nu mai este
               în uz (vezi metoda "Dispose()").
               Metoda "Run()" specifică cerința noastră de a avea 30 de evenimente de tip UpdateFrame per secundă
               și un număr nelimitat de evenimente de tip randare 3D per secundă (maximul suportat de subsistemul
               grafic). Asta nu înseamnă că vor primi garantat respectivele valori!!!
               Ideal ar fi ca după fiecare UpdateFrame să avem si un RenderFrame astfel încât toate obiectele generate
               în scena 3D să fie actualizate fără pierderi (desincronizări între logica aplicației și imaginea randată
               în final pe ecran). */

            using (ImmediateMode example = new ImmediateMode())
            {
                example.Run(30.0, 0.0);
            }
        }
    }

}

