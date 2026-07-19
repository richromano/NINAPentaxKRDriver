using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace Rtg.NINA.NinaPentaxKRDriver.NinaPentaxKRDriverDrivers 
{
    public class PentaxKRCameraEnumerator
    {
        public ArrayList Cameras
        {
            get
            {
                ArrayList result = new ArrayList();

                CameraProvider.PentaxKRProfile.DeviceInfo info = new CameraProvider.PentaxKRProfile.DeviceInfo()
                {
                    Version = 1,
                    Model = "K-5II",
                    SerialNumber = "5",
                    DeviceName = "K-5II"
                };

                PKCamera camera = new PKCamera();

                //camera.Model = "K-5II";
                //camera.SerialNumber = "5";

                /*info.DeviceName = camera.Model;
                info.SerialNumber = camera.SerialNumber;*/
                if(info.DeviceName!=null)
                	result.Add(info);
                return result;
            }
        }
    }
}