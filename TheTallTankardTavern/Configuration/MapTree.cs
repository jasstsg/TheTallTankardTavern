using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheTallTankardTavern.Configuration;

namespace TheTallTankardTavern.Configuration
{
    public class MapTree
    {
        /// <param name="currentMapPath">This is the path of both the current map file (without extension) and the folder of its children (if it exists)</param>
        public MapTree(string currentMapPath, MapTree Parent = null)
        {
            this.Name = Path.GetFileName(currentMapPath);   //Get just the filename without the path
            this.FilePath = Directory.GetFiles(Directory.GetParent(currentMapPath).FullName, $"*{this.Name}*", SearchOption.TopDirectoryOnly).Single();
            this.Parent = Parent;
            InitializeChildren(currentMapPath);
        }
        public string Name { get; }
        public string FilePath { get; }
        public MapTree Parent { get; }
        public List<MapTree> Children { get; private set; }
        public string ImgSrcAttribute { get { return FilePath.Replace(ApplicationSettings.WebRoot, ""); } }

        private void InitializeChildren(string currentMapPath)
        {
            this.Children = new List<MapTree>();
            if (Directory.Exists(currentMapPath))
            {
                string[] childmaps = Directory.GetFiles(currentMapPath).Select(f => Path.ChangeExtension(f, null)).ToArray();
                foreach (string childmap in childmaps)
                {
                    this.Children.Add(new MapTree(childmap, this));
                }
            }
        }

        public static MapTree GetMap(MapTree CurrentMapNode, List<string> MapPath)
        {
            if (MapPath.Count <= 0)
            {
                throw new Exception("Map Path Empty");
            }
            else if (MapPath.Count == 1)
            {
                return CurrentMapNode;
            }
            MapPath.RemoveAt(0);
            foreach (MapTree ChildMap in CurrentMapNode.Children)
            {
                if (MapPath.First().Equals(ChildMap.Name))
                {
                    return GetMap(ChildMap, MapPath);
                }
            }
            throw new Exception("Map was not found in the Map Tree");
        }
    }
}
