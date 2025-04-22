using System;
using System.IO;
using Eto.Veldrid;
using Eto.Veldrid.Wpf;
using Veldrid;

namespace Quilt.WPF;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        string xmlFile = "";
        int graphicsMode = -1;
        bool dark = false;
        if (args.Length > 0)
        {
            dark = Array.IndexOf(args, "--dark") != -1;
            int graphicsModeIndex = Array.IndexOf(args, "--graphicsMode");
            if (graphicsModeIndex != -1)
            {
                switch (args[graphicsModeIndex + 1].ToLower())
                {
                    case "d3d11":
                        graphicsMode = (int)GraphicsBackend.Direct3D11;
                        break;
                    default:
                        graphicsMode = (int)GraphicsBackend.Vulkan;
                        break;
                }
            }

            foreach (var t in args)
            {
                string[] tokens = t.Split(new[] { '.' });
                string extension = tokens[^1];
                if (extension.ToUpper() == "QUILT" || extension.ToUpper() == "XML")
                {
                    xmlFile = t;
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

        var platform = new Eto.Wpf.Platform();
        platform.Add<VeldridSurface.IHandler>(() => new WpfVeldridSurfaceHandler());

        QuiltContext quiltContext = new(xmlFile, backend);
        // run application with our main form
        QuiltApplication pa = new(platform, quiltContext);
        if (dark)
        {
            //System.Windows.Application.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary { Source = new Uri("pack://application:,,,/DynamicAero2;component/Theme.xaml", UriKind.RelativeOrAbsolute) });
            //System.Windows.Application.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary { Source = new Uri("pack://application:,,,/DynamicAero2;component/Brushes/Dark.xaml", UriKind.RelativeOrAbsolute) });
        }
        pa.Run();
    }
}