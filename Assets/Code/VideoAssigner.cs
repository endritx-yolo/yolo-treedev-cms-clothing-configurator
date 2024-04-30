using UnityEngine;
using UnityEngine.Video;

public class VideoAssigner : MonoBehaviour
{
    [SerializeField] private string _fileName;

    private VideoPlayer _videoPlayer;

    private void Awake() => _videoPlayer = GetComponent<VideoPlayer>();
    private void Start() => _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, _fileName);
}