using Eto.Forms;

namespace Quilt
{
    public partial class MainForm : Form
    {
        void delegates()
        {
            commonVars.storage.viewportLoad = setViewportCamera;
            commonVars.storage.viewportSave = getViewportCamera;
            commonVars.storage.preLoadUI = preLoad_Storage;
            commonVars.storage.postLoadUI = loadOK_Storage;

            commonVars.stitcher.indeterminateQuiltUI = indeterminateQuiltUI;
            commonVars.stitcher.updateUIProgress = updateProgressBar;
            commonVars.stitcher.updateUIStatus = updateProgressLabel;
            commonVars.stitcher.generatingPatternUI = generatingPatternUI;
            commonVars.stitcher.stitchingQuiltUI = stitchingQuiltUI;
            commonVars.stitcher.viewport = updateViewport_2;
            commonVars.stitcher.doneQuiltUI = doneQuiltUI;
        }

        void preLoad_Storage()
        {
            Application.Instance.Invoke(() =>
            {
                setUI(false);
                progressBar.Indeterminate = true;
                UIFreeze = true;
            });
        }

        void loadOK_Storage(string loadOK)
        {
            Application.Instance.Invoke(() =>
            {
                if (loadOK != "")
                {
                    //updateStatusLine("Project loaded successfully");
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
                    //updateStatusLine("Project loading failed.");

                }
                num_padding.Value = commonVars.stitcher.getPadding();
                checkBox_showInput.Checked = (commonVars.stitcher.getShowInput() == 1);
                listBox_entries.SelectedIndex = 0;

                progressBar.Indeterminate = false;
                UIFreeze = false;

                setUI(true);
                updatePatternElementUI();
            });
        }

        void updateProgressBar(double val)
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

        void updateProgressBar(int count, int max)
        {
            double val = (double)count / max;
            updateProgressBar(val);
        }

        void updateProgressLabel(string text)
        {
            if (text == "")
            {
                return;
            }
            Application.Instance.Invoke(() => progressLabel.Text = text);
        }

        void indeterminateQuiltUI(string tooltipText, string labelText)
        {
            Application.Instance.Invoke(() =>
            {
                setUI(false);
                progressBar.Indeterminate = true;
                progressBar.ToolTip = tooltipText;
                progressLabel.Text = labelText;
            });

        }

        void generatingPatternUI()
        {
            Application.Instance.Invoke(() =>
            {
                progressBar.Indeterminate = false;
                progressBar.Value = 0;
            });
        }

        void stitchingQuiltUI()
        {
            Application.Instance.Invoke(() =>
            {
                progressBar.MaxValue = commonVars.stitcher.previewShapes.Length;
                progressBar.Value = 0;
                progressBar.ToolTip = "Stitching quilt";
                progressLabel.Text = "Stitching";
            });
        }

        void doneQuiltUI(string text)
        {
            Application.Instance.Invoke(() =>
            {
                num_patNum.MaxValue = commonVars.stitcher.getPatternCount() - 1;
                progressBar.Indeterminate = false;
                progressBar.Value = progressBar.MaxValue;
                progressBar.ToolTip = "";
                updateProgressLabel(text);
                setUI(true);
            });
        }

    }
}
