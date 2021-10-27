using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.EnterprisePatterns.Specification
{
    public class Movie
    {
        public string Name { get; }
        public DateTime ReleaseDate { get; }
        public MpaaRating MpaaRating { get; set; }
        public string Genre { get; }
        public double Rating { get; set; }
    }

    public enum MpaaRating
    {
        G,
        PG13,
        R
    }
}
