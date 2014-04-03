using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using FFmpegSharp;
using System.Timers;

namespace CasparCG_Mediawatcher
{
    public class VideoHandler
    {
        IVideoStream m_stream;
        
        int i = 0;

        public IVideoStream Stream
        {
            get { return m_stream; }
            set
            {
                m_stream = value;
                if (m_stream != null)
                {



                    byte[] frame;
                    if (m_stream.ReadFrame(out frame))
                    {
                        Bitmap image = new Bitmap(m_stream.Width, m_stream.Height);

                        BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

                        Marshal.Copy(frame, 0, data.Scan0, frame.Length);

                        image.UnlockBits(data);

                        //pe.Graphics.DrawImage(image, ClientRectangle);

                        String fn = "g:\\graphs\\graphic-" + i + ".jpg";
                        i++;
                        image.Save(fn, ImageFormat.Jpeg);

                    }
                }
            }
        }

        public VideoHandler()
        {
            

            
        }

    }
}


