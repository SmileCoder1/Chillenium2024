using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ticker : MonoBehaviour
{
    private TMP_Text m_Text;

    static string[] headlines = new string[]
    {
        "Wolves introduced to Wisconsin killed all the cows. Cheese futures plummet.",
"Elon’s rocket blows up, there are no people in it, Tesla stock plummets.",
"Elon’s rocket blows up killing 100. Stock market sees resurgence amid Elon’s jailing.",
"Cat cryptocurrency becomes purr-fect investment, dogecoin left chasing its tail.",
"Giant inflatable duck parade causes havoc on Wall Street, rubber futures soar.",
"Clown shortage feared as circus bankruptcies hit all-time high, investors flock to red noses.",
"Alien invasion rumored to boost Earth defense stocks, conspiracy theorists rejoice.",
"Zombie apocalypse simulation goes wrong, canned food futures reach record highs.",
"World's largest rubber band ball bursts, rubber prices stretch to new heights.",
"Giant squid discovered in Bermuda Triangle, maritime insurance stocks sink.",
"Superhero convention ends in chaos, cape manufacturers see unexpected surge in demand.",
"Time travelers from the future disrupt stock market, investors scramble for crystal balls.",
"Robot uprising averted, tech stocks surge as humans regain control.",
"Unicorn farm IPO fails, investors discover mythical creatures were just goats with party hats.",
"Crop circle discovered in Kansas cornfield, corn futures soar amidst extraterrestrial speculation.",
"Mime protest shuts down financial district, silent but powerful message sends shockwaves through markets.",
"Fortune teller predicts market crash, investors flock to magic eight balls for guidance.",
"Treasure map found in old pirate chest, gold doubloons replace Bitcoin as hottest commodity.",
"World’s largest pillow fight breaks out on Wall Street, feather stocks skyrocket.",
"Leprechaun sightings increase, pot of gold ETFs outperform traditional assets.",
"Dancing flash mob disrupts trading floor, disco ball manufacturers see unexpected surge in sales.",
"Loch Ness Monster spotted, tourism stocks surge in Scotland.",
"Ghost haunting at Federal Reserve sends interest rates into uncharted territory.",
"UFO crash wipes out entire hedge fund, conspiracy theorists rejoice as Wall Street takes a hit.",
"Bigfoot brawl at trading floor, chaos erupts as hairy fists fly and stocks tumble.",
"Alien invasion sparks frenzied buying of tin foil hats, tin foil futures skyrocket.",
"Witch's curse sends stock prices plummeting, executives scramble for black magic remedies.",
"Zombie horde spotted in financial district, investors flee as undead demand brains and dividends.",
"Loch Ness Monster surfaces, causing tidal wave of panic selling in Scottish markets.",
"Ghost haunts Wall Street tycoon's mansion, poltergeist activity blamed for stock market woes.",
"Werewolf CEO accused of market manipulation, moonlit deals and howling profits under investigation.",
"Sasquatch spotted at insider trading ring, hairy accomplice sends shockwaves through regulatory agencies.",
"Vampire takeover of blood bank stocks, investors drained as undead hoard profits.",
"Presidential tweetstorm triggers market volatility, investors brace for rollercoaster ride.",
"Congress deadlock leads to government shutdown, stocks plummet as uncertainty grips markets.",
"Scandal rocks Capitol Hill, lobbyist stocks surge amidst political turmoil.",
"Alien invasion scares politicians into bipartisan cooperation, unity boosts stock market.",
"Cryptocurrency regulation sparks fierce debate in Congress, Bitcoin prices fluctuate wildly.",
"Presidential candidate caught in scandal, campaign contributions vanish, donors panic.",
"Political power struggle leads to regime change, emerging markets see upheaval.",
"Trade war escalates, tariffs send shockwaves through global supply chains.",
"Government corruption exposed, stocks of implicated companies crash.",
"Populist uprising sweeps through election, traditional party stocks tank while unconventional candidates rise.",
"Politician accidentally tweets grocery list instead of policy proposal, #AvocadoGate trends on Twitter.",
"Government shutdown blamed on Congress's inability to agree on pizza toppings for bipartisan lunch.",
"CEO caught playing Candy Crush during board meeting, blames lagging profits on addictive gameplay.",
"Presidential candidate's debate strategy revealed to be memorizing 'Yo Mama' jokes, gains unexpected popularity.",
"Senate filibuster thwarted by delivery of 100 pizzas, lawmakers too busy feasting to continue debate.",
"Congressman's attempt at TikTok dance goes viral, ignites bipartisan call for stricter regulations on political choreography.",
"Mayor's attempt to boost city's economy with world's largest rubber band ball ends in office chaos.",
"Financial analyst's forecast based solely on horoscope predictions, gains cult following on Wall Street.",
"Politician's proposal to rename national currency to 'Doge Dollars' sparks meme-inspired economic revolution.",
"Presidential debate moderator accidentally calls candidates by their Hogwarts House names, sparks bipartisan laughter and confusion.",

    };

    private float timeSinceShot = 100;
    private Coroutine br;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(TypeWriter("BULL MARKET", .02f));
            FindObjectOfType<BullMarket>().BULL_MARKET();
        }

        if(timeSinceShot > 10)
        {
            if (0 == Random.Range(0, 9))
            {
                timeSinceShot = 0;
                br = StartCoroutine(TypeWriter("BULL MARKET", .02f));
                FindObjectOfType<BullMarket>().BULL_MARKET();
            }
            else
            {

                timeSinceShot = 0;
                StartCoroutine(TypeWriter(headlines[UnityEngine.Random.Range(0, headlines.Length)], .02f));
            }
        }
    }

    private void FixedUpdate()
    {
        timeSinceShot += Time.fixedDeltaTime;
    }

    public IEnumerator TypeWriter(string text, float waitTime)
    {
        for (var i = 0; i <= text.Length; i++)
        {
            m_Text.text = text.Substring(0, i);

            yield return new WaitForSecondsRealtime(waitTime);
        }
    }
}
