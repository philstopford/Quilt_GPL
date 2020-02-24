using Eto.Forms;
using Eto.Veldrid;
using Eto.Veldrid.Gtk;
using OpenTK;
using Quilt;
using System;
using System.IO;
using Veldrid;

namespace Quilt.Gtk
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
                        case "metal":
                            graphicsMode = (int)GraphicsBackend.Metal;
                            break;
                        case "d3d11":
                            graphicsMode = (int)GraphicsBackend.Direct3D11;
                            break;
                        case "vulkan":
                        default:
                            graphicsMode = (int)GraphicsBackend.Vulkan;
                            break;
                    }
                }

                for (int i = 0; i < args.Length; i++)
                {
                    // Extract XML file.
                    string extension = args[i].Substring(args[i].Length - 3, 3).ToUpper();
                    if ((extension.ToUpper() == "QUILT") || (extension.ToUpper() == "XML"))
                    {
                        xmlFile = args[i];
                        break;
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

            if (backend == GraphicsBackend.OpenGL)
            {
                Toolkit.Init(new ToolkitOptions { Backend = PlatformBackend.PreferNative });
            }

            var platform = new Eto.GtkSharp.Platform();
            platform.Add<VeldridSurface.IHandler>(() => new GtkVeldridSurfaceHandler());

            QuiltContext quiltContext = new QuiltContext(xmlFile, backend);
            // run application with our main form
            QuiltApplication pa = new QuiltApplication(platform, quiltContext);
            pa.Run();
        }
    }
}
