function startVideo(src, cameraDeviceId)
{
  if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia)
  {
    navigator
      .mediaDevices
      .getUserMedia({
        video: {
          deviceId: {
            exact: cameraDeviceId
          }
        }
      })
      .then(function (stream)
      {
        let video = document.getElementById(src);

        if ("srcObject" in video)
        {
          video.srcObject = stream;
        }
        else
        {
          video.src = window.URL.createObjectURL(stream);
        }

        video.onloadedmetadata = function (e)
        {
          video.play();
        };
      });
  }
}

function getFrame(src, dest, dotNetHelper)
{
  let video = document.getElementById(src);
  let canvas = document.getElementById(dest);

  canvas
    .getContext('2d')
    .drawImage(video, 0, 0, 320, 240);

  let dataUrl = canvas.toDataURL("image/jpeg");
  dotNetHelper.invokeMethodAsync('ProcessImage', dataUrl);
}

function getCameraDeviceList()
{
  return navigator
    .mediaDevices
    .enumerateDevices()
    .then(c => c.filter(o => o.kind === "videoinput"));
}