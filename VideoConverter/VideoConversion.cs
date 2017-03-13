using System;
using System.IO;

public class VideoConversion {

    public VideoConversion(VideoConversionSettings settings) {
        this.Settings = settings;
        this.Settings.ID = Guid.NewGuid();

        //check if the resolution is provided otherwise we will use the original WxH
        if (settings.Width == 0 && settings.Height == 0) {
            if (File.Exists(settings.SourcePath)) {
                var output = Helper.GetVideoInfo(settings.SourcePath);

                settings.Height = output.Height;
                settings.Width = output.Width;
            } else {
                //The file do not exists
                //To do: log this error in to the database.                    
            }
        }
    }

    private VideoConversionSettings Settings { get; set; }

    public void ToJpeg() {
        var targetPath = System.IO.Path.Combine(Settings.TargetPath, string.Format("{0}.jpg", Settings.ID));

        if (!System.IO.File.Exists(targetPath)) {
            int secs = (int)Math.Round(TimeSpan.FromTicks(Settings.Duration.Ticks / 3).TotalSeconds, 0);

            Helper.ProcessVideo(string.Format("-i {0} {1} -ss {2} -qscale:v 2 -vframes 1", Settings.SourcePath, targetPath, secs));
        }
    }

    public void ToNewVideoFormat() {
        var targetPath = System.IO.Path.Combine(Settings.TargetPath, string.Format("{0}.{1}", Settings.ID, Settings.VideoFormat));

        if (!System.IO.File.Exists(targetPath)) {
            var options = string.Format(@"-i {0} -vcodec libx264 -acodec aac -strict -2 -b:v {1}k -ab {2}k -s {3} -framerate {4} -y {5}",
                                       Settings.SourcePath,
                                       Settings.VideoBitrate,
                                       Settings.AudioBitrate,
                                       Settings.Resolution,
                                       Settings.FrameRate,
                                       targetPath);

            Helper.ProcessVideo(options);
        }
    }

    public VideoConversionSettings GetVideoInfo(string sourcePath) {
        return Helper.GetVideoInfo(sourcePath);
    }
}

public class VideoConversionSettings {
    /// <summary>
    /// Video Conversion Settings ID
    /// </summary>
    public virtual Guid ID { get; set; }

    public string SourcePath { get; set; }

    public string TargetPath { get; set; }

    public TimeSpan Duration { get; set; }

    public string AspectRatio { get; set; }

    public int Height { get; set; }

    public int Width { get; set; }

    public string Resolution {
        get {

            return string.Format("{0}x{1}", Width, Height);
        }
    }

    public decimal VideoBitrate { get; set; }

    public string VideoFormat { get; set; }

    public decimal FrameRate { get; set; }

    public decimal AudioBitrate { get; set; }

    public string AudioFormat { get; set; }
}

