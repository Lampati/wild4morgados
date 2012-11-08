using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Windows.Controls.Ribbon;
using System.Windows.Controls;
using System.Windows;

namespace Ragnarok.Helpers
{
    public class RibbonTextBoxFixedWidth : RibbonTextBox
    {
        protected int textboxWidth;
        protected int labelWidth;

        public RibbonTextBoxFixedWidth()
        {
            // keep the specified textbox and label width
            this.textboxWidth = 300;
            this.labelWidth = 200;

            // add handler for the loaded event
            this.Loaded += new System.Windows.RoutedEventHandler(RibbonTextBoxFixedWidth_Loaded);
        }

        public RibbonTextBoxFixedWidth(int labelWidth, int textboxWidth)
        {
            // keep the specified textbox and label width
            this.textboxWidth = textboxWidth;
            this.labelWidth = labelWidth;

            // add handler for the loaded event
            this.Loaded += new System.Windows.RoutedEventHandler(RibbonTextBoxFixedWidth_Loaded);
        }

        void RibbonTextBoxFixedWidth_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // try to find the image inside the current control
            Image img = Utils.FindTemplateControl<Image>(this, false);

            // get the label
            Label lbl = Utils.FindTemplateControl<Label>(this, false);

            // get the scroll
            ScrollViewer scroll = Utils.FindTemplateControl<ScrollViewer>(this, false);

            // get the border
            Border border = this.Template.FindName("Bd", this) as Border;

            // if the border and label exist
            if (border != null && lbl != null && img != null)
            {
                // set the label's width
                lbl.Width = labelWidth;

                // set the left and right margin and padding to 0
                border.Margin = new Thickness(0, border.Margin.Top, 0, border.Margin.Bottom);
                border.Padding = new Thickness(0, border.Padding.Top, 0, border.Padding.Bottom);
                // the actual width of the textbox has to be set to the border
                border.Width = textboxWidth;

                // update the scroll size to the new textbox width + scroll + border
                scroll.Width = textboxWidth
                    + Utils.GetHorizontalSpace(scroll.Margin)
                    + Utils.GetHorizontalSpace(scroll.Padding)
                    + Utils.GetHorizontalSpace(border.Margin)
                    + Utils.GetHorizontalSpace(border.Padding);

                // update the width of this control to fit all its inner controls (image + label + border)
                this.Width = textboxWidth
                    + labelWidth + img.ActualWidth
                    + Utils.GetHorizontalSpace(border.BorderThickness)
                    + Utils.GetHorizontalSpace(border.Margin)
                    + Utils.GetHorizontalSpace(border.Padding)
                    + Utils.GetHorizontalSpace(img.Margin)
                    + Utils.GetHorizontalSpace(lbl.BorderThickness)
                    + Utils.GetHorizontalSpace(lbl.Margin)
                    + Utils.GetHorizontalSpace(lbl.Padding);
            }
        }
    }
}
