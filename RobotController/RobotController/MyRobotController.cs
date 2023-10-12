using System;
using System.Collections.Generic;
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

        #region public methods



        public string Hi()
        {

            string s = "hello world from our Robot Controller, we're Adrià Pérez and Abel Pujol";
            return s;

        }


        //EX1: this function will place the robot in the initial position

        public void PutRobotStraight(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3) {

            //todo: change this, use the function Rotate declared below

            MyVec rotationX;
            rotationX.x = 1;
            rotationX.y = 0;
            rotationX.z = 0;
            
            MyVec rotationY;
            rotationY.x = 0;
            rotationY.y = 1;
            rotationY.z = 0;

            MyVec rotationZ;
            rotationZ.x = 0;
            rotationZ.y = 0;
            rotationZ.z = 1;

            MyVec rotationNull;
            rotationNull.x = 0;
            rotationNull.y = 0;
            rotationNull.z = 0;
            
            rot0 = Rotate(NullQ, rotationY, 74);
            rot1 = Rotate(rot0, rotationX, -11);
            rot2 = Rotate(rot1, rotationX, 132);
            rot3 = Rotate(rot2, rotationX, -36);
        }



        //EX2: this function will interpolate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.


        public bool PickStudAnim(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            bool myCondition = false;
            //todo: add a check for your condition



            if (myCondition)
            {
                //todo: add your code here
                rot0 = NullQ;
                rot1 = NullQ;
                rot2 = NullQ;
                rot3 = NullQ;


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
            quaternionRotationZ.w = (float)Math.Cos(angle * axis.z * ((float)Math.PI / 180f));
            quaternionRotationZ.z = (float)Math.Sin(angle * axis.z * ((float)Math.PI / 180f));
            
            MyQuat quaternionRotationY = NullQ;
            quaternionRotationY.w = (float)Math.Cos(angle * axis.y * ((float)Math.PI / 180f));
            quaternionRotationY.y = (float)Math.Sin(angle * axis.y * ((float)Math.PI / 180f));
            
            MyQuat quaternionRotationX = NullQ;
            quaternionRotationX.w = (float)Math.Cos(angle * axis.x * ((float)Math.PI / 180f));
            quaternionRotationX.x = (float)Math.Sin(angle * axis.x * ((float)Math.PI / 180f));

            MyQuat result = MultiplyQuaternionsAxis(quaternionRotationZ, quaternionRotationX, quaternionRotationY);
            
            return Multiply(currentRotation, result);
        }
        
        //todo: add here all the functions needed

        internal MyQuat MultiplyQuaternionsAxis(MyQuat qz, MyQuat qx, MyQuat qy)
        {
            
            //rotation Z -> X -> Y

            MyQuat aux = Multiply(qz, qx);
            
            return Multiply(aux, qy);
        }

        #endregion
    }
}