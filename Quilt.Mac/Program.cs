using Eto.Veldrid;
using Eto.Veldrid.Mac;
using System;
using System.IO;
using Veldrid;

namespace Quilt.Mac
{
    public static class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            string xmlFile = "";
            int graphicsMode = -1;
            if (args.Length > 0)
            {
                int graphicsModeIndex = Array.IndexOf(args, "--graphicsMode");
                if (graphicsModeIndex != -1)
                {
                    switch (args[graphicsModeIndex + 1].ToLower())
                    {
                        case "opengl":
                            graphicsMode = (int)GraphicsBackend.OpenGL;
                            break;
                        case "vulkan":
                            graphicsMode = (int)GraphicsBackend.Vulkan;
                            break;
                        default:
                            graphicsMode = (int)GraphicsBackend.Metal;
                            break;
                    }
                }

                for (int i = 0; i < args.Length; i++)
                {
                    string[] tokens = args[i].Split(new[] { '.' });
                    string extension = tokens[^1];
                    if ((extension.ToUpper() == "QUILT") || (extension.ToUpper() == "XML"))
                    {
                        xmlFile = args[i];
                    }
                }
            }

            // Does file exist?
            if (xmlFile != "")
            {
                if (!File.Exists(xmlFile))
                {
                    xmlFile = "";
                }
            }

            GraphicsBackend backend = VeldridSurface.PreferredBackend;

            if (graphicsMode != -1)
            {
                try
                {
                    backend = (GraphicsBackend)graphicsMode;
                }
                catch (Exception)
                {
                    // avoid changing the backend from the preferred case.
                }
            }

            var platform = new Eto.Mac.Platform();
            platform.Add<VeldridSurface.IHandler>(() => new MacVeldridSurfaceHandler());

            QuiltContext quiltContext = new QuiltContext(xmlFile, backend);
            // run application with our main form
            QuiltApplication pa = new QuiltApplication(platform, quiltContext);
            pa.Run();
        }
    }
}
