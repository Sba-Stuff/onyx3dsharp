﻿using OpenTK;

namespace Onyx3D
{
	public class Transform
	{
		public SceneObject SceneObject;
		
		private Vector4 mLocalScale = Vector4.One;
		private Vector4 mLocalPosition = Vector4.UnitW;
		private Quaternion mLocalRotation = Quaternion.Identity;

		private Matrix4 mBakedModel;
		private Vector4 mBakedPosition;
        
        public Vector3 Forward { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 Up { get; private set; }
        //private Vector4 mBakedRotation;

        public Vector3 LocalScale
		{
			get { return mLocalScale.Xyz; }
			set { mLocalScale = new Vector4(value, 1); }
		}

		public Vector3 LocalPosition
		{
			get { return mLocalPosition.Xyz; }
			set {
				mLocalPosition = new Vector4(value, 1);
				SetDirty();
			}
		}

		public Quaternion LocalRotation
		{
			get { return mLocalRotation; }
			set {
				mLocalRotation = value;
				SetDirty();
			}
		}

		public Vector3 Position
		{
			get { return mBakedPosition.Xyz; }
		}

		public Matrix4 ModelMatrix
		{
			get { return mBakedModel; }
		}

		public Transform(SceneObject sceneObject)
		{
			SceneObject = sceneObject;
			SetDirty();
		}


		public Matrix4 CalculateModelMatrix()
		{
			//TODO - Check if needs to be rebaked
			
			Matrix4 t = Matrix4.CreateTranslation(mLocalPosition.X, mLocalPosition.Y, mLocalPosition.Z);
			Matrix4 r = Matrix4.CreateFromQuaternion(mLocalRotation);
			Matrix4 s = Matrix4.CreateScale(mLocalScale.X, mLocalScale.Y, mLocalScale.Z);

			Matrix4 model = s * r * t;
			if (SceneObject.Parent != null)
				model *= SceneObject.Parent.Transform.CalculateModelMatrix() ;

			return model;
		}

		Matrix4 GetScaleMatrix()
		{
			return Matrix4.CreateScale(mLocalScale.X, mLocalScale.Y, mLocalScale.Z);
		}

		public Matrix4 GetTranslationMatrix()
		{
			return Matrix4.CreateTranslation(mLocalPosition.X, mLocalPosition.Y, mLocalPosition.Z);
		}

        public Matrix4 GetRotationMatrix()
        {
			return Matrix4.CreateFromQuaternion(mLocalRotation);
		}

        public Matrix4 GetYawMatrix(float rotY)
        {
			return Matrix4.CreateRotationY(rotY);
        }

		public Matrix4 GetPitchMatrix(float rotX)
		{
			return Matrix4.CreateRotationX(rotX);
		}

		public Matrix4 GetRollMatrix(float rotZ)
		{
			return Matrix4.CreateRotationZ(rotZ);
		}

		public void Rotate(Vector3 euler)
		{
			Quaternion rotation = Quaternion.FromEulerAngles(euler);
			Rotate(rotation);
		}

		public void Rotate(Quaternion rot)
		{
			mLocalRotation = mLocalRotation * rot;
			SetDirty();
		}

		public void Translate(Vector3 translation)
		{
			mLocalPosition += new Vector4(translation,1);
			SetDirty();
		}

		public Vector3 LocalToWorld(Vector3 point)
		{
			Vector4 world = new Vector4(point,1);
			world = world * mBakedModel;
			return new Vector3(world);
		}

		public void SetDirty()
		{
			mBakedModel = CalculateModelMatrix();
			mBakedPosition = mLocalPosition * mBakedModel;
            //mBakedRotation = GetModelMatrix() * mLocalRotation;

            Right = new Vector3(Vector4.UnitX * mBakedModel);
            Up = new Vector3(Vector4.UnitY * mBakedModel);
            Forward = new Vector3(Vector4.UnitZ * mBakedModel);


            for (int i=0; i < SceneObject.ChildCount; ++i)
			{
				SceneObject.GetChild(i).Transform.SetDirty();
			}
		}
	}
}
