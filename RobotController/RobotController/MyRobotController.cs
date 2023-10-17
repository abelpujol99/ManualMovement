using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RobotController
{

    public struct MyQuat
    {

        public float w;
        public float x;
        public float y;
        public float z;
        
        public static MyQuat operator* (MyQuat q1, MyQuat q2) 
        {
            MyQuat result;
            result.w = q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z;
            result.x = q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y;
            result.y = q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x;
            result.z = q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w;
            
            return result;
        }
    }

    public struct MyVec
    {

        public float x;
        public float y;
        public float z;
    }

    public class MyRobotController
    {

        private readonly float _initialRotationYJoint0 = 74;
        private readonly float _finalRotationYJoint0 = 40;

        private readonly float _initialRotationXJoint1 = -11;
        private readonly float _finalRotationXJoint1 = 2;
        
        private readonly float _initialRotationXJoint2 = 132;
        private readonly float _finalRotationXJoint2 = 93;
        
        private readonly float _initialRotationXJoint3 = -36;
        private readonly float _finalRotationXJoint3 = -11;
        
        private readonly float _initialRotationXJoint4 = 33;
        private readonly float _finalRotationXJoint4 = 90;

        private static MyQuat _twist;
        private static MyQuat _swing;
        
        private int _time1;
        private int _time2;
        private int _time3;
        private int _time4;

        private float _timeElapsed1;
        private float _timeElapsed2;

        #region public methods



        public string Hi()
        {

            string s = "hello world from our Robot Controller, we're Adrià Pérez and Abel Pujol";
            return s;

        }


        //EX1: this function will place the robot in the initial position

        public void PutRobotStraight(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3) {

            //todo: change this, use the function Rotate declared below
            
            rot0 = Rotate(NullQ, RotationY, _initialRotationYJoint0);
            rot1 = Rotate(rot0, RotationX, _initialRotationXJoint1);
            rot2 = Rotate(rot1, RotationX, _initialRotationXJoint2);
            rot3 = Rotate(rot2, RotationX, _initialRotationXJoint3);

            /*_swing = Rotate(rot2, RotationX, _initialRotationXJoint3);
            _twist = Rotate(_swing, RotationY, _initialRotationXJoint4);

            rot3 = Multiply(_twist, _swing);*/
            

            _time1 = 0;
            _time3 = 0;
            _timeElapsed1 = 0;
            _timeElapsed2 = 0;
        }



        //EX2: this function will interpolate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.


        public bool PickStudAnim(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            _time3 = 0;
            _timeElapsed2 = 0;
            
            if (_time1 == 0)
            {
                _time1 = TimeSinceMidnight;
            }
            

            if (_timeElapsed1 <= 1)
            {
                _time2 = TimeSinceMidnight;

                _timeElapsed1 = (_time2 - _time1) / 1000f;

                float degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationYJoint0,
                    _finalRotationYJoint0, _timeElapsed1);
                rot0 = Rotate(NullQ, RotationY, degreesToRotate);
                
                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint1,
                    _finalRotationXJoint1, _timeElapsed1);
                rot1 = Rotate(rot0, RotationX, degreesToRotate);
                
                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint2,
                    _finalRotationXJoint2, _timeElapsed1);
                rot2 = Rotate(rot1, RotationX, degreesToRotate);

                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint3,
                    _finalRotationXJoint3, _timeElapsed1);
                rot3 = Rotate(rot2, RotationX, degreesToRotate);

                /*degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint3,
                    _finalRotationXJoint3, _timeElapsed2);
                _swing = Rotate(rot2, RotationX, degreesToRotate);

                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_finalRotationXJoint4,
                    _initialRotationXJoint4, 1);
                _twist = Rotate(_swing, RotationY, degreesToRotate);

                rot3 = Multiply(_twist, _swing);*/

                return true;
            }
            
            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            return false;
        }


        //EX3: this function will calculate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.
        //the only difference wtih exercise 2 is that rot3 has a swing and a twist, where the swing will apply to joint3 and the twist to joint4

        public bool PickStudAnimVertical(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            _time1 = 0;
            _timeElapsed1 = 0;

            if (_time3 == 0)
            {
                _time3 = TimeSinceMidnight;
            }

            if (_timeElapsed2 <= 1)
            {
                _time4 = TimeSinceMidnight;

                _timeElapsed2 = (_time4 - _time3) / 1000f;

                float degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationYJoint0,
                    _finalRotationYJoint0, _timeElapsed2);
                rot0 = Rotate(NullQ, RotationY, degreesToRotate);
                
                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint1,
                    _finalRotationXJoint1, _timeElapsed2);
                rot1 = Rotate(rot0, RotationX, degreesToRotate);
                
                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint2,
                    _finalRotationXJoint2, _timeElapsed2);
                rot2 = Rotate(rot1, RotationX, degreesToRotate);

                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint3,
                    _finalRotationXJoint3, _timeElapsed2);
                _swing = Rotate(rot2, RotationX, degreesToRotate);

                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint4,
                    _finalRotationXJoint4, _timeElapsed2);
                _twist = Rotate(_swing, RotationY, degreesToRotate);

                rot3 = _twist * _swing;
                
                return true;
            }

            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            return false;
        }


        public static MyQuat GetSwing(MyQuat rot3)
        {
            return InverseQuaternion(GetTwist(rot3)) * rot3;
        }


        public static MyQuat GetTwist(MyQuat rot3)
        {
            return NormalizeQuaternion(rot3 * InverseQuaternion(_swing));
        }

        #endregion


        #region private and internal methods

        internal int TimeSinceMidnight { get { return (DateTime.Now.Hour * 3600000) + (DateTime.Now.Minute * 60000) + (DateTime.Now.Second * 1000) + DateTime.Now.Millisecond; } }


        private static MyQuat NullQ
        {
            get
            {
                MyQuat a;
                a.w = 1;
                a.x = 0;
                a.y = 0;
                a.z = 0;
                return a;

            }
        }

        private static MyVec RotationX
        {
            get
            {
                MyVec a;
                a.x = 1;
                a.y = 0;
                a.z = 0;
                return a;
            }
        }
        
        private static MyVec RotationY
        {
            get
            {
                MyVec a;
                a.x = 0;
                a.y = 1;
                a.z = 0;
                return a;
            }
        }

        internal float Deg2Rad(float angle)
        {
            return angle * ((float)Math.PI / 180f);
        }

        internal float Rad2Deg(float angle)
        {
            return angle * (180f / (float)Math.PI);
        }

        internal MyQuat Rotate(MyQuat currentRotation, MyVec axis, float angle)
        {

            angle /= 2;
            
            //todo: change this so it takes currentRotation, and calculate a new quaternion rotated by an angle "angle" radians along the normalized axis "axis"

            MyQuat quaternionRotationZ = NullQ;
            quaternionRotationZ.w = (float)Math.Cos(Deg2Rad(angle) * axis.z);
            quaternionRotationZ.z = (float)Math.Sin(Deg2Rad(angle) * axis.z);
            
            MyQuat quaternionRotationY = NullQ;
            quaternionRotationY.w = (float)Math.Cos(Deg2Rad(angle) * axis.y);
            quaternionRotationY.y = (float)Math.Sin(Deg2Rad(angle) * axis.y);
            
            MyQuat quaternionRotationX = NullQ;
            quaternionRotationX.w = (float)Math.Cos(Deg2Rad(angle) * axis.x);
            quaternionRotationX.x = (float)Math.Sin(Deg2Rad(angle) * axis.x);

            MyQuat result = quaternionRotationZ * quaternionRotationX * quaternionRotationY;
            
            return currentRotation * result;
        }

        internal static MyQuat InverseQuaternion(MyQuat quaternion)
        {
            MyQuat result;

            result.w = quaternion.w;
            result.x = -quaternion.x;
            result.y = -quaternion.y;
            result.z = -quaternion.z;

            return result;
        }

        internal static MyQuat NormalizeQuaternion(MyQuat quaternion)
        {
            float length = (float)Math.Sqrt(Math.Pow(quaternion.w, 2f) + Math.Pow(quaternion.x, 2f) + Math.Pow(quaternion.y, 2f) + Math.Pow(quaternion.z, 2f));

            quaternion.w /= length;
            quaternion.x /= length;
            quaternion.y /= length;
            quaternion.z /= length;

            return quaternion;
        }

        internal float SphericalInterpolation(MyVec startAngle, MyVec endAngle, float degreesBetweenStartAndEnd, float interpolationValue)
        {
            MyVec vector = default;

            vector.x = ((float)Math.Sin((1 - interpolationValue) * degreesBetweenStartAndEnd) / (float)Math.Sin(degreesBetweenStartAndEnd)) * startAngle.x
                        + ((float)Math.Sin(interpolationValue * degreesBetweenStartAndEnd)/(float)Math.Sin(degreesBetweenStartAndEnd)) * endAngle.x;
            
            vector.y = ((float)Math.Sin((1 - interpolationValue) * degreesBetweenStartAndEnd) / (float)Math.Sin(degreesBetweenStartAndEnd)) * startAngle.y
                        + ((float)Math.Sin(interpolationValue * degreesBetweenStartAndEnd)/(float)Math.Sin(degreesBetweenStartAndEnd)) * endAngle.y;

            return Rad2Deg((float)Math.Atan2(vector.y, vector.x));
        }

        internal float CalculateAngleBetweenAngles(float angle1, float angle2)
        {
            return Deg2Rad(Math.Abs(angle1 - angle2));
        }

        internal float CalculateDegreesRotationFromStartAngleToFinalAngle(float initialAngle, float finalAngle, float interpolationValue)
        {
            float degreesBetweenStartAndEnd = CalculateAngleBetweenAngles(initialAngle, finalAngle);
            
            MyVec startVector = default;
            startVector.x = (float)Math.Cos(Deg2Rad(initialAngle)); 
            startVector.y = (float)Math.Sin(Deg2Rad(initialAngle));
                
            MyVec endVector = default;
            endVector.x = (float)Math.Cos(Deg2Rad(finalAngle)); 
            endVector.y = (float)Math.Sin(Deg2Rad(finalAngle));
            
            return SphericalInterpolation(startVector, endVector, degreesBetweenStartAndEnd, interpolationValue); 
        }

        #endregion
    }
}