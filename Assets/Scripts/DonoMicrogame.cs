using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DonoMicrogame : MonoBehaviour
{
    string[] names = new string[]
        {
            "PooBear",
            "Rob1999",
            "Steve4000",
            "EpicGaming",
            "dave",
            "PopularGuy123",
            "AvidChatter2317",
            "Anonymous",
            "Your_Mom",
            "Dad",
            "CharzardCool09",
            "BluDragon",
            "Zool",
            "UrnJagar",
            "Nova",
            "BlackBird",
            "Toasty",
            "Redsio",
            "sno",
            "HollowKnightStan",
            "Charnchy77",
            "CroutonCriminal",
            "pixelphantom47293",
            "turbotosty",
            "lunarlynx2198",
            "shadowsnax",
            "mysticmango",
            "quantumquokka5821",
            "FrostFalcon",
            "zenoZoomer76842",
            "CactusNinja",
            "neonnacho6410",
            "blazeboba98999",
            "PhantomPancake11777",
            "turboTaco",
            "sirsaltys_socks99234",
            "Bortexvibes",
            "sneakysloth10103",
            "Spark",
            "Arago",
            "Icy",
            "Bluesio",
            "Niva",
            "Clara",
            "BigD",
            "Goose",
            "Hannah",
            "Lav",
            "IdiotSandwich",
            "DarkSoulsII",
            "PotatoMan",
            "sky902",
            "LegionofBlue",
            "Chum",
            "Tutarabeardie",
            "Floofinator",
            "K",
            "Azalea",
            "Swift",
            "Nebby",
            "Jordan",
            "Dantsu_Flame",
            "JunglePokke99",
            "OGURICAP_FOOD",
            "Agnes.Tachyon",
            "TamamoCrossLIGHTNING",
            "INARI_ONEEEE1",
            "Cafe_Manhattan",
            "UMAPYOI_densetsu",
            "NaritaTopRoad",
            "AVega_TwinLover",
            "TMOpera_gOd",
            "specialestWeek",
            "silencesuzuka",
            "TokaiBesto",
            "MejiroMcQueen",
            "GOLDSHIPGOLDSHIP",
            "ScarletRunner",
            "VODKABetter",
            "KingBlork",
            "Gabby",
            "CJ",
            "franksboy01",
            "EbonBold",
            "GildedDragon5",
            "Lotus",
            "Lilac",
            "Dinky",
            "Luna",
            "Sol",
            "Ziggy",
            "Nohbdy",
            "Ari1460",
            "NeoTurfMaster",
            "Qubit",
            "Aquacam"
        };

    string[] donoMsg = new string[]
        {
            "hey man",
            "You're so good at this game wow",
            "Keep it up!!!!!",
            "Can you turn the game audio up please",
            "u suck jk lol love you",
            "lmao this game is weird",
            "Are you okay? What is this??",
            "What game is this",
            "r u streaming tomorrow",
            "Can I play with you",
            "You're pretty good at multitasking",
            "DISTRACTION",
            ":3",
            "HIIIIIII",
            "Good morning",
            "Have a good stream",
            "Hi from work lol",
            "Thanks for streaming, keep me sane",
            "Your food is here",
            "maybe you should try dodging the meteors lol",
            "dave I love you pls respond",
            "Would you still stream if you were a worm?",
            "Smells like updog in this stream",
            "Dave, what is your opinion on Xenoblade Chronicles 2",
            "This chat sucks lol",
            "What is this stream, dawg",
            "Bro, mute your Davecord",
            "Doin good, Dave!",
            "The people trying to hack you are nerds",
            "your mom",
            "I'm Dantsu Flame!"
        };

    public bool pinged;
    private bool readDono;
    public bool setName;
    private float responseTimer;
    public string successFlag;
    [SerializeField] private TextMeshProUGUI donoNameText;
    [SerializeField] private TextMeshProUGUI donoMsgText;
    [SerializeField] private Canvas donoCanvas;
    [SerializeField] private TextMeshProUGUI pingedText;
    private bool flickering;

    void Start() {
        pinged = false;
        readDono = false;
        setName = false;
        responseTimer = 0f;
        flickering = false;
    }

    void FixedUpdate() {
        if(pinged) {
            readDono = false;
            
            if(!flickering) {
                StartCoroutine(FlickerPing());
            }

            if(!setName) {
                donoNameText.text = names[Random.Range(0,names.Length)] +
                                " GAVE $" + Random.Range(5,10000);
                donoMsgText.text = donoMsg[Random.Range(0,donoMsg.Length)];
                setName = true;

                donoCanvas.sortingOrder = 8;
            }

            responseTimer += Time.deltaTime;

            if(responseTimer >= 3.0f && !readDono) {
                pinged = false;
                successFlag = "fail";
                setName = false;
                responseTimer = 0f;
                donoCanvas.sortingOrder = -500;
            }

            int temp = 3 - (int)responseTimer;
            pingedText.text = temp.ToString();
        }
    }

    public void ReadDono() {
        pinged = false;
        successFlag = "success";
        readDono = true;
        setName = false;
        responseTimer = 0f;
        donoCanvas.sortingOrder = -500;
    }

    IEnumerator FlickerPing() {
        flickering = true;

        while(pinged) {
            pingedText.gameObject.SetActive(!pingedText.gameObject.activeSelf);

            yield return new WaitForSeconds(0.1f);
        }

        flickering = false;
    }
}
