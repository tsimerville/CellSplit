using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{
    public void playButton()
    {
        Time.timeScale = 1f;
    }
    public void pauseButton()
    {
        Time.timeScale = 0f;
    }
    public void forwardButton()
    {
        Time.timeScale = 2.5f;
    }
    public void forward2Button()
    {
        Time.timeScale = 5f;
    }
}
