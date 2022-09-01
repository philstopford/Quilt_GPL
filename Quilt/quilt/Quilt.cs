using Eto;
using Eto.Forms;
using System;
using System.ComponentModel;

namespace Quilt;

public class QuiltApplication : Application
{
    private QuiltContext quiltContext;
    public QuiltApplication(Platform platform, QuiltContext pContext) : base(platform)
    {
        /*
        if (DateTime.Now > new DateTime(2021, 05, 28))
        {
            ErrorReporter.showMessage_OK("Contact phil.stopford@gmail.com", "Build expired!");
            Quit();
        }
        */
        quiltContext = pContext;
    }

    protected override void OnInitialized(EventArgs e)
    {
        MainForm = new MainForm(quiltContext);
        base.OnInitialized(e);
        MainForm.Show();
    }

    protected override void OnTerminating(CancelEventArgs e)
    {
        base.OnTerminating(e);

        var result = MessageBox.Show(MainForm, "Are you sure you want to quit?", MessageBoxButtons.YesNo, MessageBoxType.Question);
        if (result == DialogResult.No)
        {
            e.Cancel = true;
        }
    }
}