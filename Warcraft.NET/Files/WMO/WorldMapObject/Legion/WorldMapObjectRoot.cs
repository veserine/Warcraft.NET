﻿using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WMO.Chunks;
using MOHD = Warcraft.NET.Files.WMO.Chunks.Legion.MOHD;
using MOMT = Warcraft.NET.Files.WMO.Chunks.Wotlk.MOMT;

namespace Warcraft.NET.Files.WMO.WorldMapObject.Legion
{
    public class WorldMapObjectRoot : WorldMapObjectRootBase
    {
        /// <summary>
        /// Gets or sets the WMO header
        /// </summary>
        [ChunkOrder(2)]
        public MOHD Header { get; set; }

        /// <summary>
        /// Gets or sets WMO textures. 
        /// </summary>
        [ChunkOrder(3)]
        public MOTX Textures { get; set; }

        /// <summary>
        /// Gets or sets the materials.
        /// </summary>
        [ChunkOrder(4)]
        public MOMT Materials { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Legion.WorldMapObjectRoot"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldMapObjectRoot(byte[] inData) : base(inData)
        {
        }

        /// <summary>
        /// Serializes the current object into a byte array.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Version.Serialize());
                bw.Write(Header.Serialize());
                return ms.ToArray();
            }
        }
    }
}