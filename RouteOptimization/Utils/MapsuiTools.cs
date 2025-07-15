using Mapsui;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Styles;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Utils
{
    public static class MapsuiTools
    {
        public static WritableLayer CreateLayer(string name, params IFeature[] features)
        {
            WritableLayer layer = new WritableLayer();
            layer.Name = name;
            layer.Style = null;
            foreach (var feature in features)
            {
                layer.Add(feature);
            }
            return layer;
        }

        public static GeometryFeature CreateGeometryFeature(params IStyle[] styles)
        {
            var feature = new GeometryFeature();
            foreach (var style in styles)
            {
                feature.Styles.Add(style);
            }
            return feature;
        }
        public static GeometryFeature CreateGeometryFeature(Geometry? geometry, params IStyle[] styles)
        {
            var feature = CreateGeometryFeature(styles);
            feature.Geometry = geometry;
            return feature;
        }
        public static Geometry GetGeometry(ILocation startLocation, ILocation finishLocation)
        {
            string strStartLocation = (startLocation.X + " " + startLocation.Y).Replace(",", ".");
            string strFinishLocation = (finishLocation.X + " " + finishLocation.Y).Replace(",", ".");

            string line = $"LINESTRING({strStartLocation}, {strFinishLocation})";

            return new WKTReader().Read(line);
        }


        public static PointFeature CreatePointFeature(params IStyle[] styles) => CreatePointFeature(0, 0, styles);
        public static PointFeature CreatePointFeature(double x, double y, params IStyle[] styles)
        {
            var feature = new PointFeature(x, y);
            foreach (var style in styles)
            {
                feature.Styles.Add(style);
            }
            return feature;
        }
        public static LabelStyle CreateLabelStyle(string text = "Без имени") => new LabelStyle
        {
            Text = text,
            ForeColor = Color.White,
            BackColor = new Brush(Color.Gray),
            BorderColor = Color.Black,
            Halo = new Pen(Color.Black),
            Opacity = 0.5f,
            Offset = new Offset(5, -5),
            HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Left,
            VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Bottom
        };
        public static SymbolStyle CreateSymbolStyle() => new SymbolStyle
        {
            Fill = new Brush(Color.LightBlue),
            Outline = new Pen(Color.Blue, 4),
            SymbolScale = 0.5f,
        };
        public static VectorStyle CreateVectorStyle() => new VectorStyle
        {
            Line = new Pen(Color.Black, 4),
        };
    }
}
