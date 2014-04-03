using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace CasparCGPlayout.DigitalStudioClock
{

    public class DigitalStudioClock : UserControl
    {
        Color ticksColor = Color.Black;
        //float fTicksThickness = 1;

        float fCenterX;
        float fCenterY;
        float fRadius;

        int sec = 0;


        private Color _digitColor = Color.Red;
        [Browsable(true), DefaultValue("Color.Red")]
        public Color DigitColor
        {
            get { return _digitColor; }
            set { _digitColor = value; Invalidate(); }
        }

        private string _digitText = "88.88";
        [Browsable(true), DefaultValue("88.88")]
        public string DigitText
        {
            get { return _digitText; }
            set { _digitText = value; Invalidate(); }
        }

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Timer timer1 = new Timer();

        public DigitalStudioClock()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            this.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            // 
            // DigitalStudioClock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "DigitalStudioClock";
            this.Size = new System.Drawing.Size(649, 507);
            this.ResumeLayout(false);
            this.Paint += new PaintEventHandler(this.DigitalGauge_Paint);

            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

        }

        #endregion
        private void DigitalGauge_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.BackColor = Color.Black;

            this.fRadius = this.Height / 2;
            this.fCenterX = this.ClientSize.Width / 2;
            this.fCenterY = this.ClientSize.Height / 2;

            DateTime dateTime = DateTime.Now;
            sec = dateTime.Second;
            double secRadian = sec * 360 / 60 * Math.PI / 180;

            //Find Center
            DrawRadiusDot(e);
            DrawTime(e);

        }

        private void DrawTime(PaintEventArgs e)
        {
            FivebySevenHelper FivebySevenHelper = new FivebySevenHelper(e.Graphics);


            SizeF digitSizeF = FivebySevenHelper.GetStringSize(_digitText, Font);
            float scaleFactor = Math.Min(ClientSize.Width / digitSizeF.Width,
                                         ClientSize.Height / digitSizeF.Height);

            Font font = new Font(Font.FontFamily, scaleFactor * Font.SizeInPoints);
            digitSizeF = FivebySevenHelper.GetStringSize(_digitText, font);

            using (SolidBrush brush = new SolidBrush(_digitColor))
            {
                using (SolidBrush lightBrush =
                    new SolidBrush(Color.FromArgb(20, _digitColor)))
                {
                    FivebySevenHelper.DrawDigits(
                      _digitText, font, brush, lightBrush,
                      (ClientSize.Width - digitSizeF.Width) + 45,
                      (ClientSize.Height - digitSizeF.Height) * 0.85F);
                }
            }
        }

        private void DrawRadiusDot(PaintEventArgs e)
        {

            for (int i = 0; i < sec + 1; i++)
            {
                e.Graphics.FillEllipse(new SolidBrush(_digitColor),
                fCenterX + (float)(this.fRadius / 1.10F * System.Math.Sin(i * 6 * Math.PI / 180)),
                fCenterY - (float)(this.fRadius / 1.10F * System.Math.Cos(i * 6 * Math.PI / 180)),
                this.Height / 50, this.Height / 50);
            }

            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0)
                // Draw 5 minute ticks
                {
                    e.Graphics.FillEllipse(new SolidBrush(_digitColor),
                    fCenterX + (float)(this.fRadius / 1.15F * System.Math.Sin(i * 6 * Math.PI / 180)),
                    fCenterY - (float)(this.fRadius / 1.15F * System.Math.Cos(i * 6 * Math.PI / 180)),
                    this.Height / 60, this.Height / 60);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

    }

    public class FivebySevenHelper
    {
        readonly Graphics _graphics;

        // Indicates what segments are illuminated for all 10 digits
        static readonly byte[,] _segmentData0 ={{0, 1, 1, 1, 0},
							           {1, 0, 0, 0, 1},  
							           {1, 0, 0, 0, 1},  
							           {1, 0, 0, 0, 1},  
							           {1, 0, 0, 0, 1},  
							           {1, 0, 0, 0, 1},  
							           {0, 1, 1, 1, 0}};

        static readonly byte[,] _segmentData1 ={{0, 1, 0, 0, 0},
							           {1, 1, 0, 0, 0},  
							           {0, 1, 0, 0, 0},  
							           {0, 1, 0, 0, 0},  
							           {0, 1, 0, 0, 0},  
							           {0, 1, 0, 0, 0},  
							           {1, 1, 1, 0, 0}};

        static readonly byte[,] _segmentData2 ={{0, 1, 1, 1, 0},
							           {1, 0, 0, 0, 1},  
							           {0, 0, 0, 0, 1},  
							           {0, 1, 1, 1, 0},  
							           {1, 0, 0, 0, 0},  
							           {1, 0, 0, 0, 0},  
							           {1, 1, 1, 1, 1}};

        static readonly byte[,] _segmentData3 ={{0, 1, 1, 1, 0},
							           {1, 0, 0, 0, 1},  
							           {0, 0, 0, 0, 1},  
							           {0, 1, 1, 1, 0},  
							           {0, 0, 0, 0, 1},  
							           {1, 0, 0, 0, 1},  
							           {0, 1, 1, 1, 0}};

        static readonly byte[,] _segmentData4 ={{0, 0, 0, 1, 0},
							           {0, 0, 1, 1, 0},  
							           {0, 1, 0, 1, 0},  
							           {1, 0, 0, 1, 0},  
							           {1, 1, 1, 1, 1},  
							           {0, 0, 0, 1, 0},  
							           {0, 0, 0, 1, 0}};

        static readonly byte[,] _segmentData5 ={{1, 1, 1, 1, 1},
							           {1, 0, 0, 0, 0},  
							           {1, 0, 0, 0, 0},  
							           {1, 1, 1, 1, 0},  
							           {0, 0, 0, 0, 1},  
							           {0, 0, 0, 0, 1},  
							           {1, 1, 1, 1, 0}};

        static readonly byte[,] _segmentData6 ={{0, 1, 1, 1, 0},
							           {1, 0, 0, 0, 1},  
							           {1, 0, 0, 0, 0},  
							           {1, 1, 1, 1, 0},  
							           {1, 0, 0, 0, 1},  
							           {1, 0, 0, 0, 1},  
							           {0, 1, 1, 1, 0}};

        static readonly byte[,] _segmentData7 ={{1, 1, 1, 1, 1},
							           {0, 0, 0, 0, 1},  
							           {0, 0, 0, 1, 0},  
							           {0, 0, 1, 0, 0},  
							           {0, 1, 0, 0, 0},  
							           {0, 1, 0, 0, 0},  
							           {0, 1, 0, 0, 0}};

        static readonly byte[,] _segmentData8 ={{0, 1, 1, 1, 0},
							           {1, 0, 0, 0, 1},  
							           {1, 0, 0, 0, 1},  
							           {0, 1, 1, 1, 0},  
							           {1, 0, 0, 0, 1},  
							           {1, 0, 0, 0, 1},  
							           {0, 1, 1, 1, 0}};

        static readonly byte[,] _segmentData9 ={{0, 1, 1, 1, 0},
							           {1, 0, 0, 0, 1},  
							           {1, 0, 0, 0, 1},  
							           {0, 1, 1, 1, 1},  
							           {0, 0, 0, 0, 1},  
							           {0, 0, 0, 0, 1},  
							           {0, 1, 1, 1, 0}};

        static readonly Byte[][,] _segmentData = new Byte[10][,];


        public FivebySevenHelper(Graphics graphics)
        {
            this._graphics = graphics;
            _segmentData.SetValue(_segmentData0, 0);
            _segmentData.SetValue(_segmentData1, 1);
            _segmentData.SetValue(_segmentData2, 2);
            _segmentData.SetValue(_segmentData3, 3);
            _segmentData.SetValue(_segmentData4, 4);
            _segmentData.SetValue(_segmentData5, 5);
            _segmentData.SetValue(_segmentData6, 6);
            _segmentData.SetValue(_segmentData7, 7);
            _segmentData.SetValue(_segmentData8, 8);
            _segmentData.SetValue(_segmentData9, 9);
        }



        public SizeF GetStringSize(string text, Font font)
        {
            SizeF sizef = new SizeF(0, _graphics.DpiX * font.SizeInPoints / 72);

            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsDigit(text[i]))
                    sizef.Width += 42 * _graphics.DpiX * font.SizeInPoints / 72 / 72;
                else if (text[i] == ':' || text[i] == '.')
                    sizef.Width += 12 * _graphics.DpiX * font.SizeInPoints / 72 / 72;
            }
            return sizef;
        }

        public void DrawDigits(string text, Font font, Brush brush,
        Brush brushLight, float x, float y)
        {
            DateTime dt = DateTime.Now;
            float start = x;
            int segment1 = 0, segment2 = 0, segment3 = 0, segment4 = 0;
            int segmentsec1 = 0, segmentsec2 = 0;

            if (dt.Hour < 10)
            {
                segment1 = 0; segment2 = dt.Hour;
            }
            else
            {
                segment1 = Int32.Parse(dt.Hour.ToString(CultureInfo.InvariantCulture).Substring(0, 1));
                segment2 = Int32.Parse(dt.Hour.ToString(CultureInfo.InvariantCulture).Substring(1, 1));
            }

            if (dt.Minute < 10)
            {
                segment3 = 0; segment4 = dt.Minute;
            }
            else
            {
                segment3 = Int32.Parse(dt.Minute.ToString(CultureInfo.InvariantCulture).Substring(0, 1));
                segment4 = Int32.Parse(dt.Minute.ToString(CultureInfo.InvariantCulture).Substring(1, 1));
            }

            //Seconds
            if (dt.Second < 10)
            {
                segmentsec1 = 0; segmentsec2 = dt.Second;
            }
            else
            {
                segmentsec1 = Int32.Parse(dt.Second.ToString(CultureInfo.InvariantCulture).Substring(0, 1));
                segmentsec2 = Int32.Parse(dt.Second.ToString(CultureInfo.InvariantCulture).Substring(1, 1));
            }



            x = DrawDigit(segment1, font, brush, brushLight, x, y);
            x = DrawDigit(segment2, font, brush, brushLight, x, y);
            x = DrawColon(font, brush, x, y);
            x = DrawDigit(segment3, font, brush, brushLight, x, y);
            x = DrawDigit(segment4, font, brush, brushLight, x, y);
            x = DrawColon(font, brush, x, y);
            x = DrawDigit(segmentsec1, font, brush, brushLight, x, y);
            x = DrawDigit(segmentsec2, font, brush, brushLight, x, y);
        }

        private float DrawColon(Font font, Brush brush, float x, float y)
        {
            // if (!isColonLit)
            //{
            _graphics.FillEllipse(new SolidBrush(Color.DarkRed), x + 5, y + 13, 3, 3);
            _graphics.FillEllipse(new SolidBrush(Color.DarkRed), x + 5, y + 23, 3, 3);
            //   isColonLit = true;
            /*}
            else
            {
                _graphics.FillEllipse(new SolidBrush(Color.Transparent), x, y, 15, 15);
                _graphics.FillEllipse(new SolidBrush(Color.Transparent), x, y + 80, 15, 15);
                isColonLit = false;
            }*/
            return x + 10;
        }



        private void FillPolygon(Point[] polygonPoints, Font font,
        Brush brush, float x, float y)
        {
            PointF[] polygonPointsF = new PointF[polygonPoints.Length];

            for (int cnt = 0; cnt < polygonPoints.Length; cnt++)
            {
                polygonPointsF[cnt].X = x + polygonPoints[cnt].X *
                    _graphics.DpiX * font.SizeInPoints / 72 / 72;
                polygonPointsF[cnt].Y = y + polygonPoints[cnt].Y *
                    _graphics.DpiY * font.SizeInPoints / 72 / 72;
            }
            _graphics.FillPolygon(brush, polygonPointsF);
        }

        private float DrawDigit(int num, Font font, Brush brush, Brush brushLight,
        float x, float y)
        {
            int i = 1;
            int j = 1;

            for (i = 1; i <= 6; i++)
            {
                Color color;
                if (_segmentData[num][j - 1, i - 1] == 1) { color = Color.Red; } else { color = Color.Black; }

                _graphics.FillEllipse(new SolidBrush(color), x + (i * 4), y + (j * 4), 3, 3);
                if (i == 5) { i = 0; j++; }
                if (j == 8) { break; }
            }
            return x + 17 * _graphics.DpiX * font.SizeInPoints / 72 / 72;
        }



    }
}
