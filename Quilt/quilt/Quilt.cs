using Eto;
using Eto.Forms;
using System;
using System.ComponentModel;

namespace Quilt
{
    public class QuiltApplication : Application
    {
        bool doPrompts;

        QuiltContext quiltContext;
        public QuiltApplication(Platform platform, QuiltContext pContext) : base(platform)
        {
            quiltContext = pContext;
        }

        protected override void OnInitialized(EventArgs e)
        {
            MainForm = new MainForm(ref doPrompts, quiltContext);
            base.OnInitialized(e);
            MainForm.Show();
        }

        protected override void OnTerminating(CancelEventArgs e)
        {
            base.OnTerminating(e);

            if (doPrompts)
            {
                var result = MessageBox.Show(MainForm, "Are you sure you want to quit?", MessageBoxButtons.YesNo, MessageBoxType.Question);
                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}
