namespace NASAViewer.Models
{
    public class EpicImageResponse
    {
        public string Identifier { get; set; }
        public string Caption { get; set; }
        public string Image { get; set; }
        public string Version { get; set; }
        public string Date { get; set; }
        public CentroidCoordinates CentroidCoordinates { get; set; }
        public DscovrJ2000Position DscovrJ2000Position { get; set; }
        public LunarJ2000Position LunarJ2000Position { get; set; }
        public SunJ2000Position SunJ2000Position { get; set; }
        public AttitudeQuaternions AttitudeQuaternions { get; set; }
        public Coords Coords { get; set; }
    }

    public class Coords
    {
        public CentroidCoordinates CentroidCoordinates { get; set; }
        public DscovrJ2000Position DscovrJ2000Position { get; set; }
        public LunarJ2000Position LunarJ2000Position { get; set; }
        public SunJ2000Position SunJ2000Position { get; set; }
        public AttitudeQuaternions AttitudeQuaternions { get; set; }
    }

    public class CentroidCoordinates
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class DscovrJ2000Position
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class LunarJ2000Position
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class SunJ2000Position
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class AttitudeQuaternions
    {
        public double Q0 { get; set; }
        public double Q1 { get; set; }
        public double Q2 { get; set; }
        public double Q3 { get; set; }
    }

}

