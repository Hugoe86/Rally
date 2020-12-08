using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Ayudante.Util
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColorAttribute : Attribute
    {
        private int alpha;
        private int red;
        private int green;
        private int blue;

        public int Aplha { get { return alpha; } set { alpha = value; } }
        public int Red { get { return red; } set { red = value; } }
        public int Green { get { return green; } set { green = value; } }
        public int Blue { get { return blue; } set { blue = value; } }

        public ColorAttribute(int red, int green, int blue)
        {
            this.alpha = 1;
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public ColorAttribute(int alpha, int red, int green, int blue)
        {
            this.alpha = alpha;
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
    }
}