using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CasparCGPlayout.ItemClasses;

namespace CasparCGPlayout.Components.ExtendedListBox
{
    public partial class ExtendedListBox : ListBox
    {
        private Size _imageSize;
        private StringFormat _fmt;
        private Font _titleFont;
        private Font _detailsFont;
        private Font clipIDFont;
        private Font timeStartFont;
        private Font displayFont;
        private Font lengthOfClipFont;
        private Size imageSize;
        private StringFormat alignment;

        public ExtendedListBox(Font titleFont, Font detailsFont, Size imageSize, StringAlignment aligment, StringAlignment lineAligment)
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            _titleFont = titleFont;
            _detailsFont = detailsFont;
            _imageSize = imageSize;
            this.ItemHeight = _imageSize.Height + this.Margin.Vertical;
            _fmt = new StringFormat();
            _fmt.Alignment = aligment;
            _fmt.LineAlignment = lineAligment;
            _titleFont = titleFont;
            _detailsFont = detailsFont;
            //InitializeComponent();
        }

        public ExtendedListBox()
        {
            //InitializeComponent();

            DoubleBuffered = true;

            this.ItemHeight = _imageSize.Height + this.Margin.Vertical;
            _fmt = new StringFormat();
            _fmt.Alignment = StringAlignment.Near;
            _fmt.LineAlignment = StringAlignment.Near;
            _titleFont = new Font(this.Font, FontStyle.Bold);
            _detailsFont = new Font(this.Font, FontStyle.Bold);
            clipIDFont = new Font(this.Font, FontStyle.Bold);
            timeStartFont = new Font(this.Font, FontStyle.Bold);
            displayFont = new Font(this.Font.FontFamily, 12f, FontStyle.Bold);
            lengthOfClipFont = new Font(this.Font, FontStyle.Bold);
            imageSize = new Size(80, 60);
            this.MouseDown += new MouseEventHandler(exListBox_MouseDown);
            SetupContextMenuItems();

        }

        private void SetupContextMenuItems()
        {
            MenuItem item = new MenuItem("Delete");
            contextmenu_exListbox.MenuItems.Add(item);
        }

        void exListBox_MouseDown(object sender, MouseEventArgs e)
        {
            
        if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //select the item under the mouse pointer
                this.SelectedIndex = this.IndexFromPoint(e.Location);

                //if the selected index is an item, binding the context MenuStrip with the listBox
                if (this.SelectedIndex != -1)
                {
                    this.ContextMenu = contextmenu_exListbox;
                    contextmenu_exListbox.Show(this, e.Location);
                }
                //else, untie the contextMenuStrip to the listBox 
                else
                {
                    this.ContextMenu = null;
                }
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // prevent from error Visual Designer
            if (this.Items.Count > 0)
            {
                if ( this.Items[e.Index] is ListBoxVideoItem)
                {
                        ListBoxVideoItem item = (ListBoxVideoItem)this.Items[e.Index];
                        item.drawItem(e, this.Margin, timeStartFont, clipIDFont, displayFont, lengthOfClipFont, alignment, imageSize);
                } else if ( this.Items[e.Index] is ListBoxCGItem)
                {
                        ListBoxCGItem item = (ListBoxCGItem)this.Items[e.Index];
                        item.drawItem(e, this.Margin, timeStartFont, clipIDFont, displayFont, lengthOfClipFont, alignment, imageSize);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }


        public System.Windows.Forms.ContextMenu contextmenu_exListbox = new ContextMenu();
    }
}
