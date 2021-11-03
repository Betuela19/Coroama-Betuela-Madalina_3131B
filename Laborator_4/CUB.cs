using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Laborator_4
{
    class CUB
    {

        //constante
        private const int NUMAR_CULORI = 6;
     
        private List<Vector3> vertex;
        private List<Color> culori;
        private bool visibility;
        private float lineWidth;

        // referinta privata 
        private Randomizer localRandomizer;

      
        // se citesc coordonatele cubului din fisierul text, 
        //  se va crea un vertex cu fiecare linie a fisierului, vertexul fiind adaugat in lista de vertexuri 
        // in total vom avea 36 de vertexuri
        public CUB(Randomizer r, string numefisier)
        {
            vertex = new List<Vector3>();

            using (StreamReader sr = new StreamReader(@"./../../coordonateCub.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var numere = line.Split(',');
                    int i = 0;
                    float[] coordonate = new float[10];
                    foreach (var nr in numere)
                    {
                        coordonate[i++] = Convert.ToInt32(nr);
                    }
                    vertex.Add(new Vector3(coordonate[0], coordonate[1], coordonate[2]));
                }
            }
  

        culori = new List<Color>();

            for (int i = 0; i < 6; i++)
            {
                culori.Add(r.GetRandomColor());
            }

            visibility = true;
            lineWidth = 10.0f;

            localRandomizer = r;
        }
        
   
        public void Draw()
        {
            if (visibility)
            {
                GL.LineWidth(lineWidth);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Begin(PrimitiveType.Triangles);
                for (int i = 0; i < 35; i = i + 3)
                {
                    GL.Color3(culori[i / 6]);
                    GL.Vertex3(vertex[i]);
                    GL.Vertex3(vertex[i + 1]);
                    GL.Vertex3(vertex[i + 2]);
                }
                GL.End();
            }
        }

        public void SchimbareCuloareFata1()
        {
            Color col = localRandomizer.GetRandomColor();
            culori[0] = col;
        }

        public void SchimbareCuloareFata2()
        {
            Color col = localRandomizer.GetRandomColor();
            culori[1] = col;
        }

        public void SchimbareCuloareFata3()
        {
            Color col = localRandomizer.GetRandomColor();
            culori[2] = col;
        }

        public void SchimbareCuloareFata4()
        {
            Color col = localRandomizer.GetRandomColor();
            culori[3] = col;
        }

        public void SchimbareCuloareFata5()
        {
            Color col = localRandomizer.GetRandomColor();
            culori[4] = col;
        }

        public void SchimbareCuloareFata6()
        {
            Color col = localRandomizer.GetRandomColor();
            culori[5] = col;
        }

        public void ToggleVisibility()
        {
            visibility = !visibility;
        }

        public void AfisareRBGValues()
        {
            Console.WriteLine("\n\tValori RGB");
            for (int i = 0; i < NUMAR_CULORI; i++)
            {
                Console.WriteLine($"\tCuloare [ {0} ]: Tip de data Color: { culori[i]}");
            }
        }

        public void DiscoMode()
        {
            List<Color> culoriRandomizer = new List<Color>();
            for (int i = 0; i < NUMAR_CULORI; i++)
            {
                culoriRandomizer.Add(localRandomizer.GetRandomColor());
            }

            culori = culoriRandomizer;
        }

        public void DrawVertexRGB()
        {
            if (visibility)
            {
                GL.LineWidth(lineWidth);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.Begin(PrimitiveType.Triangles);
                for (int i = 0; i < 35; i = i + 3)
                {
                    GL.Color3(localRandomizer.GetRandomColor());
                    GL.Vertex3(vertex[i]);

                    GL.Color3(localRandomizer.GetRandomColor());
                    GL.Vertex3(vertex[i + 1]);

                    GL.Color3(localRandomizer.GetRandomColor());
                    GL.Vertex3(vertex[i + 2]);
                }
                GL.End();
            }
        }

    }

}
