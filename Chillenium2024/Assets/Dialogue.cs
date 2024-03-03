using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    private TMP_Text m_Text;
    private AudioSource s;
    public AudioClip normal;
    public AudioClip mad;
    public AudioClip mad2;
    // Start is called before the first frame update
    void Start()
    {
                m_Text = GetComponent<TMP_Text>();
        s = gameObject.AddComponent<AudioSource>();
        StartCoroutine(TypeWriter());
    }

    public IEnumerator TypeWriter()
    {

        (string, AudioClip)[] dialogue = new (string, AudioClip)[]
        {
            ("I have to get to the penthouse", normal),
            ("Need to DRS my GME before robinhood screws me over", normal),
            ("It's just a temporary dip in the market, like a healthy correction, you know?", normal),
            ("The bears are trying to shake weak hands, but we diamond hands are built for this volatility.", normal),
            ("Buy the dip, stay chill, and remember, we're in it for the long haul.", normal),
            ("...", normal),
            ("Oh my god they got my wife", mad),
            ("I will treasure her monkey dog in her memory", normal),
            ("*sad monkey noises*...", normal),
            ("NOOOO Not the monkey dog too :(", mad2),
            ("They really did it this time", mad),
            ("I need to get my computer before the Robinhood bugs do!", normal),
            ("The elevator is out! I'm going to have to find another way up.", normal),
            ("My GME is all I have left. Gotta hang in there ;)", mad),
        };

        yield return new WaitForSecondsRealtime(1f);

        for (var text = 0; text < dialogue.Length; text++)
        {
            yield return new WaitForSecondsRealtime(1f);
            s.clip = dialogue[text].Item2;
            if(s.clip == normal)
                s.time = new float[] { 0f, 5.5f }[ Random.Range(0, 2)];
            s.Play();
            for (var i = 0; i <= dialogue[text].Item1.Length; i++)
            {
                m_Text.text = dialogue[text].Item1.Substring(0, i);

                yield return new WaitForSecondsRealtime(.015f);
            }
            yield return new WaitForSecondsRealtime(1f);
            s.Stop();

        }

        yield return new WaitForSecondsRealtime(1f);

        SceneManager.LoadScene("Menu");
    }

}
