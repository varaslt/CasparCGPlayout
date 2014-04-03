using System;
using System.Globalization;

namespace CasparCGPlayout.Utils
{
    class TimeUtils
    {
        public static string fix0Padding(int p)
        {
            if (p < 10)
            { return "0" + p.ToString(CultureInfo.InvariantCulture); }
            return p.ToString(CultureInfo.InvariantCulture);
        }

        public static String frameToHHMMSSFF(Int32 iFrames, Int32 fps)
        {
            //TO DO: should switch for 25 / 30 / 29.... 

            long iWorkingFrames = iFrames;

            long iHr = iWorkingFrames / (fps * 60 * 60);
            iWorkingFrames = (iWorkingFrames - (iHr * fps * 60 * 60));

            long iMn = iWorkingFrames / (fps * 60);
            iWorkingFrames = (iWorkingFrames - (iMn * fps * 60));

            long iSec = iWorkingFrames / fps;

            iWorkingFrames = iWorkingFrames - (iSec * fps);
            long iFr = iWorkingFrames;

            return ((iHr < 10 ? "0" : "") + iHr + ":" + (iMn < 10 ? "0" : "") + iMn + ":" + (iSec < 10 ? "0" : "") + iSec +":"+ (iFr < 10 ? "0" : "") + iFr);
            

        }

       /* public static String SecondsToHHMMSSFF(double iSeconds)
        {
            double iSec, iMn, iHr;
            double iWorkingFrames;

            iHr = iWorkingFrames / (double.Parse(fps.ToString()) * 60 * 60); ;
            iWorkingFrames = iWorkingFrames - (iHr * fps * 60 * 60);

            iMn = iWorkingFrames / (double.Parse(fps.ToString()) * 60);
            iWorkingFrames = iWorkingFrames - (iMn * fps * 60);

            iSec = iWorkingFrames / double.Parse(fps.ToString());
            iWorkingFrames = iWorkingFrames - (iSec * fps);
            iFr = iWorkingFrames;

            return ((iHr < 10 ? "0" : "") + iHr
             + ":" + (iMn < 10 ? "0" : "") + iMn
             + ":" + (iSec < 10 ? "0" : "") + iSec);
            //+ ":" + (iFr < 10 ? "0" : "") + iFr);

            //  return ((iHr < 10 ? "0" : "") + Math.Round(iHr)
            //  + ":" + (iMn < 10 ? "0" : "") + Math.Round(iMn)
            //  + ":" + (iSec < 10 ? "0" : "") + Math.Round(iSec));
            //  //+ ":" + (iFr < 10 ? "0" : "") + iFr);

        }
        */
    }
}
