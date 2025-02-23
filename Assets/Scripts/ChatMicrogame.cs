using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatMicrogame : MonoBehaviour
{
    private float cooldown;
    public bool chatClicked;
    private bool flickering;
    private bool pinged;
    [SerializeField] private TextMeshProUGUI pingedText;
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
    string[] messages = new string[]
        {
            "lol",
            "lmao",
            "hi",
            "hey",
            "hi YT",
            "What's going on??",
            "what game is this",
            "Ads in stream??? Wtf",
            "Who are you messaging lmao",
            "Dude focus on the game",
            "First time catching a stream!",
            "Is this part of the stream?",
            "This game sucks lol",
            "play something more interesting",
            "u suck LMAO",
            "Hi chat",
            "Why are you coding",
            "???",
            "Are you okay dude",
            "Hello??",
            ":)",
            "<3",
            "This message was removed by automod.",
            "I'M UR BIGGEST FAN",
            "Anyone want to bet on how long this run lasts?",
            "Wait",
            "Are you getting hacked or smth",
            "What is this",
            "Who is this guy",
            "When is the PO Box stream",
            "Why are you so locked in?",
            "Bro got malware LMAO",
            "Is this part of the game",
            "This is boooooriiiiinnnngggg",
            "YAWNNN",
            ":3",
            "Mods do your job please",
            ":pogchamp:",
            ":fire:",
            ":thumbsdown:",
            "DAAAAVEEEEEEE",
            "Play DF2 instead",
            "This is putting me to sleep",
            "Where'''s the content",
            "Dave are you gonna release your OS",
            "AAAAAA",
            "Can u turn the volume u p",
            "How long r u streaming 4",
            "R u stupid",
            "Can someone gift me a suubbbbbb",
            "plssss",
            "hhasdfhsdfaasfjlkhasdlkjhsdafljkh",
            "omg my cat typed that last one sry",
            "Ahahahahahahhahhaha",
            "Dog cam when",
            "Dave can you move your cam we cant see",
            "whoa these graphics are awesome",
            "where did you find this game lol",
            "bargain bin type stuff",
            "dave do you like me",
            "fisrt tim catching a stream",
            "your last vid was so funny lmaoo",
            "Hi Dave :)",
            "lock in",
            "is he even looking at chat",
            "is this prerecorded",
            "is he ok",
            "im new",
            "wake me up when something interesting happens",
            "i could do better",
            "THIS is what gets 1k views?",
            "i love dave",
            "merch when??",
            "i would spend so much on a dave shirt lol",
            "the coveted dave plush",
            "how is he so good",
            "this guys a master programmer",
            "whoaaa big dono",
            "aw he turned tts off :(",
            "when are you going back to dave corp",
            "daverune is peam",
            "peak",
            "you gotta play dave fortress 2 again the update is wild lol",
            "dude you gotta stop these ads",
            "isnt this against tos",
            "bro its dave",
            "pls tell me ur streaming tomorrow",
            "hi from my car",
            "daveing and driving",
            "wtf",
            "what's wrong with your computer",
            "yeah fr",
            "ong",
            "this tbh",
            "how much for a date w dave",
            "idc what hes doing i just like looking at him",
            "make the cam bigger",
            "bro your davecord is showing",
            "who is that",
            "when is library of dave",
            "manual antivirus",
            "zool",
            "play daveblue",
            "play with chat when?",
            "dude i would kill for a dave's chat plays pkmn",
            "who are these people",
            "um is he even looking at chat",
            "hEYYY",
            "I'm Dantsu Flame!",
            "hes just getting lucky",
            "mm burger..",
            "shovelware is righrt",
            "are these ads real mlamooo"
        };
    [SerializeField] private TextMeshProUGUI chat;
    private float chatCD;

    void Start()
    {
        cooldown = 0f;
        chatCD = 0f;
    }

    void FixedUpdate()
    {
        if(chatCD <= 0f) {
            chat.text += "\n" + names[Random.Range(0,names.Length)] + ": " + messages[Random.Range(0,messages.Length)];
            chatCD = Random.Range(0.1f,0.5f);
        }
        chatCD -= Time.deltaTime;

        if(cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
        if(cooldown <= 0) {
            if(!flickering) {
                pingedText.gameObject.SetActive(true);
                StartCoroutine(FlickerPing());
            }
            
            pinged = true;       
        }
    }

    public void RespondToChat() {
        if(cooldown <= 0) {
            pinged = false;
            chatClicked = true;
            pingedText.gameObject.SetActive(false);
        }
        cooldown = 15f;
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
