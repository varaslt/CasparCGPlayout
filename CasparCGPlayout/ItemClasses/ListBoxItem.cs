using CasparCGPlayout.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CasparCGPlayout.ItemClasses
{
    public enum VideoCatergoryEnum
    {
        Programme,
        Commercial,
        Ident
    }

    public enum TypeEnum
    {
        Video,
        CG
    }

        class ListBoxItem
        {
            public int id { get; set; }
            public string timeStart { get; set; }
            public bool isPlaying { get; set; }
            

            public ListBoxItem(int id, string timestart)
            {
                isPlaying = false;
                this.id = id;
            }

            
        }

}
