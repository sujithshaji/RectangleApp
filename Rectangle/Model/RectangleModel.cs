#region Namespace
using System;
#endregion

namespace RectangleApp
{
    #region RectangleModel Class
    /// <summary>
    /// Model class representing each rectangle
    /// </summary>
    public class RectangleModel
    {
        /// <summary>
        /// Width of rectangle
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height of rectangle
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Area of rectangle
        /// </summary>
        public double Area
        {
            get
            {
                return Width * Height;
            }
        }

        /// <summary>
        /// Diagonal length of rectangle
        /// </summary>
        public double DiagonalLength
        {
            get
            {
                return GetDiagonalLength();
            }
        }

        /// <summary>
        /// Type of rectangle (Tall/Flat/Square)
        /// </summary>
        public RectangleType RectangleType
        {
            get
            {
                return GetTypeOfRectangle();
            }
        }

        /// <summary>
        /// Type of rectangle (Tall/Flat/Square) considering it's width and height
        /// </summary>
        private RectangleType GetTypeOfRectangle()
        {
            if (Width > Height)
            {
                return RectangleType.Flat;
            }
            else if (Width < Height)
            {
                return RectangleType.Tall;
            }
            return RectangleType.Square;
        }

        /// <summary>
        /// Calculate diagonal length of rectangle
        /// </summary>
        /// <returns></returns>
        private double GetDiagonalLength()
        {
            return (Math.Sqrt((Width * Width) + (Height * Height)));
        }

    } 
    #endregion
}
