using Eto.Drawing;
using Eto.Forms;
using System;
using shapeEngine;

namespace Quilt;

public partial class MainForm
{
    private void pDoColors()
    {
        Color lyr1Color = Color.FromArgb(quiltContext.colors.subshape1_Color.R,
            quiltContext.colors.subshape1_Color.G,
            quiltContext.colors.subshape1_Color.B);
        num_layer_subshape_minhl.TextColor = lyr1Color;
        num_layer_subshape_minvl.TextColor = lyr1Color;
        num_layer_subshape_minho.TextColor = lyr1Color;
        num_layer_subshape_minvo.TextColor = lyr1Color;

        num_layer_subshape_incHL.TextColor = lyr1Color;
        num_layer_subshape_incVL.TextColor = lyr1Color;
        num_layer_subshape_incHO.TextColor = lyr1Color;
        num_layer_subshape_incVO.TextColor = lyr1Color;

        num_layer_subshape_stepsHL.TextColor = lyr1Color;
        num_layer_subshape_stepsVL.TextColor = lyr1Color;
        num_layer_subshape_stepsHO.TextColor = lyr1Color;
        num_layer_subshape_stepsVO.TextColor = lyr1Color;

        comboBox_tipLocations.TextColor = lyr1Color;

        Color lyr2Color = Color.FromArgb(quiltContext.colors.subshape2_Color.R,
            quiltContext.colors.subshape2_Color.G,
            quiltContext.colors.subshape2_Color.B);
        num_layer_subshape2_minhl.TextColor = lyr2Color;
        num_layer_subshape2_minvl.TextColor = lyr2Color;
        num_layer_subshape2_minho.TextColor = lyr2Color;
        num_layer_subshape2_minvo.TextColor = lyr2Color;

        num_layer_subshape2_incHL.TextColor = lyr2Color;
        num_layer_subshape2_incVL.TextColor = lyr2Color;
        num_layer_subshape2_incHO.TextColor = lyr2Color;
        num_layer_subshape2_incVO.TextColor = lyr2Color;

        num_layer_subshape2_stepsHL.TextColor = lyr2Color;
        num_layer_subshape2_stepsVL.TextColor = lyr2Color;
        num_layer_subshape2_stepsHO.TextColor = lyr2Color;
        num_layer_subshape2_stepsVO.TextColor = lyr2Color;

        comboBox_tipLocations2.TextColor = lyr2Color;

        Color lyr3Color = Color.FromArgb(quiltContext.colors.subshape3_Color.R,
            quiltContext.colors.subshape3_Color.G,
            quiltContext.colors.subshape3_Color.B);
        num_layer_subshape3_minhl.TextColor = lyr3Color;
        num_layer_subshape3_minvl.TextColor = lyr3Color;
        num_layer_subshape3_minho.TextColor = lyr3Color;
        num_layer_subshape3_minvo.TextColor = lyr3Color;

        num_layer_subshape3_incHL.TextColor = lyr3Color;
        num_layer_subshape3_incVL.TextColor = lyr3Color;
        num_layer_subshape3_incHO.TextColor = lyr3Color;
        num_layer_subshape3_incVO.TextColor = lyr3Color;

        num_layer_subshape3_stepsHL.TextColor = lyr3Color;
        num_layer_subshape3_stepsVL.TextColor = lyr3Color;
        num_layer_subshape3_stepsHO.TextColor = lyr3Color;
        num_layer_subshape3_stepsVO.TextColor = lyr3Color;

        comboBox_tipLocations3.TextColor = lyr3Color;

        ovpSettings.minorGridColor = Color.FromArgb(quiltContext.colors.minor_Color.toArgb());
        ovpSettings.majorGridColor = Color.FromArgb(quiltContext.colors.major_Color.toArgb());
        ovpSettings.axisColor = Color.FromArgb(quiltContext.colors.axis_Color.toArgb());
        ovpSettings.backColor = Color.FromArgb(quiltContext.colors.background_Color.toArgb());
        ovpSettings.reset(false);

    }

    private void pDoPatternElementUI(object sender, EventArgs e)
    {
        bool updateUI = false;
        try
        {
            if ((DropDown)sender == comboBox_patternElementShape)
            {
                updateUI = true;
            }
        }
        catch
        {
            // ignored
        }
        pDoPatternElementUI(updateUI);
        pasteLayer.Enabled = commonVars.stitcher.isCopySet();
        revertSim.Enabled = commonVars.projectFileName != "";
    }
    
    private void pDoPatternElementUI(bool updateUI = false)
    {
        if (UIFreeze)
        {
            return;
        }
        pDoPatternElementUI(0, updateUI);
        pUpdateLBContextMenu();
    }

    private void pDoPatternElementUI_num(int pattern, int index, string shapeString)
    {
        groupBox_properties.Content = groupBox_subShapes_table;
        comboBox_patternElementShape.Visible = true;

        pSwitchUIGadgets_off();

        pRefState_subShape1(pattern, index);
            
        // Any configuration beyond the first couple requires a second shape to be defined so we need to display that part of the interface.
        if (shapeString != "none" && shapeString != "rectangle" && shapeString != "text")
        {
            // Let's display the subshape 2 section if a shape configuration is chosen that requires it.

            pRefState_subShape2(pattern, index);
                
            if (shapeString == "S")
            {
                pRefState_subShape3(pattern, index);
                    
                commonVars.subshapes.Clear();
                commonVars.subshapes.Add("1");
                commonVars.subshapes.Add("2");
                commonVars.subshapes.Add("3");
            }
            else
            {
                commonVars.subshapes.Clear();
                commonVars.subshapes.Add("1");
                commonVars.subshapes.Add("2");
            }
        }
        else
        {
            commonVars.subshapes.Clear();
            commonVars.subshapes.Add("1");
        }
        
        pRefState_tips(pattern, index);
    }

    private void pDoPatternElementUI(int pattern, bool updateUI = false, bool doPreview = true)
    {
        if (UIFreeze)
        {
            return;
        }

        if (suspendBuild)
        {
            pUpdateProgressLabel("Build suspended.");
        }

        UIFreeze = true;

        int index = listBox_entries.SelectedIndex;

        if (index == -1)
        {
            UIFreeze = false;
            pClearPatternElementUI();
            comboBox_patternElementShape.Visible = true;
            return;
        }

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setString(PatternElement.properties_s.name, commonVars.stitcher.patternElementNames[index]);

        comboBox_patternElementShape.Visible = true;

        commonVars.stitcher.getPatternElement(patternIndex: pattern, index).setInt(PatternElement.properties_i.shapeIndex, comboBox_patternElementShape.SelectedIndex);

        if (commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.shapeIndex) == (int)CentralProperties.shapeNames.none)
        {

            if (doPreview)
            {
                pDrawPreviewPanelHandler();
            }
            UIFreeze = false;
            pClearPatternElementUI();
            return;
        }

        groupBox_properties.Visible = true;

        string shapeString = ((ShapeSettings.typeShapes_mode1)commonVars.stitcher.getPatternElement(patternIndex: pattern, index).getInt(PatternElement.properties_i.shapeIndex)).ToString();

        switch (shapeString)
        {
            case "bounding":
                groupBox_properties.Content = groupBox_bounding_table;
                break;
            case "complex":
                groupBox_properties.Content = new Expander() {Content = groupBox_layout_table, Header = "Edges", Expanded = false};
                break;
        }

        pDoPatternElementUI_subshape(pattern, index, updateUI, shapeString);
        pDoPatternElementUI_position(pattern, index);
        pDoPatternElementUI_rotation(pattern, index);
        pDoPatternElementUI_transform(pattern, index);
        pDoPatternElementUI_array(pattern, index);

        pDoPatternElementUI_baseShape_referencesUI(index);

        UIFreeze = false;

        if (doPreview)
        {
            pDrawPreviewPanelHandler();
        }
    }
}