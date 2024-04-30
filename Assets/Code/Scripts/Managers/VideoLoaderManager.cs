using System;
using UnityEngine;
using UnityEngine.Video;
using Utility;

public class VideoLoaderManager
{
    private VideoPlaceholder _videoPlaceholder;
    private string _videoName;
    private VideoPlayer _videoPlayer;

    public VideoLoaderManager(VideoPlaceholder videoPlaceholder, string videoName, VideoPlayer videoPlayer)
    {
        _videoPlaceholder = videoPlaceholder;
        _videoName = videoName;
        _videoPlayer = videoPlayer;
    }

    public void LoadVideoFromURL(Action callback)
    {
        string requestUri = UriUtil.FormatUriUploads(_videoName);
        _videoPlayer.url = requestUri;
        GameObject quad = _videoPlayer.gameObject;
        quad.name = $"{quad.name} - {_videoName}";
        callback?.Invoke();
    }
}
