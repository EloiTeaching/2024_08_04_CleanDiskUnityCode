//using UnityEngine;
//using System.Collections;
//using Windows.Kinect;

//public class CubeWallTest : MonoBehaviour
//{

//    public DepthSourceManager depthSource;
//    private KinectSensor sensor;
//    public ushort[] depthValue;

//    public int division = 16;
//    public float distanceBetween = 0.2f;
//    public float heightAdjuster = 0.1f;
//    public GameObject[] gride;
//    public GameObject prefab;

//    [Range(0, ushort.MaxValue)]
//    public int from, to;

//    void Start()
//    {

//        sensor = KinectSensor.GetDefault();
//        if (sensor != null)
//        {
//            FrameDescription descri = sensor.DepthFrameSource.FrameDescription;
//            gride = new GameObject[descri.Width * descri.Height];
//            int width = descri.Width;
//            int height = descri.Height;
//            float offsetX = ((float)width) * distanceBetween / 2f;
//            float offsety = ((float)height) * distanceBetween;
//            Vector3 pos = new Vector3();
//            for (int y = 0; y < descri.Height; y += division)
//            {
//                for (int x = 0; x < descri.Width; x += division)
//                {
//                    GameObject gamo = GameObject.Instantiate(prefab) as GameObject;
//                    gamo.transform.parent = this.transform;
//                    gride[DoubleCoord(ref x, ref y, ref width)] = gamo;

//                    pos.x = x * distanceBetween - offsetX;
//                    pos.y = -y * distanceBetween + offsety;
//                    gamo.transform.localPosition = pos;
//                }
//            }
//        }
//    }
//    void Update()
//    {

//        if (depthSource == null || sensor == null) return;
//        ushort from = (ushort)this.from;
//        ushort to = (ushort)this.to;

//        FrameDescription descri = sensor.DepthFrameSource.FrameDescription;
//        depthValue = depthSource.GetData();

//        int width = descri.Width;
//        int height = descri.Height;

//        for (int y = 0; y < descri.Height; y += division)
//        {
//            for (int x = 0; x < descri.Width; x += division)
//            {
//                int index = DoubleCoord(ref x, ref y, ref width);
//                ushort value = depthValue[index];
//                if (value > from && value < to)
//                {

//                    Vector3 pos = gride[index].transform.localPosition;
//                    pos.z = -value * distanceBetween * heightAdjuster;
//                    gride[index].transform.localPosition = pos;
//                    gride[index].renderer.enabled = true;
//                }
//                else
//                {

//                    gride[index].renderer.enabled = false;
//                }
//            }
//        }



//    }

//    public int DoubleCoord(ref int x, ref int y, ref int width) { return y * width + x; }
//}
