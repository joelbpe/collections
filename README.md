<p>
# Shipwaze

Started a small project that will ask users to enter the from and to locations where the item(s) is going to be move using less than truck loads apis from 3rd party logistic companies. Eg. USP, Fedex, Dominion, AAA Cooper, Pilot Freight and more... I have at the moment a website using Unify Template, MVC framework, C#. These will be my core components Google map apis, KnockoutJS, Jquery, requireJS and typescript to implement the product. <br /> <b>Your suggestion is greatly appreciated.</b>

<h2><a href='https://htmlstream.com/preview/unify-v1.9.8/index.html'>Unify</a></h2>
is a clean and fully responsive incredible Template
</p>
<p>
# Video Converter (FFmpeg )

C# wrapper that allows you to convert media type to any format using 

<a href='https://ffmpeg.org/about.html.'>FFmpeg</a> is the leading multimedia framework, able to decode, encode, transcode, mux, demux, stream, filter and play pretty much anything that humans and machines have created. It supports the most obscure ancient formats up to the cutting edge. No matter if they were designed by some standards committee, the community or a corporation. It is also highly portable: FFmpeg compiles, runs, and passes our testing infrastructure FATE across Linux, Mac OS X, Microsoft Windows, the BSDs, Solaris, etc. under a wide variety of build environments, machine architectures, and configurations.

<h3>Eg.</h3>

```
public void ConvertVideo() {
  var toSettings = new VideoConversionSettings();
      toSettings.VideoBitrate = 16;
      toSettings.FrameRate = 23.97;
      toSettings.AudioBitrate = 192;
      toSettings.Width = 1024;
      toSettings.Height = 512;
      toSettings.VideoFormat = VideoFormat.mkv;
      toSettings.SourcePath = @"\SourcePath";
      toSettings.TargetPath = @"\TargetPath";

      var convertToVideo = new VideoConversion(toSettings);
	
      //Capture image of the first frame
      convertToVideo.ToJpeg(); 

      //Convert Current Video to the specified settings *.mkv
      convertToVideo.ToNewVideoFormat();
}
```
</p>
