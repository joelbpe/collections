using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

public class Helper {

    public static byte[] ConvertToBytes(string sourcePath) {
        byte[] bytes = null;

        using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
            using (System.IO.FileStream f = new System.IO.FileStream(sourcePath, System.IO.FileMode.Open, System.IO.FileAccess.Read)) {
                f.CopyTo(ms);
                bytes = ms.ToArray();
            }
        }

        return bytes;
    }

    public static string ProcessVideo(string options) {
        var output = string.Empty;
        var ffmpegPath = ConfigurationManager.AppSettings["Component.Media:ffmpeg"];

        try {
            ProcessStartInfo info = new ProcessStartInfo(Path.Combine(ffmpegPath, "ffmpeg.exe"), options);
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;

            Process proc = System.Diagnostics.Process.Start(info);
            output = proc.StandardError.ReadToEnd();

            proc.WaitForExit();
            proc.Close();
        } catch (Exception ex) { }

        return output;
    }

    public static VideoConversionSettings GetVideoInfo(string sourcePath) {
        if (string.IsNullOrEmpty(sourcePath)) {
            return null;
        }

        var video = new VideoConversionSettings();

        //set up the parameters for video info
        string output = ProcessVideo(string.Format("-i {0}", sourcePath));

        //get duration
        Regex re = new Regex("[D|d]uration:.((\\d|:|\\.)*)");
        Match m = re.Match(output);

        if (m.Success) {
            string duration = m.Groups[1].Value;
            string[] timepieces = duration.Split(new char[] { ':', '.' });
            if (timepieces.Length == 4) {
                video.Duration = new TimeSpan(0, System.Convert.ToInt16(timepieces[0]), System.Convert.ToInt16(timepieces[1]), System.Convert.ToInt16(timepieces[2]), System.Convert.ToInt16(timepieces[3]));
            }
        }

        //get audio bit rate
        re = new Regex("[B|b]itrate:.((\\d|:)*)");
        m = re.Match(output);
        decimal kb = 0;
        if (m.Success) {
            decimal.TryParse(m.Groups[1].Value, out kb);
        }

        video.AudioBitrate = kb;

        //get the audio format
        re = new Regex("[A|a]udio:.*");
        m = re.Match(output);
        if (m.Success) {
            video.AudioFormat = m.Value;
        }

        //get the video format
        re = new Regex("[V|v]ideo:.*");
        m = re.Match(output);
        if (m.Success) {
            video.VideoFormat = m.Value;
        }

        //get the video format
        re = new Regex("(\\d{2,3})x(\\d{2,3})");
        m = re.Match(output);
        if (m.Success) {
            int width = 0; int height = 0;
            int.TryParse(m.Groups[1].Value, out width);
            int.TryParse(m.Groups[2].Value, out height);

            video.Height = height;
            video.Width = width;
        }

        return video;
    }

}
