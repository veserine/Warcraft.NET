using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;
using TerrainObj1WoD = Warcraft.NET.Files.ADT.Terrain.WoD.TerrainObj1;

namespace Warcraft.NET.Files.ADT.Terrain.BfA
{
    public class TerrainObj1 : TerrainObj1WoD
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BfA.Terrain"/> class.
        /// </summary>
        /// <param name="file">The file path</param>
        public TerrainObj1(string file) : base(file)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BfA.TerrainObj1"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainObj1(byte[] inData) : base(inData)
        {
        }
    }
}
