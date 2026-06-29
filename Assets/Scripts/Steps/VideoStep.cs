using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoStep : Step
{

    [SerializeField] VideoClip videoClip;

    public override void Activate()
    {

        StartCoroutine(PlayVideo());

        base.Activate();

    }

    public override void Reactivate()
    {
        Debug.Log("[video] Reactivation");
        StartCoroutine(PlayVideo());
        base.Reactivate();
    }

    public override void Succeeded()
    {
        base.Succeeded();
    }

    public override void Failed()
    {
        base.Failed();
    }

    private IEnumerator PlayVideo()
    {
        if (MediaManager.Instance.videoPlayer != null)
        {
            var vp = MediaManager.Instance.videoPlayer;
            var audioSrc = MediaManager.Instance.audioSource;

            vp.Stop();
            vp.clip = null;
            vp.playOnAwake = false;

            audioSrc.Stop();
            audioSrc.clip = null;
            audioSrc.loop = false;
            audioSrc.playOnAwake = false;
            audioSrc.volume = 1.0f;

            yield return null;

            vp.audioOutputMode = VideoAudioOutputMode.AudioSource;
            vp.controlledAudioTrackCount = 1;
            vp.EnableAudioTrack(0, true);
            vp.SetTargetAudioSource(0, audioSrc);

            vp.clip = videoClip;
            vp.Prepare();

            while (!vp.isPrepared) {
                yield return null;
            }

            vp.SetTargetAudioSource(0, audioSrc);

            vp.Play();

            while (vp.isPlaying) {
                yield return null;
            }

            if (autoStep)
            {
                StepManager.Instance.OnStepSuccess();
            }
        }
 

    }

}
