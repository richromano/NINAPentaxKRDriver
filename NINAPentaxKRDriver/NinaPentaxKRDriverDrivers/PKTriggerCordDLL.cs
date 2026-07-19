using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PKTriggerCord
{
    [Flags]
    public enum PslrBufferType
    {
        PSLR_BUF_PEF,
        PSLR_BUF_DNG,
        PSLR_BUF_JPEG_MAX,
        PSLR_BUF_JPEG_MAX_M1,
        PSLR_BUF_JPEG_MAX_M2,
        PSLR_BUF_JPEG_MAX_M3,
        PSLR_BUF_PREVIEW = 8,
        PSLR_BUF_THUMBNAIL = 9
    }

    public enum UserFileFormat
    {
        USER_FILE_FORMAT_PEF,
        USER_FILE_FORMAT_DNG,
        USER_FILE_FORMAT_JPEG,
        USER_FILE_FORMAT_MAX
    }

    public enum PslrExposureMode
    {
        PSLR_EXPOSURE_MODE_P = 0,
        PSLR_EXPOSURE_MODE_GREEN = 1,
        PSLR_EXPOSURE_MODE_TV = 4,
        PSLR_EXPOSURE_MODE_AV = 5,
        PSLR_EXPOSURE_MODE_M = 8,
        PSLR_EXPOSURE_MODE_B = 9,
        PSLR_EXPOSURE_MODE_AV_OFFAUTO = 10,
        PSLR_EXPOSURE_MODE_M_OFFAUTO = 11,
        PSLR_EXPOSURE_MODE_B_OFFAUTO = 12,
        PSLR_EXPOSURE_MODE_TAV = 13,
        PSLR_EXPOSURE_MODE_SV = 15,
        PSLR_EXPOSURE_MODE_X = 16,
        PSLR_EXPOSURE_MODE_MAX = 17
    }

    public enum PslrGuiExposureMode
    {
        PSLR_GUI_EXPOSURE_MODE_GREEN,
        PSLR_GUI_EXPOSURE_MODE_P,
        PSLR_GUI_EXPOSURE_MODE_SV,
        PSLR_GUI_EXPOSURE_MODE_TV,
        PSLR_GUI_EXPOSURE_MODE_AV,
        PSLR_GUI_EXPOSURE_MODE_TAV,
        PSLR_GUI_EXPOSURE_MODE_M,
        PSLR_GUI_EXPOSURE_MODE_B,
        PSLR_GUI_EXPOSURE_MODE_X,
        PSLR_GUI_EXPOSURE_MODE_MAX
    }

    public enum PslrColorSpace
    {
        PSLR_COLOR_SPACE_SRGB,
        PSLR_COLOR_SPACE_ADOBERGB,
        PSLR_COLOR_SPACE_MAX
    }

    public enum PslrAfMode
    {
        PSLR_AF_MODE_MF,
        PSLR_AF_MODE_AF_S,
        PSLR_AF_MODE_AF_C,
        PSLR_AF_MODE_AF_A,
        PSLR_AF_MODE_MAX
    }

    public enum PslrAeMetering
    {
        PSLR_AE_METERING_MULTI,
        PSLR_AE_METERING_CENTER,
        PSLR_AE_METERING_SPOT,
        PSLR_AE_METERING_MAX
    }

    public enum PslrFlashMode
    {
        PSLR_FLASH_MODE_MANUAL = 0,
        PSLR_FLASH_MODE_MANUAL_REDEYE = 1,
        PSLR_FLASH_MODE_SLOW = 2,
        PSLR_FLASH_MODE_SLOW_REDEYE = 3,
        PSLR_FLASH_MODE_TRAILING_CURTAIN = 4,
        PSLR_FLASH_MODE_AUTO = 5,
        PSLR_FLASH_MODE_AUTO_REDEYE = 6,
        PSLR_FLASH_MODE_WIRELESS = 8,
        PSLR_FLASH_MODE_MAX = 9
    }

    public enum PslrDriveMode
    {
        PSLR_DRIVE_MODE_SINGLE,
        PSLR_DRIVE_MODE_CONTINUOUS_HI,
        PSLR_DRIVE_MODE_SELF_TIMER_12,
        PSLR_DRIVE_MODE_SELF_TIMER_2,
        PSLR_DRIVE_MODE_REMOTE,
        PSLR_DRIVE_MODE_REMOTE_3,
        PSLR_DRIVE_MODE_CONTINUOUS_LO,
        PSLR_DRIVE_MODE_MAX
    }

    public enum PslrAfPointSel
    {
        PSLR_AF_POINT_SEL_AUTO_5,
        PSLR_AF_POINT_SEL_SELECT,
        PSLR_AF_POINT_SEL_SPOT,
        PSLR_AF_POINT_SEL_AUTO_11,
        PSLR_AF_POINT_SEL_EXPANDED,
        PSLR_AF_POINT_SEL_MAX
    }

    public enum PslrJpegImageTone
    {
        PSLR_JPEG_IMAGE_TONE_NONE = -1,
        PSLR_JPEG_IMAGE_TONE_NATURAL,
        PSLR_JPEG_IMAGE_TONE_BRIGHT,
        PSLR_JPEG_IMAGE_TONE_PORTRAIT,
        PSLR_JPEG_IMAGE_TONE_LANDSCAPE,
        PSLR_JPEG_IMAGE_TONE_VIBRANT,
        PSLR_JPEG_IMAGE_TONE_MONOCHROME,
        PSLR_JPEG_IMAGE_TONE_MUTED,
        PSLR_JPEG_IMAGE_TONE_REVERSAL_FILM,
        PSLR_JPEG_IMAGE_TONE_BLEACH_BYPASS,
        PSLR_JPEG_IMAGE_TONE_RADIANT,
        PSLR_JPEG_IMAGE_TONE_CROSS_PROCESSING,
        PSLR_JPEG_IMAGE_TONE_FLAT,
        PSLR_JPEG_IMAGE_TONE_AUTO,
        PSLR_JPEG_IMAGE_TONE_MAX
    }

    public enum PslrWhiteBalanceMode
    {
        PSLR_WHITE_BALANCE_MODE_AUTO,
        PSLR_WHITE_BALANCE_MODE_DAYLIGHT,
        PSLR_WHITE_BALANCE_MODE_SHADE,
        PSLR_WHITE_BALANCE_MODE_CLOUDY,
        PSLR_WHITE_BALANCE_MODE_FLUORESCENT_DAYLIGHT_COLOR,
        PSLR_WHITE_BALANCE_MODE_FLUORESCENT_DAYLIGHT_WHITE,
        PSLR_WHITE_BALANCE_MODE_FLUORESCENT_COOL_WHITE,
        PSLR_WHITE_BALANCE_MODE_TUNGSTEN,
        PSLR_WHITE_BALANCE_MODE_FLASH,
        PSLR_WHITE_BALANCE_MODE_MANUAL,
        PSLR_WHITE_BALANCE_MODE_MANUAL_2,
        PSLR_WHITE_BALANCE_MODE_MANUAL_3,
        PSLR_WHITE_BALANCE_MODE_KELVIN_1,
        PSLR_WHITE_BALANCE_MODE_KELVIN_2,
        PSLR_WHITE_BALANCE_MODE_KELVIN_3,
        PSLR_WHITE_BALANCE_MODE_FLUORESCENT_WARM_WHITE,
        PSLR_WHITE_BALANCE_MODE_CTE,
        PSLR_WHITE_BALANCE_MODE_MULTI_AUTO,
        PSLR_WHITE_BALANCE_MODE_MAX
    }

    public enum PslrImageFormat
    {
        PSLR_IMAGE_FORMAT_JPEG,
        PSLR_IMAGE_FORMAT_RAW,
        PSLR_IMAGE_FORMAT_RAW_PLUS,
        PSLR_IMAGE_FORMAT_MAX
    }

    public enum PslrRawFormat
    {
        PSLR_RAW_FORMAT_PEF,
        PSLR_RAW_FORMAT_DNG,
        PSLR_RAW_FORMAT_MAX
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PslrRational
    {
        public int nom;
        public int denom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PslrStatus
    {
        public ushort bufmask;
        public uint current_iso;
        public PslrRational current_shutter_speed;
        public PslrRational current_aperture;
        public PslrRational lens_max_aperture;
        public PslrRational lens_min_aperture;
        public PslrRational set_shutter_speed;
        public PslrRational set_aperture;
        public PslrRational max_shutter_speed;
        public uint auto_bracket_mode;
        public PslrRational auto_bracket_ev;
        public uint auto_bracket_picture_count;
        public uint auto_bracket_picture_counter;
        public uint fixed_iso;
        public uint jpeg_resolution;
        public uint jpeg_saturation;
        public uint jpeg_quality;
        public uint jpeg_contrast;
        public uint jpeg_sharpness;
        public uint jpeg_image_tone;
        public uint jpeg_hue;
        public PslrRational zoom;
        public int focus;
        public uint image_format;
        public uint raw_format;
        public uint light_meter_flags;
        public PslrRational ec;
        public uint custom_ev_steps;
        public uint custom_sensitivity_steps;
        public uint exposure_mode;
        public uint scene_mode;
        public uint user_mode_flag;
        public uint ae_metering_mode;
        public uint af_mode;
        public uint af_point_select;
        public uint selected_af_point;
        public uint focused_af_point;
        public uint auto_iso_min;
        public uint auto_iso_max;
        public uint drive_mode;
        public uint shake_reduction;
        public PslrWhiteBalanceMode white_balance_mode;
        public uint white_balance_adjust_mg;
        public uint white_balance_adjust_ba;
        public uint flash_mode;
        public int flash_exposure_compensation;
        public int manual_mode_ev;
        public uint color_space;
        public uint lens_id1;
        public uint lens_id2;
        public uint battery_1;
        public uint battery_2;
        public uint battery_3;
        public uint battery_4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PslrBoolSetting
    {
        public PslrSettingStatus status;
        [MarshalAs(UnmanagedType.I1)]
        public bool value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PslrUint16Setting
    {
        public PslrSettingStatus status;
        public ushort value;
    }

    public enum PslrSettingStatus
    {
        PSLR_SETTING_STATUS_UNKNOWN,
        PSLR_SETTING_STATUS_READ,
        PSLR_SETTING_STATUS_HARDWIRED,
        PSLR_SETTING_STATUS_NA
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PslrSettings
    {
        public PslrBoolSetting one_push_bracketing;
        public PslrBoolSetting bulb_mode_press_press;
        public PslrBoolSetting bulb_timer;
        public PslrUint16Setting bulb_timer_sec;
        public PslrBoolSetting using_aperture_ring;
        public PslrBoolSetting shake_reduction;
        public PslrBoolSetting astrotracer;
        public PslrUint16Setting astrotracer_timer_sec;
        public PslrBoolSetting horizon_correction;
        public PslrBoolSetting remote_bulb_mode_press_press;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class PKTriggerCordDLL
    {
        private const string DllName = "PKTriggerCordDLL";

        [DllImport(DllName, EntryPoint = "pslr_init", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr pslr_init(string model, string device);

        [DllImport(DllName, EntryPoint = "pslr_set_verbosity", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr pslr_set_verbosity(int verbosity);

        [DllImport(DllName, EntryPoint = "pslr_button_test", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_button_test(IntPtr h, int a, int b);

        [DllImport(DllName, EntryPoint = "pslr_connect", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_connect(IntPtr h);

        [DllImport(DllName, EntryPoint = "pslr_disconnect", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_disconnect(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_shutdown(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_shutter(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_focus(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_get_status(IntPtr h, ref PslrStatus sbuf);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_get_settings_json(IntPtr h, ref PslrSettings ps);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr pslr_get_status_info(IntPtr h, PslrStatus status);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr pslr_get_settings_info(IntPtr h, PslrSettings settings);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_shutter(IntPtr h, PslrRational value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_aperture(IntPtr h, PslrRational value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_iso(IntPtr h, uint value, uint auto_min_value, uint auto_max_value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_expose_compensation(IntPtr h, PslrRational value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_white_balance(IntPtr h, PslrWhiteBalanceMode wb_mode);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_white_balance_adjustment(IntPtr h, PslrWhiteBalanceMode wb_mode, uint wbadj_mg, uint wbadj_ba);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_flash_mode(IntPtr h, PslrFlashMode value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_flash_exposure_compensation(IntPtr h, PslrRational value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_drive_mode(IntPtr h, PslrDriveMode drive_mode);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_af_mode(IntPtr h, PslrAfMode af_mode);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_af_point_sel(IntPtr h, PslrAfPointSel af_point_sel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_ae_metering_mode(IntPtr h, PslrAeMetering ae_metering_mode);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_color_space(IntPtr h, PslrColorSpace color_space);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_jpeg_stars(IntPtr h, int jpeg_stars);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_jpeg_resolution(IntPtr h, int megapixel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_jpeg_image_tone(IntPtr h, PslrJpegImageTone image_mode);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_user_file_format(IntPtr h, UserFileFormat uff);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern UserFileFormat pslr_get_user_file_format(ref PslrStatus st);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_delete_buffer(IntPtr h, int bufno);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_green_button(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_dust_removal(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_bulb(IntPtr h, [MarshalAs(UnmanagedType.I1)] bool on);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool pslr_continuous(IntPtr h, [MarshalAs(UnmanagedType.I1)] bool on);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_buffer_open(IntPtr h, int bufno, PslrBufferType type, int resolution);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool pslr_get_model_old_bulb_mode(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool pslr_get_model_bufmask_single(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern uint pslr_buffer_read(IntPtr h, IntPtr buf, uint size);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern uint pslr_fullmemory_read(IntPtr h, IntPtr buf, uint offset, uint size);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void pslr_buffer_close(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern uint pslr_buffer_get_size(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_exposure_mode(IntPtr h, PslrExposureMode mode);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_set_selected_af_point(IntPtr h, uint point);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string pslr_get_camera_name(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_get_model_max_jpeg_stars(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_get_model_status_buffer_size(IntPtr h);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int pslr_get_model_fastest_shutter_speed(IntPtr h);
    }
}