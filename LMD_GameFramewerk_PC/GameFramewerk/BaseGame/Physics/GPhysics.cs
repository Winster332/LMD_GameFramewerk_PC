using System;
using System.Drawing;
using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using LMD_GameFramewerk_PC.GameFramewerk.UI;

namespace LMD_GameFramewerk_PC.GameFramewerk.BaseGame.Physics
{
	public class GPhysics : IPhysics
	{
		private const float metric = 30f;
		private IGame game;
		private World world;
		private Solver solver;

		public GPhysics(IGame game)
		{
			this.game = game;
		}

		public void Initialize(float x, float y, float w, float h, float grav_x, float grav_y, bool doSleep)
		{
			solver = new Solver(game);

			AABB ab = new AABB();
			ab.LowerBound.Set(x, y);
			ab.UpperBound.Set(w, h);

			world = new World(ab, new Vec2(grav_x, grav_y), doSleep);
			world.SetContactListener(solver);
		}

		public void Dispose()
		{
			for (Body list = world.GetBodyList(); list != null; list = list.GetNext())
			{
				world.DestroyBody(list);
			}
			//	world.Dispose();
		}

		public void Step(float dt, int iterat)
		{
			world.Step(1f / 30f, iterat, iterat);
		}

		public void Draw()
		{
			for (Body list = world.GetBodyList(); list != null; list = list.GetNext())
			{
				InfoBody info = (InfoBody)list.GetUserData();

				if (info != null)
				{
					info.image.SetX(list.GetPosition().X * metric);
					info.image.SetY(list.GetPosition().Y * metric);
					info.image.SetAndle(list.GetAngle());

					info.image.Draw();
				}
			}
		}

		public Solver GetSolver()
		{
			return solver;
		}

		#region Rectangle
		public InfoBody AddRect(float x, float y, float w, float h, float angle, float density,
			float friction, float restetution, uint texture, Object userDate = null)
		{
			GImage g_image = new GImage(game);
			g_image.SetTexture(texture);
			g_image.SetWidth(w);
			g_image.SetHeight(h);

			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x / metric, y / metric);
			bDef.Angle = angle;

			PolygonDef pDef = new PolygonDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.SetAsBox(w / metric / 2, h / metric / 2);

			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();

			InfoBody info = new InfoBody();
			info.image = g_image;
			info.body = body;
			info.userDate = userDate;

			body.SetUserData(info);

			return info;
		}

		public InfoBody AddRect(float x, float y, float w, float h, float angle, float density,
			float friction, float restetution, float mass, GImage image, Object userDate = null)
		{
			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x / metric, y / metric);
			bDef.Angle = angle;

			PolygonDef pDef = new PolygonDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.SetAsBox(w / metric / 2, h / metric / 2);

			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();

			float Inertia = body.GetInertia();
			MassData md = new MassData();
			md.I = Inertia;
			md.Mass = mass;
			body.SetMass(md);

			InfoBody info = new InfoBody();
			info.image = image;
			info.body = body;
			info.userDate = userDate;

			body.SetUserData(info);

			return info;
		}

		public InfoBody AddRect(float x, float y, float w, float h, float angle, float density,
			float friction, float restetution, float mass, GImage image, bool IsBullet = true,
			bool IsSensor = false, bool AllowSleep = false, short group_index = 1, Object userDate = null)
		{
			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x / metric, y / metric);
			bDef.Angle = angle;
			bDef.AllowSleep = AllowSleep;
			bDef.IsBullet = IsBullet;

			PolygonDef pDef = new PolygonDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.SetAsBox(w / metric / 2, h / metric / 2);
			pDef.Filter.GroupIndex = group_index;
			pDef.IsSensor = IsSensor;

			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();

			float Inertia = body.GetInertia();
			MassData md = new MassData();
			md.I = Inertia;
			md.Mass = mass;
			body.SetMass(md);

			InfoBody info = new InfoBody();
			info.image = image;
			info.body = body;
			info.userDate = userDate;

			body.SetUserData(info);

			return info;
		}
		#endregion
		#region Circle
		public InfoBody AddCircle(float x, float y, float radius, float angle, float density,
			float friction, float restetution, uint texture, Object userDate = null)
		{
			GImage g_image = new GImage(game);
			g_image.SetTexture(texture);
			g_image.SetWidth(radius * 2);
			g_image.SetHeight(radius * 2);

			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x / metric, y / metric);
			bDef.Angle = angle;

			CircleDef pDef = new CircleDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.Radius = radius / metric;

			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();

			InfoBody info = new InfoBody();
			info.image = g_image;
			info.body = body;
			info.userDate = userDate;

			body.SetUserData(info);

			return info;
		}

		public InfoBody AddCircle(float x, float y, float radius, float angle, float density,
			float friction, float restetution, float mass, GImage image, Object userDate = null)
		{
			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x / metric, y / metric);
			bDef.Angle = angle;

			CircleDef pDef = new CircleDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.Radius = radius / metric;

			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();

			float Inertia = body.GetInertia();
			MassData md = new MassData();
			md.I = Inertia;
			md.Mass = mass;
			body.SetMass(md);

			InfoBody info = new InfoBody();
			info.image = image;
			info.body = body;
			info.userDate = userDate;

			body.SetUserData(info);

			return info;
		}

		public InfoBody AddCircle(float x, float y, float radius, float angle, float density,
			float friction, float restetution, float mass, GImage image, bool IsBullet = true,
			bool IsSensor = false, bool AllowSleep = false, short group_index = 1, Object userDate = null)
		{
			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x / metric, y / metric);
			bDef.Angle = angle;
			bDef.AllowSleep = AllowSleep;
			bDef.IsBullet = IsBullet;

			CircleDef pDef = new CircleDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.Radius = radius / metric;
			pDef.Filter.GroupIndex = group_index;
			pDef.IsSensor = IsSensor;

			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();

			float Inertia = body.GetInertia();
			MassData md = new MassData();
			md.I = Inertia;
			md.Mass = mass;
			body.SetMass(md);

			InfoBody info = new InfoBody();
			info.image = image;
			info.body = body;
			info.userDate = userDate;

			body.SetUserData(info);

			return info;
		}
		#endregion
		#region Vert
		public InfoBody AddVert(float x, float y, Vec2[] vert, float angle, float density,
			float friction, float restetution, uint texture, Object userDate = null)
		{
			float max_w = vert[0].X;
			float max_h = vert[0].Y;

			for (int i = 0; i < vert.Length; i++)
			{
				if (vert[i].X > max_w)
					max_w = vert[i].X;

				if (vert[i].Y > max_h)
					max_h = vert[i].Y;

				vert[i].X /= 2;
				vert[i].Y /= 2;

				vert[i].X /= metric;
				vert[i].Y /= metric;
			}

			GImage g_image = new GImage(game);
			g_image.SetTexture(texture);
			g_image.SetWidth(max_w);
			g_image.SetHeight(max_h);

			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x / metric, y / metric);
			bDef.Angle = angle;

			PolygonDef pDef = new PolygonDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.SetAsBox(max_w / metric / 2, max_h / metric / 2);
			pDef.Vertices = vert;

			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();

			InfoBody info = new InfoBody();
			info.image = g_image;
			info.body = body;
			info.userDate = userDate;

			body.SetUserData(info);

			return info;
		}

		public InfoBody AddVert(float x, float y, Vec2[] vert, float angle, float density,
			float friction, float restetution, float mass, GImage image, Object userDate = null)
		{
			for (int i = 0; i < vert.Length; i++)
			{
				vert[i].X /= 2;
				vert[i].Y /= 2;

				vert[i].X /= metric;
				vert[i].Y /= metric;
			}

			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x / metric, y / metric);
			bDef.Angle = angle;

			PolygonDef pDef = new PolygonDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.SetAsBox(image.GetWidth() / metric / 2, image.GetHeight() / metric / 2);
			pDef.Vertices = vert;

			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();

			float Inertia = body.GetInertia();
			MassData md = new MassData();
			md.I = Inertia;
			md.Mass = mass;
			body.SetMass(md);

			InfoBody info = new InfoBody();
			info.image = image;
			info.body = body;
			info.userDate = userDate;

			body.SetUserData(info);

			return info;
		}

		public InfoBody AddVert(float x, float y, Vec2[] vert, float angle, float density,
			float friction, float restetution, float mass, GImage image, bool IsBullet = true,
			bool IsSensor = false, bool AllowSleep = false, short group_index = 1, Object userDate = null)
		{
			for (int i = 0; i < vert.Length; i++)
			{
				vert[i].X /= 2;
				vert[i].Y /= 2;

				vert[i].X /= metric;
				vert[i].Y /= metric;
			}

			BodyDef bDef = new BodyDef();
			bDef.Position.Set(x / metric, y / metric);
			bDef.Angle = angle;
			bDef.AllowSleep = AllowSleep;
			bDef.IsBullet = IsBullet;

			PolygonDef pDef = new PolygonDef();
			pDef.Restitution = restetution;
			pDef.Friction = friction;
			pDef.Density = density;
			pDef.SetAsBox(image.GetWidth() / metric / 2, image.GetHeight() / metric / 2);
			pDef.Vertices = vert;
			pDef.Filter.GroupIndex = group_index;
			pDef.IsSensor = IsSensor;

			Body body = world.CreateBody(bDef);
			body.CreateShape(pDef);
			body.SetMassFromShapes();

			float Inertia = body.GetInertia();
			MassData md = new MassData();
			md.I = Inertia;
			md.Mass = mass;
			body.SetMass(md);

			InfoBody info = new InfoBody();
			info.image = image;
			info.body = body;
			info.userDate = userDate;

			body.SetUserData(info);

			return info;
		}
		#endregion
		#region Removes
		public void RemoveBody(InfoBody infoBody, bool remove_image = true)
		{
			if (remove_image)
				infoBody.image.Dispose();
			world.DestroyBody(infoBody.body);
		}

		public void RemoveBody(Body body)
		{
			world.DestroyBody(body);
		}
		#endregion
		#region Create joints
		#region Revolute joints
		public Joint AddJoint(Body b1, Body b2, float x, float y, bool enableLimit = false)
		{
			RevoluteJointDef jd = new RevoluteJointDef();
			jd.Initialize(b1, b2, new Vec2(x / metric, y / metric));
			jd.EnableLimit = enableLimit;

			Joint joint = world.CreateJoint(jd);

			return joint;
		}

		public Joint AddJoint(Body b1, Body b2, float x, float y, bool collideConnected, bool enableMotor = false,
			float motor_speed = 0, float maxMotorTorque = float.MaxValue)
		{
			RevoluteJointDef jd = new RevoluteJointDef();
			jd.Initialize(b1, b2, new Vec2(x / metric, y / metric));
			jd.CollideConnected = collideConnected;
			jd.EnableMotor = enableMotor;
			jd.MotorSpeed = motor_speed;
			jd.MaxMotorTorque = maxMotorTorque;

			Joint joint = world.CreateJoint(jd);

			return joint;
		}

		public Joint AddJoint(Body b1, Body b2, float x, float y, bool collideConnected, float upperAngle, float lowerAngle, float referenceAngle = 0)
		{
			RevoluteJointDef jd = new RevoluteJointDef();
			jd.Initialize(b1, b2, new Vec2(x / metric, y / metric));
			jd.CollideConnected = collideConnected;
			jd.EnableMotor = true;
			jd.EnableLimit = true;

			jd.LowerAngle = lowerAngle;
			jd.UpperAngle = upperAngle;
		
			jd.ReferenceAngle = referenceAngle;

			Joint joint = world.CreateJoint(jd);

			return joint;
		}
		#endregion
		#region Distance joints
		public Joint AddDistanceJoint(Body b1, Body b2, float x1, float y1, float x2, float y2, 
			bool collideConnected = true, float hz = 1f)
		{
			DistanceJointDef jd = new DistanceJointDef();
			jd.Initialize(b1, b2, new Vec2(x1 / metric, y1 / metric), new Vec2(x2 / metric, y2 / metric));
			jd.FrequencyHz = 0.3f;
			jd.CollideConnected = collideConnected;
			jd.FrequencyHz = hz;

			Joint joint = world.CreateJoint(jd);

			return joint;
		}

		public Joint AddDistanceJoint(Body b1, Body b2, float x1, float y1, float x2, float y2, float length,
			bool collideConnected = true, float hz = 1f)
		{
			DistanceJointDef jd = new DistanceJointDef();
			jd.Initialize(b1, b2, new Vec2(x1 / metric, y1 / metric), new Vec2(x2 / metric, y2 / metric));
			jd.FrequencyHz = 0.3f;
			jd.CollideConnected = collideConnected;
			jd.FrequencyHz = hz;
			jd.Length = length;

			Joint joint = world.CreateJoint(jd);

			return joint;
		}
		public Joint AddDistanceJoint(Body b1, Body b2, float x1, float y1, float x2, float y2, float length,
			bool collideConnected = true, float hz = 1f, float dampingRatio = 0)
		{
			DistanceJointDef jd = new DistanceJointDef();
			jd.Initialize(b1, b2, new Vec2(x1 / metric, y1 / metric), new Vec2(x2 / metric, y2 / metric));
			jd.FrequencyHz = 0.3f;
			jd.CollideConnected = collideConnected;
			jd.FrequencyHz = hz;
			jd.Length = length;
			jd.DampingRatio = dampingRatio;

			Joint joint = world.CreateJoint(jd);

			return joint;
		}
		#endregion
		public Joint AddJoint(Body b1, Body b2, float x, float y,float ddd)
		{
			RevoluteJointDef jd = new RevoluteJointDef();
			jd.Initialize(b1, b2, new Vec2(x / metric, y / metric));
			Joint joint = world.CreateJoint(jd);
			
			return joint;
		}
		#endregion
	}
}