using PKTriggerCord;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static System.Net.Mime.MediaTypeNames;
using NINA.Core.Enum;
using NINA.Core.Utility;

// With code from ASCOM DSLR 
// https://github.com/FearL0rd/ASCOM.DSLR


/*
 *     { 0x12aa2, "*ist DS",     true,  true,  true,  false, false, false, 264, 3, {6, 4, 2},       5, 4000, 200, 3200, 200,  3200,  PSLR_JPEG_IMAGE_TONE_BRIGHT,           false, 11, ipslr_status_parse_istds},
    { 0x12cd2, "K20D",        false, true,  true,  false, false, false, 412, 4, {14, 10, 6, 2},  7, 4000, 100, 3200, 100,  6400,  PSLR_JPEG_IMAGE_TONE_MONOCHROME,       true,  11, ipslr_status_parse_k20d},
    { 0x12c1e, "K10D",        false, true,  true,  false, false, false, 392, 3, {10, 6, 2},      7, 4000, 100, 1600, 100,  1600,  PSLR_JPEG_IMAGE_TONE_BRIGHT,           false, 11, ipslr_status_parse_k10d},
    { 0x12c20, "GX10",        false, true,  true,  false, false, false, 392, 3, {10, 6, 2},      7, 4000, 100, 1600, 100,  1600,  PSLR_JPEG_IMAGE_TONE_BRIGHT,           false, 11, ipslr_status_parse_k10d},
    { 0x12cd4, "GX20",        false, true,  true,  false, false, false, 412, 4, {14, 10, 6, 2},  7, 4000, 100, 3200, 100,  6400,  PSLR_JPEG_IMAGE_TONE_MONOCHROME,       true,  11, ipslr_status_parse_k20d},
    { 0x12dfe, "K-x",         false, true,  true,  false, false, false, 436, 3, {12, 10, 6, 2},  9, 6000, 200, 6400, 100, 12800,  PSLR_JPEG_IMAGE_TONE_MONOCHROME,       true,  11, ipslr_status_parse_kx}, //muted: bug
    { 0x12cfa, "K200D",       false, true,  true,  false, false, false, 408, 3, {10, 6, 2},      9, 4000, 100, 1600, 100,  1600,  PSLR_JPEG_IMAGE_TONE_MONOCHROME,       true,  11, ipslr_status_parse_k200d},
    { 0x12db8, "K-7",         false, true,  true,  false, false, false, 436, 4, {14, 10, 6, 2},  9, 8000, 100, 3200, 100,  6400,  PSLR_JPEG_IMAGE_TONE_MUTED,            true,  11, ipslr_status_parse_kx},
    { 0x12e6c, "K-r",         false, true,  true,  false, false, false, 440, 3, {12, 10, 6, 2},  9, 6000, 200,12800, 100, 25600,  PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS,    true,  11, ipslr_status_parse_kr},
    { 0x12e76, "K-5",         false, true,  true,  false, false, false, 444, 4, {16, 10, 6, 2},  9, 8000, 100,12800,  80, 51200,  PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS,    true,  11, ipslr_status_parse_k5},
    { 0x12d72, "K-2000",      false, true,  true,  false, false, false, 412, 3, {10, 6, 2},      9, 4000, 100, 3200, 100,  3200,  PSLR_JPEG_IMAGE_TONE_MONOCHROME,       true,  11, ipslr_status_parse_km},
    { 0x12d73, "K-m",         false, true,  true,  false, false, false, 412, 3, {10, 6, 2},      9, 4000, 100, 3200, 100,  3200,  PSLR_JPEG_IMAGE_TONE_MONOCHROME,       true,  11, ipslr_status_parse_km},
    { 0x12f52, "K-30",        false, true,  false, false, false, false, 452, 3, {16, 12, 8, 5},  9, 6000, 100,12800, 100, 25600,  PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS,    true,  11, ipslr_status_parse_k30},
    { 0x12ef8, "K-01",        false, true,  true,  false, false, false, 452, 3, {16, 12, 8, 5},  9, 4000, 100,12800, 100, 25600,  PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS,    true,  11, ipslr_status_parse_k01},
    { 0x12f70, "K-5II",       false, true,  true,  false, false, false, 444,  4, {16, 10, 6, 2}, 9, 8000, 100, 12800, 80, 51200,  PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS,    true,  11, ipslr_status_parse_k5},
    { 0x12f71, "K-5IIs",      false, true,  true,  false, false, false, 444,  4, {16, 10, 6, 2}, 9, 8000, 100, 12800, 80, 51200,  PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS,    true,  11, ipslr_status_parse_k5},
    { 0x12fb6, "K-50",        false, true,  true,  false, false, false, 452,  4, {16, 12, 8, 5}, 9, 6000, 100, 51200, 100, 51200, PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS,    true,  11, ipslr_status_parse_k50},
    { 0x12fc0, "K-3",         false, true,  true,  false, false, true,  452,  4, {24, 14, 6, 2}, 9, 8000, 100, 51200, 100, 51200, PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS,    true,  27, ipslr_status_parse_k3},
    { 0x1309c, "K-3II",       false, false, true,  true,  false, true,  452,  4, {24, 14, 6, 2}, 9, 8000, 100, 51200, 100, 51200, PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS,    true,  27, ipslr_status_parse_k3},
    { 0x12fca, "K-500",       false, true,  true,  false, false, false, 452,  3, {16, 12, 8, 5}, 9, 6000, 100, 51200, 100, 51200, PSLR_JPEG_IMAGE_TONE_CROSS_PROCESSING, true,  11, ipslr_status_parse_k500},
    // only limited support from here
    { 0x12994, "*ist D",      true,  true,  true,  false, false, false, 0,   3, {6, 4, 2}, 3, 4000, 200, 3200, 200, 3200, PSLR_JPEG_IMAGE_TONE_NONE,   false, 11, NULL},   // buffersize: 264
    { 0x12b60, "*ist DS2",    true,  true,  true,  false, false, false, 0,   3, {6, 4, 2}, 5, 4000, 200, 3200, 200, 3200, PSLR_JPEG_IMAGE_TONE_BRIGHT, false, 11, NULL},
    { 0x12b1a, "*ist DL",     true,  true,  true,  false, false, false, 0,   3, {6, 4, 2}, 5, 4000, 200, 3200, 200, 3200, PSLR_JPEG_IMAGE_TONE_BRIGHT, false, 11, NULL},
    { 0x12b80, "GX-1L",       true,  true,  true,  false, false, false, 0,   3, {6, 4, 2}, 5, 4000, 200, 3200, 200, 3200, PSLR_JPEG_IMAGE_TONE_BRIGHT, false, 11, NULL},
    { 0x12b9d, "K110D",       false, true,  true,  false, false, false, 0,   3, {6, 4, 2}, 5, 4000, 200, 3200, 200, 3200, PSLR_JPEG_IMAGE_TONE_BRIGHT, false, 11, NULL},
    { 0x12b9c, "K100D",       true,  true,  true,  false, false, false, 0,   3, {6, 4, 2}, 5, 4000, 200, 3200, 200, 3200, PSLR_JPEG_IMAGE_TONE_BRIGHT, false, 11, NULL},
    { 0x12ba2, "K100D Super", true,  true,  true,  false, false, false, 0,   3, {6, 4, 2}, 5, 4000, 200, 3200, 200, 3200, PSLR_JPEG_IMAGE_TONE_BRIGHT, false, 11, NULL},
    { 0x1301a, "K-S1",        false, true,  true,  false, false, true,  452,  3, {20, 12, 6, 2}, 9, 6000, 100, 51200, 100, 51200, PSLR_JPEG_IMAGE_TONE_CROSS_PROCESSING, true,  11, ipslr_status_parse_ks1},
    { 0x13024, "K-S2",        false, true,  true,  false, false, true,  452,  3, {20, 12, 6, 2}, 9, 6000, 100, 51200, 100, 51200, PSLR_JPEG_IMAGE_TONE_CROSS_PROCESSING, true,  11, ipslr_status_parse_k3},
    { 0x13092, "K-1",         false, false, true,  true,  false, true,  456,  3, {36, 22, 12, 2}, 9, 8000, 100, 204800, 100, 204800, PSLR_JPEG_IMAGE_TONE_FLAT, true,  33, ipslr_status_parse_k1 },
    { 0x13240, "K-1 II",      false, false, true,  true,  false, true,  456,  3, {36, 22, 12, 2}, 9, 8000, 100, 819200, 100, 819200, PSLR_JPEG_IMAGE_TONE_FLAT, true,  33, ipslr_status_parse_k1 },
    { 0x13222, "K-70",        false, false, true,  true,  true,  true,  456,  3, {24, 14, 6, 2}, 9, 6000, 100, 102400, 100, 102400, PSLR_JPEG_IMAGE_TONE_AUTO, true,  11, ipslr_status_parse_k70},
    { 0x1322c, "KP",          false, false, true,  true,  false, true,  456,   3, {24, 14, 6, 2}, 9, 6000, 100, 819200, 100, 819200, PSLR_JPEG_IMAGE_TONE_AUTO, true,  27, ipslr_status_parse_k70},
    { 0x13010, "645Z",        false, false, true,  true,  false, false,  0,   3, {51, 32, 21, 3}, 9, 4000, 100, 204800, 100, 204800, PSLR_JPEG_IMAGE_TONE_CROSS_PROCESSING, true,  35, NULL},
    { 0x13254, "K-3III",      false, false, true,  true,  false, true,  452,  4, {24, 14, 6, 2}, 9, 8000, 100, 51200, 100, 51200, PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS, true,  27, ipslr_status_parse_k3}
*/

namespace Rtg.NINA.NinaPentaxKRDriver.NinaPentaxKRDriverDrivers {
    public class PentaxKRProfileJunk
    {
        public const int PERSONALITY_SHARPCAP = 0;
        public const int PERSONALITY_NINA = 1;
        public const short OUTPUTFORMAT_RAWBGR = 0;
        public const short OUTPUTFORMAT_BGR = 1;
        public const short OUTPUTFORMAT_RGGB = 2;

        private DeviceInfo m_info;

        public bool EnableLogging = false;
        public int DebugLevel = 0;
        public string DeviceId = "";
//        public int DeviceIndex = 0;
        //public short DefaultReadoutMode = PentaxKRProfile.OUTPUTFORMAT_RGGB;
        public bool UseLiveview = true;
        public int Personality = PERSONALITY_SHARPCAP;
        public bool BulbModeEnable = false;
        public bool KeepInterimFiles = false;
        public int SerialPort = 1;
        //public bool UseFile = false;

        public void assignCamera(int index)
        {
            m_info.ImageWidthPixels = PentaxCameraInfo.ElementAt(index).ImageWidthPixels;
            m_info.ImageHeightPixels = PentaxCameraInfo.ElementAt(index).ImageHeightPixels;
            m_info.LiveViewWidthPixels = PentaxCameraInfo.ElementAt(index).LiveViewWidthPixels;
            m_info.LiveViewHeightPixels = PentaxCameraInfo.ElementAt(index).LiveViewHeightPixels;
            m_info.PixelWidth = PentaxCameraInfo.ElementAt(index).PixelWidth;
            m_info.PixelHeight = PentaxCameraInfo.ElementAt(index).PixelHeight;
        }

        public void assignCamera(string name)
        {
            for (int i = 0; i < PentaxCameraInfo.Count; i++)
            {
                DriverCommon.LogCameraMessage(0,"assignCamera", PentaxCameraInfo.ElementAt(i).label+" "+name);
                if (PentaxCameraInfo.ElementAt(i).label == name)
                {
                    assignCamera(i);
                    return;
                }
            }

            assignCamera(0);
            return;
        }

        public struct DeviceInfo
        {
            public int Version;
            public int ImageWidthPixels;
            public int ImageHeightPixels;
            public int LiveViewHeightPixels;
            public int LiveViewWidthPixels;
            //            public int BayerXOffset;
            //            public int BayerYOffset;
            //            public int ExposureTimeMin;
            //            public int ExposureTimeMax;
            //            public int ExposureTimeStep;
            public double PixelWidth;
            public double PixelHeight;
            //            public int BitsPerPixel;

            public string Manufacturer;
            public string Model;
            public string SerialNumber;
            public string DeviceName;
            public string SensorName;
            public string DeviceVersion;
        }

        public struct CameraInfo
        {
            internal readonly string label;
            internal readonly int id;
            internal readonly int ImageWidthPixels;
            internal readonly int ImageHeightPixels;
            internal readonly int LiveViewWidthPixels;
            internal readonly int LiveViewHeightPixels;
            internal readonly double PixelWidth;
            internal readonly double PixelHeight;

            public CameraInfo(string label, int id, int ImageWidthPixels, int ImageHeightPixels, int LiveViewWidthPixels, int LiveViewHeightPixels, double PixelWidth, double PixelHeight)
            {
                this.label = label;
                this.id = id;
                this.ImageWidthPixels = ImageWidthPixels;
                this.ImageHeightPixels = ImageHeightPixels;
                this.LiveViewWidthPixels = LiveViewWidthPixels;
                this.LiveViewHeightPixels = LiveViewHeightPixels;
                this.PixelWidth = PixelWidth;
                this.PixelHeight = PixelHeight;
            }

            public string Label { get { return label; } }
            public int Id { get { return id; } }

        }

        // KP 6016x4000 14bit
        // K70 6000x4000 14bit
        // KF 6000x4000 14bit
        // K1ii 7360x4912 14bit
        // K1  7360x4912 14bit
        // K3iii 6192x4128 14bit 
        // 645Z 8256x6192 14bit

        static readonly IList<CameraInfo> PentaxCameraInfo = new ReadOnlyCollection<CameraInfo>
            (new[] {
                // TODO: fix preview size
             new CameraInfo ("PENTAX KP", 0, 6016, 4000, 720, 480, 3.88, 3.88),
             new CameraInfo ("PENTAX K-70", 1, 6000, 4000, 720, 480, 3.88, 3.88),
             new CameraInfo ("PENTAX KF", 2, 6000, 4000, 720, 480, 3.88, 3.88),
             new CameraInfo ("PENTAX K-1 Mark II", 3, 7360, 4912, 720, 480, 4.86, 4.86),
             new CameraInfo ("PENTAX K-1", 4, 7360, 4912, 720, 480, 4.86, 4.86),
             new CameraInfo ("PENTAX K-3 Mark III", 5, 6192, 4128, 1080, 720, 3.75, 3.75),
             new CameraInfo ("PENTAX 645Z", 6, 8256, 6192, 720, 480, 5.32, 5.32),
             new CameraInfo ("K-r", 7, 4288, 2848, 4288, 2848, 5.49, 5.49),
             new CameraInfo ("K-70", 1, 6000, 4000, 6000, 4000, 3.88, 3.88),
             new CameraInfo ("K-3", 1, 6016, 4000, 6016, 4000, 3.88, 3.88),
             new CameraInfo ("K-3II", 1, 6016, 4000, 6016, 4000, 3.88, 3.88),
             new CameraInfo ("K-5", 1, 4928, 3264, 4928, 3264, 4.77, 4.77),
             new CameraInfo ("K-5II", 1, 4928, 3264, 4928, 3264, 4.78, 4.78),
             new CameraInfo ("K-5IIs", 1, 4928, 3264, 4928, 3264, 4.78, 4.78),
             new CameraInfo ("K-50", 1, 4928, 3264, 4928, 3264, 4.78, 4.78),
             new CameraInfo ("K-30", 1, 4928, 3264, 4928, 3264, 4.78, 4.78),
             new CameraInfo ("K200D", 1, 3872, 2592, 3872, 2592, 6.01, 6.01)
            });

        public DeviceInfo Info
        {
            get
            {
                return m_info;
            }
        }

        public String SerialNumber
        {
            get
            {
                return m_info.SerialNumber.TrimStart(new char[] { '0' });
            }
        }

        public String DisplayName
        {
            get
            {
                return String.Format("{0} (s/n: {1})", "Pentax KR", SerialNumber);
            }
        }

        public String Model
        {
            get
            {
                return m_info.Model;
            }
        }




    }

    public class Capture
    {
        public String State;
    }

    public class CameraStatus
    {
        public CameraStatus()
        {

        }

        public uint BatteryLevel { get; set; }
        public Capture CurrentCapture { get; set; }
    }

    public class PKCamera
    {
        int m_id;
        string _modelStr;

        public Dictionary<string, string> ParseStatus(string status)
        {
            var result = new Dictionary<string, string>();

            using (StringReader sr = new StringReader(status))
            {
                string line;
                // Read the first line
//                Logger.Info("ParseStatus");
                line = sr.ReadLine();
//                Logger.Info(line);

                if (line != null)
                {
                    var parts = line.Split(':').Select(p => p.Trim()).ToList();
                    if (parts.Count == 3)
                    {
                        var elements = parts[2].Split(' ');
                        result.Add("pktriggercord-cli", elements[0]);
                        Logger.Debug("Added pktriggercord-cli : " + elements[0]);
                    }
                }

                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(':').Select(p => p.Trim()).ToList();
                    if (parts.Count == 2)
                    {
                        result.Add(parts[0], parts[1]);
                    }
                }
            }

            return result;
        }

        public string Model {
            get
            {
                if (string.IsNullOrEmpty(_modelStr))
                {
                    var result = ExecuteCommand("-s");
                    var parsedStatus = ParseStatus(result);
                    if (parsedStatus.ContainsKey("pktriggercord-cli"))
                    {
                        //_modelStr= "K-5II";
                        _modelStr = parsedStatus["pktriggercord-cli"];
                    }
                }
                
                // _modelStr= "K-5II";

                return _modelStr;
            }
            set
            { _modelStr = value; }
        }
        public string SerialNumber { get; set; }

        public int ISO { get; set; }

        IntPtr camHandle = IntPtr.Zero;
        PKTriggerCord.PslrStatus status;

        public uint Mode { 
            get {
                int result = PKTriggerCord.PKTriggerCordDLL.pslr_get_status(camHandle, ref status);
                return status.exposure_mode;
            }
        }

        public bool OldBulb
        {
            get
            {
                return PKTriggerCord.PKTriggerCordDLL.pslr_get_model_old_bulb_mode(camHandle);
            }
        }

        public bool BufMaskSingle()
        {
            return PKTriggerCord.PKTriggerCordDLL.pslr_get_model_bufmask_single(camHandle);
        }

        public bool StartLiveView()
        {
            //PKTriggerCord.PKTriggerCordDLL.pslr_set_jpeg_resolution(camHandle, 1);
            int quality = PKTriggerCord.PKTriggerCordDLL.pslr_get_model_max_jpeg_stars(camHandle);
            PKTriggerCord.PKTriggerCordDLL.pslr_set_jpeg_stars(camHandle, 1);
            //result = PKTriggerCord.PKTriggerCordDLL.pslr_get_status(camHandle, ref status);
            PKTriggerCord.PKTriggerCordDLL.pslr_set_drive_mode(camHandle, PKTriggerCord.PslrDriveMode.PSLR_DRIVE_MODE_CONTINUOUS_LO);
            PKTriggerCord.PKTriggerCordDLL.pslr_set_user_file_format(camHandle, UserFileFormat.USER_FILE_FORMAT_JPEG);
            lastShutterSpeed = 0.1;
            double F = 0.1 * 1000;
            PKTriggerCord.PslrRational shutter_speed;
            shutter_speed.denom = 1000;
            shutter_speed.nom = (int)F;
            PKTriggerCord.PKTriggerCordDLL.pslr_set_shutter(camHandle, shutter_speed);
            //PKTriggerCord.PKTriggerCordDLL.pslr_continuous(camHandle, true);
            //if (Model == "K200D")
            //    PKTriggerCord.PKTriggerCordDLL.pslr_shutter(camHandle);

            return false;
        }

        public bool StopLiveView()
        {
            bool ret;

            PKTriggerCord.PKTriggerCordDLL.pslr_continuous(camHandle, false);

            while (true)
            {
                PKTriggerCordDLL.pslr_get_status(camHandle, ref status);
                int buffer_index;
                int bracket_download = status.bufmask;
                if (bracket_download == 0)
                    break;
                buffer_index = getFirstSetBit(status.bufmask) - 1;
                PKTriggerCordDLL.pslr_delete_buffer(camHandle, buffer_index);
            }

            PKTriggerCord.PKTriggerCordDLL.pslr_set_user_file_format(camHandle, UserFileFormat.USER_FILE_FORMAT_DNG);
            PKTriggerCord.PKTriggerCordDLL.pslr_set_drive_mode(camHandle, PKTriggerCord.PslrDriveMode.PSLR_DRIVE_MODE_SINGLE);

            return false;
        }

        public bool Connect()
        {
            camHandle = PKTriggerCord.PKTriggerCordDLL.pslr_init(null, null);
            if (camHandle != IntPtr.Zero)
            {
                int result = PKTriggerCord.PKTriggerCordDLL.pslr_connect(camHandle);
                if (result == 0)
                {
                    Console.WriteLine("Connected to camera successfully!");

                    // Get camera status
                    status = new PKTriggerCord.PslrStatus();
                    result = PKTriggerCord.PKTriggerCordDLL.pslr_get_status(camHandle, ref status);
                    if (result == 0)
                    {
                        Console.WriteLine("Current ISO: " + status.current_iso);
                        Console.WriteLine("Current shutter speed: " + status.current_shutter_speed.nom + "/" + status.current_shutter_speed.denom);
                    }

                    lastISO = 0;
                    lastShutterSpeed = 0.0;

                    PKTriggerCord.PKTriggerCordDLL.pslr_set_user_file_format(camHandle, UserFileFormat.USER_FILE_FORMAT_DNG);

                    return true;
                }
                return false;
            }
            return false;
        }
        public void Disconnect() {
            // Disconnect
            PKTriggerCord.PKTriggerCordDLL.pslr_disconnect(camHandle);
            PKTriggerCord.PKTriggerCordDLL.pslr_shutdown(camHandle);
        }

        public bool IsConnected() { 
            if(camHandle!= IntPtr.Zero)
                return true;
            return false;
        }

        static int count = 0;
        int lastISO;
        double lastShutterSpeed;

        private bool IsFileClosed(string filePath)
        {
            try
            {
                using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        int getFirstSetBit(int n)
        {

            // If no set bit 
            if (n == 0) return 0;

            // Get the rightmost set bit
            int res = n & (~n + 1);

            // Find position using log2
            return (int)(Math.Log(res)/Math.Log(2)) + 1;
        }

        public int StartLiveViewCapture(double Duration)
        {
            string StorePath = GetStoragePath();
            string Light = "hi";
            count++;

            DriverCommon.LogCameraMessage(0, "StartCapture", "PentaxCamera.StartExposure(Duration, Light), duration ='" + Duration.ToString() + "', Light = '" + Light.ToString() + "'");

            string fileName = StorePath + "\\" + "test" + count.ToString(); // GetFileName(Duration, DateTime.Now);

            if (camHandle != IntPtr.Zero)
            {
                CameraDriver.m_captureState = CameraStates.Exposing;

                while (true)
                {
                    PKTriggerCordDLL.pslr_get_status(camHandle, ref status);
                    int buffer_index;
                    int bracket_download = status.bufmask;

                    if (bracket_download == 0)
                    {
                        PKTriggerCordDLL.pslr_continuous(camHandle, true);
                        if (Model == "K200D")
                            PKTriggerCord.PKTriggerCordDLL.pslr_shutter(camHandle);
                    }

                    buffer_index = getFirstSetBit(status.bufmask) - 1;
                    if (buffer_index >= 0)
                    {
                        bool ret;

                        bracket_download -= 1 << buffer_index;
                        if (bracket_download != 0)
                        {
                            PKTriggerCordDLL.pslr_continuous(camHandle, false);
                        }
                        else
                        {
                            PKTriggerCordDLL.pslr_continuous(camHandle, true);
                            if (Model == "K200D")
                                PKTriggerCord.PKTriggerCordDLL.pslr_shutter(camHandle);
                        }

                        //string fileName = output_file + (counter + frameNo - bracket_download + buffer_index + 1).ToString();
                        using (FileStream fs = new FileStream(fileName + ".JPG", FileMode.Create, FileAccess.Write))
                        {
                            ret = SaveBuffer(camHandle, buffer_index, fs, ref status, UserFileFormat.USER_FILE_FORMAT_JPEG);
                        }

                        if (ret)
                        {
                            PKTriggerCordDLL.pslr_delete_buffer(camHandle, buffer_index);
                            while (!IsFileClosed(fileName + ".JPG")) { Thread.Sleep(100); }
                            CameraDriver.imagesToProcess.Enqueue(fileName + ".JPG");
                            break;
                        }
                    }

                }

                PKTriggerCordDLL.pslr_continuous(camHandle, false);
                CameraDriver.m_captureState = CameraStates.Idle;
            }
            else
            {
                Console.WriteLine("Failed to connect to camera.");
            }

            return 1;
        }
        public int StartCapture(double Duration) {
            string StorePath = GetStoragePath();
            string Light="hi";
            count++;

            DriverCommon.LogCameraMessage(0,"StartCapture","PentaxCamera.StartExposure(Duration, Light), duration ='" + Duration.ToString() + "', Light = '" + Light.ToString() + "'");

            string fileName = StorePath + "\\" + "test" + count.ToString(); // GetFileName(Duration, DateTime.Now);
            //pktriggercord-cli --file_format dng -o c:\temp\test.dng -i 400 -t 1

            if (camHandle != IntPtr.Zero)
            {
                if (lastISO != ISO)
                {
                    PKTriggerCord.PKTriggerCordDLL.pslr_set_iso(camHandle, (uint)ISO, 0, 0);
                    lastISO = ISO;
                }

                if (lastShutterSpeed != Duration)
                {
                    lastShutterSpeed = Duration;
                    double F = Duration * 1000;
                    PKTriggerCord.PslrRational shutter_speed;
                    shutter_speed.denom = 1000;
                    shutter_speed.nom = (int)F;
                    PKTriggerCord.PKTriggerCordDLL.pslr_set_shutter(camHandle, shutter_speed);
                }

                PKTriggerCord.PKTriggerCordDLL.pslr_shutter(camHandle);
                CameraDriver.m_captureState = CameraStates.Exposing;

                bool ret;

                //string fileName = output_file + (counter + frameNo - bracket_download + buffer_index + 1).ToString();
                using (FileStream fs = new FileStream(fileName+".DNG", FileMode.Create, FileAccess.Write))
                {
                    ret=SaveBuffer(camHandle, 0, fs, ref status, UserFileFormat.USER_FILE_FORMAT_DNG);
                }
                PKTriggerCordDLL.pslr_delete_buffer(camHandle, 0);
                while (!IsFileClosed(fileName + ".DNG")) { Thread.Sleep(100); }

                if(ret)
                    CameraDriver.imagesToProcess.Enqueue(fileName + ".DNG");
                else
                    throw new ASCOM.DriverException("Read Error");

                CameraDriver.m_captureState = CameraStates.Idle;
            }
            else
            {
                Console.WriteLine("Failed to connect to camera.");
            }

            return 1;
        }
        public int StartBulbCapture()
        {
            string StorePath = GetStoragePath();
            string Light = "hi";

            DriverCommon.LogCameraMessage(0, "StartCapture", "StartBulbCapture");

            //MarkWaitingForExposure(Duration, fileName);
            //watch();

            // Example usage of PKTriggerCordDLL
            if (camHandle != IntPtr.Zero)
            {
                if (lastISO != ISO)
                {
                    PKTriggerCord.PKTriggerCordDLL.pslr_set_iso(camHandle, (uint)ISO, 0, 0);
                    lastISO = ISO;
                }

                PKTriggerCord.PKTriggerCordDLL.pslr_bulb(camHandle, true);

                PKTriggerCord.PKTriggerCordDLL.pslr_shutter(camHandle);
                CameraDriver.m_captureState = CameraStates.Exposing;
            }
            else
            {
                Console.WriteLine("Failed to connect to camera.");
            }

            return 1;
        }
        public void StopBulbCapture() {
            PKTriggerCord.PKTriggerCordDLL.pslr_bulb(camHandle, false);
            count++;

            //string fileName = output_file + (counter + frameNo - bracket_download + buffer_index + 1).ToString();
            string StorePath = GetStoragePath();
            string fileName = StorePath + "\\" + "test" + count.ToString(); // GetFileName(Duration, DateTime.Now);

            bool ret;

            using (FileStream fs = new FileStream(fileName + ".DNG", FileMode.Create, FileAccess.Write))
            {
                ret = SaveBuffer(camHandle, 0, fs, ref status, UserFileFormat.USER_FILE_FORMAT_DNG);
            }
            PKTriggerCordDLL.pslr_delete_buffer(camHandle, 0);
            while (!IsFileClosed(fileName + ".DNG")) { Thread.Sleep(100); }

            if (ret)
                CameraDriver.imagesToProcess.Enqueue(fileName + ".DNG");
            else
                throw new ASCOM.DriverException("Read Error");

            CameraDriver.m_captureState = CameraStates.Idle;
        }

        private static bool SaveBuffer(IntPtr camhandle, int buffer_index, FileStream fs, ref PslrStatus status, UserFileFormat uff)
        {
            PslrBufferType type = (uff == UserFileFormat.USER_FILE_FORMAT_JPEG) ? PslrBufferType.PSLR_BUF_JPEG_MAX : PslrBufferType.PSLR_BUF_DNG;
            int ret = 1;
                
            while(ret!=0)
                ret=PKTriggerCordDLL.pslr_buffer_open(camhandle, buffer_index, type, (int)status.jpeg_resolution);

            CameraDriver.m_captureState = CameraStates.Reading;

            uint size = PKTriggerCordDLL.pslr_buffer_get_size(camhandle);
            uint remainder = size;
            IntPtr buf = Marshal.AllocHGlobal((int)size);
            uint read = PKTriggerCordDLL.pslr_buffer_read(camhandle, buf, size);
            while(read > 0) {
                //if (read > 0)
                {
                    byte[] data = new byte[read];
                    Marshal.Copy(buf, data, 0, (int)read);
                    fs.Write(data, 0, (int)read);
                    remainder -= read;
                }
                read = PKTriggerCordDLL.pslr_buffer_read(camhandle, buf, size);
            }
            Marshal.FreeHGlobal(buf);
            PKTriggerCordDLL.pslr_buffer_close(camhandle);
            if(remainder>0)
                return false;
            return true; // Synchronous, so exit loop
        }

        //pktriggercord-cli.exe --frames=1 --shutter_speed=0.1 --file_format=DNG --iso=400 --aperture=2.8 -o test1.dng --green
        public void StopCapture() { }

        private string _fileNameWaiting;

        private bool _waitingForImage = false;
        private DateTime _exposureStartTime;
        private const int timeout = 60;
        private double _lastDuration;
        private string _lastFileName;

        private void MarkWaitingForExposure(double Duration, string fileName)
        {
            _exposureStartTime = DateTime.Now;
            _lastDuration = Duration;
            _waitingForImage = true;
            _fileNameWaiting = fileName;
        }

        FileSystemWatcher watcher;

        private void watch()
        {
            string StorePath = GetStoragePath();

            watcher = new FileSystemWatcher();
            watcher.Path = StorePath;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.DNG";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;

            //Logger.WriteTraceMessage("watch " + StorePath);
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var fileName = e.FullPath;
            //            string StorePath = GetStoragePath();

            DriverCommon.LogCameraMessage(0, "OnChanged", "onchanged " + fileName);

//            var destinationFilePath = Path.ChangeExtension(Path.Combine(StorePath, Path.Combine(StorePath, _fileNameWaiting)), ".dng");

            //Logger.WriteTraceMessage("onchanged dest " + destinationFilePath);

//            File.Copy(fileName, destinationFilePath);
//            File.Delete(fileName);

            CameraDriver.imagesToProcess.Enqueue(e.FullPath);
            CameraDriver.m_captureState = CameraStates.Idle;

            watcher.Changed -= OnChanged;
            watcher.EnableRaisingEvents = false;
            watcher = null;
        }

        private string GetAppPath()
        {
            string AppPath;
            AppPath = Assembly.GetExecutingAssembly().Location;
            AppPath = Path.GetDirectoryName(AppPath);

            return AppPath;
        }
        private string GetStoragePath()
        {
            string StoragePath = System.IO.Path.GetTempPath();

            return StoragePath;
        }

        public string ExecuteCommand(string args)
        {
            DriverCommon.LogCameraMessage(1, "ExecuteCommand", "ExecuteCommand(), args = '" + args + "'");

            string exeDir = Path.Combine(GetAppPath(), "pktriggercord", "pktriggercord-cli.exe");
            ProcessStartInfo procStartInfo = new ProcessStartInfo();

            procStartInfo.FileName = exeDir;
            procStartInfo.Arguments = args;// + " --timeout 10";
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            DriverCommon.LogCameraMessage(1, "ExecuteCommand","about to start process with command = '" + procStartInfo.FileName + " " + procStartInfo.Arguments + "'");

            string result = string.Empty;
            using (Process process = new Process())
            {
                process.StartInfo = procStartInfo;
                process.Start();
                process.WaitForExit(60000);

                result = process.StandardOutput.ReadToEnd();
                DriverCommon.LogCameraMessage(1, "ExecuteCommand", "result of command = '" + result + "'");
            }
            //result = "pktriggercord-cli: K-5IIs Connected...";
            return result;
        }



    }

    class DriverCommon
    {
        private static Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public static string DriverVersion = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

        // IMPORTANT
        // CameraDriverName **cannot** change, the APT software recognizes this name specifically and enables fast-readout
        // for preview mode.
        // "Pentax KR Camera"
        public static string CameraDriverName = "Pentax K200D/KR/K5II Camera";
        public static string CameraDriverId = "ASCOM.PentaxKR.Camera";
        public static string CameraDriverDescription = "Pentax K200D/KR/K5II Camera";
        public static string CameraDriverInfo = $"Camera control for Pentax K200D/KR/K5II cameras. Version: {DriverVersion}";

        // This can't be static???????
        public static CameraProvider.PentaxKRProfile Settings = new CameraProvider.PentaxKRProfile();
        //private static TraceLogger Logger = new TraceLogger("", "PentaxKR");

        //internal static PKCamera m_camera = null;

        // Common to both
        internal static string debugLevelProfileName = "Debug Level";
        internal static string debugLevelDefault = "5";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";
        internal static string cameraProfileName = "Camera ID";
        internal static string cameraDefault = "";

        // Specific to Camera
        internal static string readoutModeDefaultProfileName = "Readout Mode";
        internal static string readoutModeDefault = "2";
        internal static string useLiveviewProfileName = "Use Camera Liveview";
        internal static string useLiveviewDefault = "true";
        internal static string personalityProfileName = "Personality";
        internal static string personalityDefault = "0";
        internal static string serialPortProfileName = "SerialPort";
        internal static string serialPortDefault = "1";
        internal static string bulbModeEnableProfileName = "Bulb Mode Enable";
        internal static string bulbModeEnableDefault = "false";
        internal static string keepInterimFilesProfileName = "Keep Interim Files";
        internal static string keepInterimFilesDefault = "false";

        // Specific to Focuser

/*        static public bool CameraConnected
        {
            get
            {
                // TODO:  this is not Mutex safe
                //using (new DriverCommon.SerializedAccess("get_Connected"))
                {
                    DriverCommon.LogCameraMessage(0,"Connected", "get");
                    if (_camera == null)
                        return false;

                    return _camera.IsConnected();
                }
            }

            set
            {
                bool oldValue = cameraConnected;

                cameraConnected = value;

                try
                {
                    EnsureCameraConnection();
                }
                catch
                {
                    cameraConnected = oldValue;
                }
            }
        }*/

        public static void LogCameraMessage(int level, string identifier, string message, params object[] args) {
            if (level == 0) {
                var msg = string.Format(message, args);
                Logger.Info($"[camera] {identifier}", msg);
            } else if (level == 1) {
                var msg = string.Format(message, args);
                Logger.Debug($"[camera] {identifier}", msg);
            } else {
                var msg = string.Format(message, args);
                Logger.Trace($"[camera] {identifier}", msg);
            }
        }

        private static void Log(String message, String source = "DriverCommon") {
            Logger.Info(source, message);
        }

/*        public class SerializedAccess : IDisposable
        {
            internal static Mutex m_serialAccess = new Mutex();

            internal String m_method;
            internal bool m_mustReleaseMutex;


            public SerializedAccess(String method, bool shortWait = true)
            {
                // Dont need camera
                // Need to know what kind of message
                m_method = method;
                m_mustReleaseMutex = true;
//                DriverCommon.LogCameraMessage(0,m_method, "[enter] " + m_serialAccess.ToString() + " " + shortWait.ToString());

                if (!m_serialAccess.WaitOne(10))
                {
//                    DriverCommon.LogCameraMessage(0,m_method, "Waiting to enter " + m_serialAccess.ToString());

                    if (shortWait)
                    {
                        m_mustReleaseMutex = false;
//                        DriverCommon.LogCameraMessage(0,m_method, "[in] short " + m_serialAccess.ToString());
                        return;
                    }
                    m_serialAccess.WaitOne(20000);
                }

//                DriverCommon.LogCameraMessage(0,m_method, "[in] " + m_serialAccess.ToString());
            }

            public void Dispose()
            {
                //DriverCommon.LogCameraMessage(0,m_method, "[out] " + m_serialAccess.ToString());
                if (m_mustReleaseMutex)
                    m_serialAccess.ReleaseMutex();
            }
        }*/


    }
}
