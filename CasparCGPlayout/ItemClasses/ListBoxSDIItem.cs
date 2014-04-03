using System;
using CasparCGPlayout.Utils;
using System.Drawing;

namespace CasparCGPlayout.ItemClasses
{
    class ListBoxSDIItem : ListBoxItem
    {

        public ListBoxSDIItem(int id, string timestart, string clipid, string display1, string display2, TimeSpan lengthofclip, TypeEnum type, WhatNextEnum whatnext, Image itemImage, VideoCatergoryEnum category)
            : base(id, timestart)
        {
            
        }

    }
}
