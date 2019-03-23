﻿
using OpenTK.Graphics;
using OpenTK.Platform;
using System;

namespace Onyx3D
{
	public class Onyx3DInstance : IDisposable
	{
		//public SceneManager Scenes = new SceneManager();
		public RenderManager Renderer = new RenderManager();
		public ResourcesManager Resources = new ResourcesManager();
		public GizmosManager Gizmos = new GizmosManager();

		private IGraphicsContext mGraphicsContext;
		private IWindowInfo mWindowsInfo;

		public static Onyx3DInstance CurrentContext { get; private set;}

        // --------------------------------------------------------------------

        public Onyx3DInstance(IntPtr handle)
        {
            IWindowInfo winInfo = Utilities.CreateWindowsWindowInfo(handle);
            IGraphicsContext context = new GraphicsContext(new GraphicsMode(), winInfo);
            Init(context, winInfo);
        }

        // --------------------------------------------------------------------

        public Onyx3DInstance(IGraphicsContext context, IWindowInfo info)
		{
            Init(context, info);
            MakeCurrent();
		}

        // --------------------------------------------------------------------

        private void Init(IGraphicsContext context, IWindowInfo info)
		{
            mWindowsInfo = info;
            mGraphicsContext = context;

			MakeCurrent();

			Resources.Init(this);
			Renderer.Init(this);
			Gizmos.Init(this);
		}

        // --------------------------------------------------------------------

        public void Reset()
        {
            Resources.ClearAll();
        }

        // --------------------------------------------------------------------

        public void Dispose()
		{
            mWindowsInfo.Dispose();
            mGraphicsContext.Dispose();

            mWindowsInfo = null;
            mGraphicsContext = null;
		}

        // --------------------------------------------------------------------

        public void MakeCurrent()
        {
            mGraphicsContext.MakeCurrent(mWindowsInfo);
			CurrentContext = this;
        }
    }
}
