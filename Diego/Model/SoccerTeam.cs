using System;
using System.Windows.Media.Imaging;

namespace Diego.Model
{
    public class SoccerTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public BitmapSource Logo { get; set; }
        public Uri LogoUrl { get; set; }
    }
}
