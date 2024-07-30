using Avalonia.Input;
using DynamicData;
using HarfBuzzSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RouteOptimization.Utils
{
    public static class RouteMath
    {
        public static bool CursorInLine(double x1, double y1, double x2, double y2, double cX, double cY, double radius)
        {
            bool pointInside1 = CursorInPoint(x1, y1, cX, cY, radius);
            bool pointInside2 = CursorInPoint(x2, y2, cX, cY, radius);
            if (pointInside1 || pointInside2)
                return true;

            double lenght = Distance(x1, y1, x2, y2);

            double dot = (((cX - x1)*(x2-x1)) + ((cY - y1)*(y2-y1))) / Math.Pow(lenght,2);

            double closestX = x1 + (dot * (x2 - x1));
            double closestY = y1 + (dot * (y2 - y1));

            bool onSegment = linePoint(x1, y1, x2, y2, closestX, closestY);
            if (!onSegment) return false;

            double distance = Distance(cX,cY,closestX, closestY);

            if (distance <= radius)
            {
                return true;
            }
            return false;
        }

        private static bool linePoint(double x1, double y1, double x2, double y2, double pX, double pY)
        {
            double distance1 = Distance(x1, y1, pX, pY);
            double distance2 = Distance(x2, y2, pX, pY);

            double lenght = Distance(x1, y1, x2, y2);

            double buffer = 0.1;

            if (distance1 + distance2 >= lenght - buffer && distance1 + distance2 <= lenght + buffer)
            {
                return true;
            }
            return false;
        }

        public static bool CursorInPoint(double pointX, double pointY, double cursorX, double cursorY, double radius)
        {
            double r = Distance(pointX, pointY, cursorX, cursorY);
            if (r <= radius)
            {
                return true;
            }
            return false;
        }
        public static double Distance(double x1, double y1, double x2, double y2)
        {
            double x = Math.Abs(x2 - x1);
            double y = Math.Abs(y2 - y1);

            return Math.Sqrt((x * x) + (y * y));
        }

    }
}
