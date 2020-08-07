using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioSystem;

public class Grandchild : MonoBehaviour
{
    private float previous_change = 0;
    private string chosen_clip = "";

    private ASAudioManager audioManager;

    enum State
    {
        Showing,
        Caught,
        Kissed,
        Hiding
    }
    State current_state = State.Hiding;


    private string[] audioclips = new string[] { "hed", "avsha", };

    // Start is called before the first frame update
    void Start()
    {
        audioManager = ASAudioManager.ins;
        if (audioManager == null) Debug.LogError("No ASAudioManager?!");

        hide();
    }


    void hide()
    {
        this.transform.position = new Vector3(-100, -100, 0);
        previous_change = Time.fixedTime;
        current_state = State.Hiding;
    }


    // Update is called once per frame
    void Update()
    {

        if (current_state == State.Caught) {
            current_state = State.Kissed;
            audioManager.stop(chosen_clip);
            audioManager.Play("kiss");
            previous_change = Time.fixedTime;
        }
        else if (current_state == State.Kissed && Time.fixedTime-previous_change > 2)
        {
            hide();
            current_state = State.Hiding;
            audioManager.stop("kiss");
            previous_change = Time.fixedTime;

        }
        else if (current_state == State.Hiding && Time.fixedTime - previous_change > 4)
        {
            current_state = State.Showing;
            previous_change = Time.fixedTime;

            int x = Random.Range(-5, 5);
            int y = Random.Range(-5, 5);
            this.transform.position = new Vector3(x, y, 0);

            chosen_clip = audioclips[Random.Range(0, audioclips.Length)];
            audioManager.Play(chosen_clip);

        }
    }



    void OnMouseDown()
    {
        current_state = State.Caught;
    }

}
