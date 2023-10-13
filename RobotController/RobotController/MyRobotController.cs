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
        
        private readonly float _initialRotationXJoint4 = -11;
        private readonly float _finalRotationXJoint4 = -11;

        private int _time1;
        private int _time2;

        private float _timeElapsed;

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

            _time1 = 0;
            _timeElapsed = 0;
        }



        //EX2: this function will interpolate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.


        public bool PickStudAnim(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            //todo: add a check for your condition

            if (_time1 == 0)
            {
                _time1 = TimeSinceMidnight;
            }
            

            if (_timeElapsed <= 1)
            {
                //todo: add your code here
            
                _time2 = TimeSinceMidnight;

                _timeElapsed = (_time2 - _time1) / 1000f;

                float degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationYJoint0,
                    _finalRotationYJoint0, _timeElapsed);
                rot0 = Rotate(NullQ, RotationY, degreesToRotate);
                
                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint1,
                    _finalRotationXJoint1, _timeElapsed);
                rot1 = Rotate(rot0, RotationX, degreesToRotate);
                
                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint2,
                    _finalRotationXJoint2, _timeElapsed);
                rot2 = Rotate(rot1, RotationX, degreesToRotate);
                
                degreesToRotate = CalculateDegreesRotationFromStartAngleToFinalAngle(_initialRotationXJoint3,
                    _finalRotationXJoint3, _timeElapsed);
                rot3 = Rotate(rot2, RotationX, degreesToRotate);

                return true;
            }

            //todo: remove this once your code works.
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

            bool myCondition = false;
            //todo: add a check for your condition



            while (myCondition)
            {
                //todo: add your code here


            }

            //todo: remove this once your code works.
            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            return false;
        }


        public static MyQuat GetSwing(MyQuat rot3)
        {
            //todo: change the return value for exercise 3
            return NullQ;

        }


        public static MyQuat GetTwist(MyQuat rot3)
        {
            //todo: change the return value for exercise 3
            return NullQ;

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
        
        private static MyVec RotationZ
        {
            get
            {
                MyVec a;
                a.x = 0;
                a.y = 0;
                a.z = 1;
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

        internal MyQuat Multiply(MyQuat q1, MyQuat q2) {

            
            MyQuat result;
            result.w = q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z;
            result.x = q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y;
            result.y = q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x;
            result.z = q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w;
            
            return result;

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

            MyQuat result = MultiplyQuaternionsAxis(quaternionRotationZ, quaternionRotationX, quaternionRotationY);
            
            return Multiply(currentRotation, result);
        }
        
        //todo: add here all the functions needed

        internal MyQuat MultiplyQuaternionsAxis(MyQuat qz, MyQuat qx, MyQuat qy)
        {
            
            //order to multiply rotation quaternions Z -> X -> Y

            MyQuat aux = Multiply(qz, qx);
            
            return Multiply(aux, qy);
        }

        internal float SphericalInterpolation(MyVec startAngle, MyVec endAngle, float degreesBetweenStartAndEnd, float interpolationValue)
        {
            float result;
            
            MyVec vector = default;

            vector.x = ((float)Math.Sin((1 - interpolationValue) * degreesBetweenStartAndEnd) / (float)Math.Sin(degreesBetweenStartAndEnd)) * startAngle.x
                        + ((float)Math.Sin(interpolationValue * degreesBetweenStartAndEnd)/(float)Math.Sin(degreesBetweenStartAndEnd)) * endAngle.x;
            
            vector.y = ((float)Math.Sin((1 - interpolationValue) * degreesBetweenStartAndEnd) / (float)Math.Sin(degreesBetweenStartAndEnd)) * startAngle.y
                        + ((float)Math.Sin(interpolationValue * degreesBetweenStartAndEnd)/(float)Math.Sin(degreesBetweenStartAndEnd)) * endAngle.y;

            return result = Rad2Deg((float)Math.Atan2(vector.y, vector.x));
            
            //return 180f - Math.Abs(result);
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