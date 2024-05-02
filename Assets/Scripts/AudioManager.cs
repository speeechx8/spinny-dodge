using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager ins;

    void Awake()
    {
        if (ins != null)
        {
            Debug.LogError("More than 1 Audio Manager.");
        }
        else
        {
            ins = this;
        }
    }
    #endregion

    [SerializeField] private AudioClip scoreSound;

    public AudioClip ScoreSound
    {
        get
        {
            return scoreSound;
        }
    }

}
