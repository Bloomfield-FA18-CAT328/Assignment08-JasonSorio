using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TS
{
    public enum DrawMode { Show, ShowAll, HideAll }

    public class TagSensor
    {
        //public vars
        public DrawMode DrawMode { get; set; }
        public DrawMode ShowLines = DrawMode.Show;
        public float Precision;

        //private vars
        private float fov;
        private float minRange;
        private float maxRange;
        private float offsetY;
        private Quaternion offset3D;
        private bool hide;
        private GameObject receivingGameObject;
        private bool disabled;

        //constructors
        public TagSensor()
        {
        }

        public TagSensor(GameObject receiver)
        {
            this.receivingGameObject = receiver;
            this.fov = 0;
            this.minRange = 0;
            this.maxRange = 0;
            this.offsetY = 0;
            this.offset3D = new Quaternion();
            this.hide = false;
        }

        public TagSensor(GameObject receiver, float minRange, float maxRange, float FieldOfView)
        {
            this.receivingGameObject = receiver;
            this.minRange = minRange;
            this.maxRange = maxRange;
            this.fov = FieldOfView;
            this.maxRange = maxRange;
            this.offsetY = 0;
            this.offset3D = new Quaternion();
            this.hide = false;
        }

        public TagSensor(GameObject receiver, float minRange, float maxRange, float FieldOfView, float OffsetY)
        {
            this.receivingGameObject = receiver;
            this.minRange = minRange;
            this.maxRange = maxRange;
            this.fov = FieldOfView;
            this.offsetY = OffsetY;
            this.offset3D = Quaternion.AngleAxis(offsetY, receivingGameObject.transform.up);
            this.hide = false;
        }

        public TagSensor(GameObject receiver, float minRange, float maxRange, float FieldOfView, Quaternion Offset3D)
        {
            this.receivingGameObject = receiver;
            this.minRange = minRange;
            this.maxRange = maxRange;
            this.fov = FieldOfView;
            this.offsetY = 0;
            this.offset3D = Offset3D;
            this.hide = false;
        }

        /* get set methods
        ReceivingGameObject
        FOV
        Range
        OffsetY
        Offset3D
        Hide
        Active
        */

        public GameObject ReceivingGameObject
        {
            get { return receivingGameObject; }
            private set { receivingGameObject = value; }
        }

        public float FOV
        {
            get { return fov; }
            set { fov = value; }
        }

        public float MinRange
        {
            get { return minRange; }
            set { minRange = value; }
        }

        public float MaxRange
        {
            get { return maxRange; }
            set { maxRange = value; }
        }

        public float OffsetY
        {
            get { return offsetY; }
            set { offsetY = value; }
        }

        public Quaternion Offset3D
        {
            get { return offset3D; }
            set { offset3D = value; }
        }

        public bool Hide
        {
            get { return hide; }
            set { hide = value; }
        }

        public bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }



        //
        // detection methods
        //todo: figure out why OffsetY isn't offsetting OnDetect
        public bool OnDetect(string t) // when true, repeatedly goes from true to false for some reason
        {
            if(!Disabled)
            { 
                GameObject[] tag = GameObject.FindGameObjectsWithTag(t);
                Vector3 dir = receivingGameObject.transform.forward;

                foreach (GameObject items in tag)
                {
                    //float angle = DotToAngle(offset3D * receivingGameObject.transform.forward, Vector3.Normalize(items.transform.position - receivingGameObject.transform.position));
                    float distance = Vector3.Distance(receivingGameObject.transform.position, items.transform.position);
                    float dot = Vector3.Dot(Quaternion.AngleAxis(offsetY, receivingGameObject.transform.up) * receivingGameObject.transform.forward,/**/ Vector3.Normalize(items.transform.position - receivingGameObject.transform.position));
                    float compare = Vector3.Dot(Quaternion.AngleAxis(offsetY, receivingGameObject.transform.up) * receivingGameObject.transform.forward,/*why fov?*/ Quaternion.AngleAxis(offsetY + fov / 2, receivingGameObject.transform.up) * receivingGameObject.transform.forward);


                    if (dot >= compare & distance <= maxRange & distance >= minRange)
                    //if (angle <= fov / 2 & distance <= maxRange & distance >= minRange)
                    {
                        return true;
                    }
                    else
                    {
                        //Debug.Log(dot + "," + fov + "," + distance);
                        //Debug.Break();
                        return false;
                    }
                }
            }

            return false;
        }

        //todo: write method OnDetectLOS(string) using raycast






        // visualization methods

        public void DrawLines()
        {
            if (ShowLines != DrawMode.HideAll & hide == false)
            {
                Debug.DrawRay(receivingGameObject.transform.position, (Quaternion.AngleAxis(offsetY, receivingGameObject.transform.up) * receivingGameObject.transform.forward) * maxRange, Color.blue);

                Debug.DrawRay(receivingGameObject.transform.position + Vector3.Normalize(offset3D * receivingGameObject.transform.forward) * minRange, Quaternion.AngleAxis(offsetY + 90, receivingGameObject.transform.up) * receivingGameObject.transform.forward, Color.red);
                Debug.DrawRay(receivingGameObject.transform.position + Vector3.Normalize(offset3D * receivingGameObject.transform.forward) * minRange, Quaternion.AngleAxis(offsetY - 90, receivingGameObject.transform.up) * receivingGameObject.transform.forward, Color.red);
                Debug.DrawRay(receivingGameObject.transform.position + Vector3.Normalize(offset3D * receivingGameObject.transform.forward) * maxRange, Quaternion.AngleAxis(offsetY + 90, receivingGameObject.transform.up) * receivingGameObject.transform.forward, Color.red);
                Debug.DrawRay(receivingGameObject.transform.position + Vector3.Normalize(offset3D * receivingGameObject.transform.forward) * maxRange, Quaternion.AngleAxis(offsetY - 90, receivingGameObject.transform.up) * receivingGameObject.transform.forward, Color.red);

                Debug.DrawRay(receivingGameObject.transform.position, (Quaternion.AngleAxis((fov / 2) + offsetY, receivingGameObject.transform.up) * receivingGameObject.transform.forward) * maxRange, Color.green);
                Debug.DrawRay(receivingGameObject.transform.position, (Quaternion.AngleAxis((-fov / 2) + offsetY, receivingGameObject.transform.up) * receivingGameObject.transform.forward) * maxRange, Color.green);

            }
        }

        public void GridLines()
        {
            Precision = 5;

            for (float degrees = 0; degrees <= 360; degrees += Precision)
            {
                if (ShowLines != DrawMode.HideAll & hide == false)
                {
                    float donutRadius = maxRange - minRange;
                    float dot = Vector3.Dot(Quaternion.AngleAxis(offsetY, receivingGameObject.transform.up) * receivingGameObject.transform.forward, Quaternion.AngleAxis(degrees, receivingGameObject.transform.up) * receivingGameObject.transform.forward);
                    Quaternion directionToCompare = Quaternion.AngleAxis(degrees, receivingGameObject.transform.up);
                    float compare = Vector3.Dot(Quaternion.AngleAxis(offsetY, receivingGameObject.transform.up) * receivingGameObject.transform.forward,/**/ Quaternion.AngleAxis(offsetY + fov / 2, receivingGameObject.transform.up) * receivingGameObject.transform.forward);


                    if (dot >= compare)
                    {
                        Debug.DrawRay(receivingGameObject.transform.position + Vector3.Normalize(directionToCompare * receivingGameObject.transform.forward) * minRange, directionToCompare * receivingGameObject.transform.forward * donutRadius, Color.blue);
                    }
                }
            }
        }

        static float RotationIndex;
        public void SweepLines(float rate) //rate is degrees per tick
        {
            RotationIndex = (RotationIndex >= 360 ? RotationIndex -= 360 : RotationIndex += rate);
            if (ShowLines != DrawMode.HideAll & hide == false)
            {
                float donutRadius = maxRange - minRange;

                float angleA = DotToAngle(Quaternion.AngleAxis(offsetY, receivingGameObject.transform.up) * receivingGameObject.transform.forward, Quaternion.AngleAxis(RotationIndex, receivingGameObject.transform.up) * receivingGameObject.transform.forward);
                Quaternion direction1 = Quaternion.AngleAxis(RotationIndex, receivingGameObject.transform.up);

                //too repetive
                //todo: make an array of these values instead
                float angleB = DotToAngle(Quaternion.AngleAxis(offsetY, receivingGameObject.transform.up) * receivingGameObject.transform.forward, Quaternion.AngleAxis(RotationIndex + 180, receivingGameObject.transform.up) * receivingGameObject.transform.forward);
                Quaternion direction2 = Quaternion.AngleAxis(RotationIndex + 180, receivingGameObject.transform.up);

                if (angleA <= fov / 2)
                {
                    Debug.DrawRay(receivingGameObject.transform.position + Vector3.Normalize(direction1 * receivingGameObject.transform.forward) * minRange, direction1 * receivingGameObject.transform.forward * donutRadius, Color.blue);
                }

                if (angleB <= fov / 2)
                {
                    Debug.DrawRay(receivingGameObject.transform.position + Vector3.Normalize(direction2 * receivingGameObject.transform.forward) * minRange, direction2 * receivingGameObject.transform.forward * donutRadius, Color.blue);
                }
            }
        }

        /*
        void foo()
        {
            if (ShowLines == DrawMode.ShowAll)
            {
                DrawLines();
            }
        }*/

        public float DotToAngle(Vector3 A, Vector3 B)
        {
            float angle = Mathf.Acos(Vector3.Dot(A, B) / (Vector3.Magnitude(A) * Vector3.Magnitude(B))) * Mathf.Rad2Deg;
            return angle;
        }

        public float CrudeDotToAngle(Vector3 A, Vector3 B) //very fast, but also very inaccurate
        {
            float angle = 90 * (-Vector3.Dot(A, B) + 1);
            return angle;
        }
    }
}
