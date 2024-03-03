using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private TMP_Text m_Text;
    // Start is called before the first frame update
    void Start()
    {
                m_Text = GetComponent<TMP_Text>();
        StartCoroutine(TypeWriter());
    }

    public IEnumerator TypeWriter()
    {

        string[] dialogue = new string[]
        {
            "I have to get to the penthouse",
            "Need to DRS my GME before robinhood screws me over",
            "It's just a temporary dip in the market, like a healthy correction, you know?",
            "The bears are trying to shake weak hands, but we diamond hands are built for this volatility.",
            "Buy the dip, stay chill, and remember, we're in it for the long haul.",
            "...",
            "Oh my god they got my wife",
            "I will treasure her monkey dog 'Venus Fly Trap' in her memory",
            "*sad monkey noises*",
            "NOOOO Not the monkey dog too :(",
            "They really did it this time",
            "I need to get my computer before the Robinhood bugs do!",
            "My GME is all I have left. Gotta hang in there"
        };


        for (var text = 0; text < dialogue.Length; text++)
        {
            yield return new WaitForSecondsRealtime(2f);

            for (var i = 0; i <= dialogue[text].Length; i++)
            {
                m_Text.text = dialogue[text].Substring(0, i);

                yield return new WaitForSecondsRealtime(.01f);
            }

        }
    }

}
