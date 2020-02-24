using Eto.Drawing;
using Eto.Forms;
using System;
using System.ComponentModel;

namespace Quilt
{
    public class CreditsScreen : Form
    {
        // public System.Diagnostics.Process p = new System.Diagnostics.Process();

        RichTextArea textBox_credits;

        public CreditsScreen(Form parent, string textToDisplay)
        {
            Title = CentralProperties.productName + " " + CentralProperties.version;
            PixelLayout content = new PixelLayout();
            Content = content;

            Size = new Size(600, 430);

            textBox_credits = new RichTextArea();
            try
            {
                textBox_credits.Font = SystemFonts.Default(13 * 0.66f);
            }
            catch (Exception)
            {

            }
            textBox_credits.Size = new Size(550, 253);
            textBox_credits.Wrap = true;
            textBox_credits.ReadOnly = true;
            textBox_credits.Text = textToDisplay;

            Panel textArea = new Panel();
            textArea.Size = textBox_credits.Size;
            textArea.Content = textBox_credits;
            content.Add(textArea, 15, 15);

            Resizable = false;
            Maximizable = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        public override void Close()
        {
            this.Visible = false;
        }

        /*
		private void linkClicked(object sender, EventArgs e)
		{
			// Call Process.Start method to open a browser
			// with link text as URL.
			p = System.Diagnostics.Process.Start("IExplore.exe", e.LinkText);
		}
		*/
    }
}
