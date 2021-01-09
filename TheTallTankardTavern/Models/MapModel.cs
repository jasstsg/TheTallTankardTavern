using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static TheTallTankardTavern.Configuration.ApplicationSettings;

namespace TheTallTankardTavern.Models
{
    public class MapModel
    {
        public MapModel(string imageFolder)
        {
            Name = Path.GetFileNameWithoutExtension(imageFolder);
            ImageFolder = imageFolder;
        }
        public string Name { get; }
        public string ImageFolder { get; }
        public string ImageFile { get { return Directory.GetFiles(Directory.GetParent(this.ImageFolder).FullName, $"*{this.Name}*", SearchOption.TopDirectoryOnly).Single().Replace(WebRoot, ""); } }
        public List<MapModel> Children { get; set; } = new List<MapModel>();
        public MapModel Parent { get; set; }
    }
}
