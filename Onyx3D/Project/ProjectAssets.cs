﻿using System;

namespace Onyx3D
{

	public class BuiltInShader
	{
		public static int Default = 300000001;
		public static int Unlit = 300000002;
		public static int UnlitVertexColor = 300000003;
		public static int ReflectionProbe = 300000004;
	}

	public class BuiltInMesh
	{
		public static int Teapot = 100000001;
		public static int Sphere = 100000002;
		public static int Torus = 100000003;
		public static int Cube = 100000004;
		public static int Cylinder = 100000005;
        public static int Quad = 100000006;
    }

	public class BuiltInTexture
	{
		public static int Checker = 200000001;
		public static int White = 200000002;
		public static int Black = 200000003;
		public static int Normal = 200000004;
	}


	public class BuiltInMaterial
	{
        public static int NotFound = 400000000;
        public static int Default = 400000001;
		public static int Unlit = 400000002;
		public static int UnlitVertexColor = 400000003;
		public static int ReflectionProbe = 400000004;
	}

	public class ContentIds
	{
		public static int Scenes = 0000000;
		public static int Meshes = 1000000;
		public static int Textures = 2000000;
		public static int Shaders = 3000000;
		public static int Materials = 4000000;
	}

	[Serializable]
	public class OnyxProjectAsset
	{
        public bool Dirty;

        public string AbsolutePath
        {
            get { return ProjectContent.GetAbsolutePath(Path); }
        }

		public string Path;

		public int Guid;

		public OnyxProjectAsset() { }

		public OnyxProjectAsset(string path, int guid = 0)
		{
			Path = path;
			Guid = guid;
		}

    };

	[Serializable]
	public class OnyxProjectShaderAsset : OnyxProjectAsset
	{
		public string PathVertex;
		public string PathFragment;

		public OnyxProjectShaderAsset() { }

		public OnyxProjectShaderAsset(string pathV, string pathF, int guid = 0) : base("", guid)
		{
			PathVertex = pathV;
			PathFragment = pathF;
		}
	};

	[Serializable]
	public class OnyxProjectSceneAsset : OnyxProjectAsset
	{
		public string Name;

		public OnyxProjectSceneAsset() { }
		public OnyxProjectSceneAsset(string path, string name = "", int guid = 0) : base(path, guid)
		{
			Name = name;
		}
	};

	[Serializable]
	public class OnyxProjectMaterialAsset : OnyxProjectAsset
	{
		public string Name;

		public OnyxProjectMaterialAsset() { }
		public OnyxProjectMaterialAsset(string path, string name = "", int guid = 0) : base(path, guid)
		{
			Name = name;
		}

    };
}
