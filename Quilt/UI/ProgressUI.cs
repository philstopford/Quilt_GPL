using Eto.Forms;

namespace Quilt;

public partial class MainForm
{
    private void pDelegates()
    {
        commonVars.storage.viewportLoad = pSetViewportCamera;
        commonVars.storage.viewportSave = pGetViewportCamera;
        commonVars.storage.preLoadUI = pPreLoad_Storage;
        commonVars.storage.postLoadUI = pLoadOK_Storage;

        commonVars.stitcher.indeterminateQuiltUI = pIndeterminateQuiltUI;
        commonVars.stitcher.updateUIProgress = pUpdateProgressBar;
        commonVars.stitcher.updateUIStatus = pUpdateProgressLabel;
        commonVars.stitcher.generatingPatternUI = pGeneratingPatternUI;
        commonVars.stitcher.stitchingQuiltUI = pStitchingQuiltUI;
        commonVars.stitcher.viewport = pUpdateViewport_2;
        commonVars.stitcher.doneQuiltUI = pDoneQuiltUI;

        commonVars.storage.updateUIProgress = pUpdateProgressBar;
        commonVars.storage.updateUIstatus = pUpdateProgressLabel;
    }

    private void pPreLoad_Storage()
    {
        Application.Instance.Invoke(() =>
        {
            pSetUI(false);
            progressBar.Indeterminate = true;
            UIFreeze = true;
        });
    }

    private void pLoadOK_Storage(string loadOK)
    {
        Application.Instance.Invoke(() =>
        {
            if (loadOK != "")
            {
                commonVars.projectFileName = loadOK;
                Title = commonVars.titleText + " - " + commonVars.projectFileName;
                commonVars.stitcher.reset(true);
                commonVars.stitcher.setPadding(commonVars.storage.getPadding());
                commonVars.stitcher.setShowInput(commonVars.storage.getShowInput());
                commonVars.stitcher.addPatternElements(commonVars.storage.getElements());
            }
            else
            {
                commonVars.reset();
                Title = commonVars.titleText;
                viewPort.reset();

            }
            num_padding.Value = commonVars.stitcher.getPadding();
            checkBox_showInput.Checked = commonVars.stitcher.getShowInput() == 1;
            listBox_entries.SelectedIndex = 0;

            progressBar.Indeterminate = false;
            UIFreeze = false;

            pSetUI(true);
            pUpdatePatternElementUI();
        });
    }

    private void pUpdateProgressBar(double val)
    {
        Application.Instance.Invoke(() =>
        {
            progressBar.Indeterminate = false;
            if (progressBar.MaxValue < 100)
            {
                progressBar.MaxValue = 100;
            }
            if (val > 1)
            {
                val = 1;
            }
            if (val < 0)
            {
                val = 0;
            }
            progressBar.Value = (int)(val * progressBar.MaxValue);
        });
    }

    private void pUpdateProgressBar(int count, int max)
    {
        double val = (double)count / max;
        pUpdateProgressBar(val);
    }

    private void pUpdateProgressLabel(string text)
    {
        if (text == "")
        {
            return;
        }
        Application.Instance.Invoke(() => progressLabel.Text = text);
    }

    private void pIndeterminateQuiltUI(string tooltipText, string labelText)
    {
        Application.Instance.Invoke(() =>
        {
            pSetUI(false);
            progressBar.Indeterminate = true;
            progressBar.ToolTip = tooltipText;
            progressLabel.Text = labelText;
        });

    }

    private void pGeneratingPatternUI()
    {
        Application.Instance.Invoke(() =>
        {
            progressBar.Indeterminate = false;
            progressBar.Value = 0;
        });
    }

    private void pStitchingQuiltUI()
    {
        Application.Instance.Invoke(() =>
        {
            progressBar.MaxValue = commonVars.stitcher.previewShapes.Length;
            progressBar.Value = 0;
            progressBar.ToolTip = "Stitching quilt";
            progressLabel.Text = "Stitching";
        });
    }

    private void pDoneQuiltUI(string text)
    {
        Application.Instance.Invoke(() =>
        {
            num_patNum.MaxValue = commonVars.stitcher.getPatternCount() - 1;
            progressBar.Indeterminate = false;
            progressBar.Value = progressBar.MaxValue;
            progressBar.ToolTip = "";
            pUpdateProgressLabel(text);
            pSetUI(true);
            pUpdatePatternElementUI();
        });
    }

}