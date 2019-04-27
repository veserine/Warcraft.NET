﻿using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;
using Warcraft.NET.Files.WMO.Flags;

namespace Warcraft.NET.Files.WMO.Chunks.Wotlk
{
    /// <summary>
    /// MOMT Chunk - Contains the WMO materials.
    /// </summary>
    public class MOMT : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MOMT";

        /// <summary>
        /// Gets or sets the MOMT flags.
        /// </summary>
        public MOMTFlags Flags { get; set; }

        /// <summary>
        ///  Gets or sets the shader. Index in CMapObj::s_wmoShaderMetaData. See https://wowdev.wiki/WMO#Shader_types_.2826522.29
        /// </summary>
        public uint Shader { get; set; }

        /// <summary>
        /// Gets or sets the blend mode. See https://wowdev.wiki/Rendering#EGxBlend
        /// </summary>
        public uint BlendMode { get; set; }

        /// <summary>
        /// Gets or sets the offset of the first texture in the <see cref="MOTX"/> chunk.
        /// </summary>
        public uint Texture1Offset { get; set; }

        /// <summary>
        /// Gets or sets the emissive color. See https://wowdev.wiki/WMO#Emissive_color
        /// </summary>
        public RGBA SidnColor { get; set; }

        /// <summary>
        /// Gets or sets the sidn emissive color. Set at runtime. Gets sidn-manipulated emissive color. See https://wowdev.wiki/WMO#Emissive_color
        /// </summary>
        public RGBA FrameSidnColor { get; set; }

        /// <summary>
        /// Gets or sets the offset of the second texture in the <see cref="MOTX"/> chunk.
        /// </summary>
        public uint Texture2Offset { get; set; }

        /// <summary>
        /// Gets or sets the diffuse color.
        /// </summary>
        public RGBA Texture2DiffuseColor { get; set; }

        /// <summary>
        /// Gets or sets the ground type according to CMapObjDef::GetGroundType.
        /// </summary>
        public uint GroundType { get; set; }

        /// <summary>
        /// Gets or sets the offset of the third texture in the <see cref="MOTX"/> chunk.
        /// </summary>
        public uint Texture3Offset { get; set; }

        /// <summary>
        /// Gets or sets the diffuse color of the second texture.
        /// </summary>
        public RGBA Texture3DiffuseColor { get; set; }

        /// <summary>
        /// Gets or sets unknown(?) flags.
        /// </summary>
        public uint Texture3Flags { get; set; }

        /// <summary>
        /// This data is explicitly nulled upon loading. Cotains textures or similar stuff.
        /// </summary>
        public uint[] RunTimeData { get; set; } = new uint[4];

        /// <summary>
        /// Initializes a new instance of the <see cref="MOMT"/> class.
        /// </summary>
        public MOMT()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MOMT"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MOMT(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Flags = (MOMTFlags)br.ReadUInt32();
                Shader = br.ReadUInt32();
                BlendMode = br.ReadUInt32();
                Texture1Offset = br.ReadUInt32();
                SidnColor = br.ReadBGRA();
                FrameSidnColor = br.ReadBGRA();
                Texture2Offset = br.ReadUInt32();
                Texture2DiffuseColor = br.ReadBGRA();
                GroundType = br.ReadUInt32();
                Texture3Offset = br.ReadUInt32();
                Texture3DiffuseColor = br.ReadBGRA();
                Texture3Flags = br.ReadUInt32();
                for (int i = 0; i < 4; i++)
                    RunTimeData[i] = br.ReadUInt32();
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((uint)Flags);
                bw.Write(Shader);
                bw.Write(BlendMode);
                bw.Write(Texture1Offset);
                bw.WriteBGRA(SidnColor);
                bw.WriteBGRA(FrameSidnColor);
                bw.Write(Texture2Offset);
                bw.WriteBGRA(Texture2DiffuseColor);
                bw.Write(GroundType);
                bw.Write(Texture3Offset);
                bw.WriteBGRA(Texture3DiffuseColor);
                bw.Write(Texture3Flags);
                for (int i = 0; i < 4; i++)
                    bw.Write(RunTimeData[i]);
                return ms.ToArray();
            }
        }
    }
}
