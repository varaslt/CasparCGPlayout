using System;
using CasparCGPlayout.Utils;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Svt.Caspar;

namespace CasparCGPlayout.ItemClasses
{
    class ListBoxVideoItem : ListBoxItem
    {
        Image _whatnextimage;
        Image _categoryimage;
        Image _segueimage;
        WhatNextEnum _whatnext;
        Rectangle _clipIdBounds;
        Rectangle _display1Bounds;
        Rectangle _display2Bounds;
        Rectangle _lengthOfClipBounds;

        
        public string clipID { get; set; }
        public string display1 { get; set; }
        public string display2 { get; set; }
        public TimeSpan lengthOfClip { get; set; }
        public TypeEnum type { get; set; }
        public int inFrames { get; set; }
        public int outFrames { get; set; }
        public Image itemImage { get; set; }
        public VideoCatergoryEnum Catergory { get; set; }
        public TransitionType Segue { get; set; }
        public Int32 SegLength { get; set; }
        public bool existOnDisk { get; set; }
        public string ClipFilename { get; set; }
           
        public ListBoxVideoItem(int id, string timestart, string clipid, string display1, string display2, TimeSpan lengthofclip, TypeEnum type, WhatNextEnum whatnext, int inframes, int outframes, Image itemImage, VideoCatergoryEnum category, TransitionType segue, Int32 seglength, string clipfilename) : base (id, timestart)
        {
            this.clipID = clipid;
            this.display1 = display1;
            this.display2 = display2;
            this.lengthOfClip = lengthofclip;
            this.type = type;
            this._whatnext = whatnext;
            this.inFrames = inframes;
            this.outFrames = outframes;
            this.itemImage = itemImage;
            this.Catergory = category;
            this.Segue = segue;
            this.SegLength = seglength;
            
            this.ClipFilename = clipfilename; 
        }

        public void drawItem(DrawItemEventArgs e, Padding margin, Font timeStartFont, Font clipIDFont, Font displayFont, Font lengthOfClipFont, StringFormat aligment, Size imageSize)
        {
            
            //logger.Info(String.Format("{0}, {1}, {2}, {3}", e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            //e.Graphics.Clip = new Region(new Rectangle(32, 32, 32, 32));

            // if selected, mark the background differently
            if (((e.State & DrawItemState.Selected) == DrawItemState.Selected) && !isPlaying)
            {
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            }
            else
            {
                switch (type)
                {
                    case TypeEnum.Video:
                        switch (Catergory)
                        {
                            case VideoCatergoryEnum.Programme:
                                e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds);
                                break;
                            case VideoCatergoryEnum.Commercial:
                                e.Graphics.FillRectangle(Brushes.LightSeaGreen, e.Bounds);
                                break;
                            default:
                                e.Graphics.FillRectangle(Brushes.LightPink, e.Bounds);
                                break;
                        }
                        break;
                    case TypeEnum.CG:
                        e.Graphics.FillRectangle(Brushes.PaleVioletRed, e.Bounds);
                        break;
                }

            }

           
            switch (_whatnext)
            {
                case WhatNextEnum.Playnext:
                    _whatnextimage = Image.FromStream(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("CasparCGPlayout.images.greendot.png"));
                    break;
                case WhatNextEnum.Wait:
                    _whatnextimage = Image.FromStream(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("CasparCGPlayout.images.redsquare.png"));
                    break;
            }

            if (isPlaying)
            {
                e.Graphics.FillRectangle(Brushes.YellowGreen, e.Bounds);
                _whatnextimage = Image.FromStream(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("CasparCGPlayout.images.play.png"));
            }


            switch (Catergory)
             {
                 case VideoCatergoryEnum.Commercial:
                     _categoryimage = Image.FromStream(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("CasparCGPlayout.images.commercial.png"));
                     break;
                 case VideoCatergoryEnum.Ident:
                     _categoryimage = Image.FromStream(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("CasparCGPlayout.images.ident.png"));
                     break;
                 case VideoCatergoryEnum.Programme:
                     _categoryimage = Image.FromStream(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("CasparCGPlayout.images.programme.png"));
                     break;
             }

            switch (Segue)
            {
                case TransitionType.CUT:
                    _segueimage = Image.FromStream(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("CasparCGPlayout.images.quickopen.png"));
                    break;
                case TransitionType.MIX:
                    _segueimage = Image.FromStream(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("CasparCGPlayout.images.view-remove.png"));
                    break;
                case TransitionType.WIPE:
                    _segueimage = Image.FromStream(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("CasparCGPlayout.images.transform-distort.png"));
                    break;
            }


            // draw some item separator
            e.Graphics.DrawLine(Pens.DarkGray, e.Bounds.X, e.Bounds.Y, e.Bounds.X + e.Bounds.Width, e.Bounds.Y);

            // draw item image
            e.Graphics.DrawImage(this.itemImage, e.Bounds.X + margin.Left, e.Bounds.Y + margin.Top, imageSize.Width, imageSize.Height);

            // draw segue image
            e.Graphics.DrawImage(this._segueimage, e.Bounds.Width - imageSize.Width - margin.Right - imageSize.Width, e.Bounds.Y + margin.Top, imageSize.Width, imageSize.Height);

            //Draw Catergory Image
            e.Graphics.DrawImage(this._categoryimage, e.Bounds.Width - imageSize.Width - margin.Right - _whatnextimage.Width - imageSize.Width - 30, e.Bounds.Y + margin.Top, imageSize.Width / 2, imageSize.Height / 2);

            //Draw Type Image
            if (_whatnextimage != null)
            {
                e.Graphics.DrawImage(_whatnextimage, e.Bounds.Width - imageSize.Width - margin.Right, e.Bounds.Y + margin.Top, imageSize.Width, imageSize.Height);
                
            }

            // calculate bounds for details text drawing
            _clipIdBounds = new Rectangle(e.Bounds.X + margin.Horizontal + imageSize.Width,
                                                   e.Bounds.Y + (int)timeStartFont.GetHeight() + 2 + margin.Vertical + margin.Top,
                                                   e.Bounds.Width - margin.Right - imageSize.Width - margin.Horizontal,
                                                   e.Bounds.Height - margin.Bottom - (int)timeStartFont.GetHeight() - 2 - margin.Vertical - margin.Top);

            if (display2 == "")
            {

                _display1Bounds = new Rectangle((e.Bounds.X + margin.Horizontal) + 220, (e.Bounds.Y + (int)displayFont.GetHeight() + 5), e.Bounds.Width - margin.Right - imageSize.Width - margin.Horizontal,
                                                      (int)displayFont.GetHeight() + 2);
            }
            else
            {
                _display1Bounds = new Rectangle((e.Bounds.X + margin.Horizontal) + 220, (e.Bounds.Y + (int)displayFont.GetHeight() - 5), e.Bounds.Width - margin.Right - imageSize.Width - margin.Horizontal,
                                                      (int)displayFont.GetHeight() + 2);
            }

            _display2Bounds = new Rectangle((e.Bounds.X + margin.Horizontal) + 220, (e.Bounds.Y + (int)displayFont.GetHeight() + 15), e.Bounds.Width - margin.Right - imageSize.Width - margin.Horizontal,
                                                  (int)displayFont.GetHeight() + 2);

            _lengthOfClipBounds = new Rectangle(e.Bounds.X + margin.Horizontal + imageSize.Width,
                                               e.Bounds.Y + margin.Top + 39,
                                                e.Bounds.Width - margin.Right - imageSize.Width - margin.Horizontal,
                                                  (int)timeStartFont.GetHeight() + 2);



            // draw the text within the bounds
            e.Graphics.DrawString(clipID, clipIDFont, Brushes.Black, _clipIdBounds, aligment);
            e.Graphics.DrawString(display1, displayFont, Brushes.Black, _display1Bounds, aligment);
            e.Graphics.DrawString(display2, displayFont, Brushes.Black, _display2Bounds, aligment);
            e.Graphics.DrawString(TimeUtils.fix0Padding(lengthOfClip.Hours) + ":" + TimeUtils.fix0Padding(lengthOfClip.Minutes) + ":" + TimeUtils.fix0Padding(lengthOfClip.Seconds), lengthOfClipFont, Brushes.Black, _lengthOfClipBounds, aligment);


            // put some focus rectangle
            e.DrawFocusRectangle();
        }

        internal List<Rectangle> getRects()
        {
            var rects = new List<Rectangle> { _clipIdBounds, _display1Bounds, _display2Bounds, _clipIdBounds };
            return rects;
        }

        
    }
}
