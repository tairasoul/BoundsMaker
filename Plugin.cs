using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BoundsMaker
{
    [BepInPlugin("tairasoul.boundsmaker", "BoundsMaker", "1.0.1")]
    class Plugin : BaseUnityPlugin
    {
        internal Vector3 StartPos;
        internal Vector3 EndPos;
        internal Text SizeText;
        internal Text PositionText;
        internal ConfigEntry<KeyCode> keybind;
        internal bool CreatingBounds = false;
        internal VisualiserComponent component;

        void Awake()
        {
            keybind = Config.Bind("Keybind", "main_key", KeyCode.K, "The key that starts or ends the creation of a bound.");
        }

        void Start()
        {
            SceneManager.activeSceneChanged += SceneChanged;
            //GameObject ComponentContainer = new("BoundsMaker");
            //DontDestroyOnLoad(ComponentContainer);
            component = gameObject.AddComponent<VisualiserComponent>();
        }

        void SceneChanged(Scene old, Scene newS)
        {
            if (newS.name != "Intro" && newS.name != "Menu")
            {
                GameObject Area = GameObject.Find("UI/ui/Area");
                GameObject Size = GameObject.Instantiate(Area);
                GameObject Pos = GameObject.Instantiate(Area);
                Pos.name = "Position";
                Size.name = "Size";
                Pos.transform.SetParent(Area.transform.parent);
                Size.transform.SetParent(Area.transform.parent);
                Destroy(Size.GetComponent<Locations>());
                Destroy(Pos.GetComponent<Locations>());
                SizeText = Size.GetComponent<Text>();
                PositionText = Pos.GetComponent<Text>();
                Size.GetComponent<RectTransform>().anchoredPosition = new(-435.8934f, -165.9394f);
                Pos.GetComponent<RectTransform>().anchoredPosition = new(-438.0144f, -199.0975f);
                SizeText.text = "";
                PositionText.text = "";
                Size.transform.localScale = new(0.5f, 0.5f, 0.5f);
                Pos.transform.localScale = new(0.5f, 0.5f, 0.5f);
            }
        }

        bool keyDown = false;
        bool newBounds = true;

        void Update() 
        {
            if (!keyDown && UnityInput.Current.GetKeyDown(keybind.Value))
            {
                CreatingBounds = !CreatingBounds;
                keyDown = true;
            }
            else
            {
                keyDown = false;
            }
            GameObject Sen = GameObject.Find("S-105.1");
            if (CreatingBounds)
            {
                if (newBounds)
                {
                    newBounds = false;
                    StartPos = Sen.transform.position;
                }
                EndPos = Sen.transform.position;
                Vector3 size = new(
                    Mathf.Abs(StartPos.x - EndPos.x),
                    Mathf.Abs(StartPos.y - EndPos.y),
                    Mathf.Abs(StartPos.z - EndPos.z)
                );

                Vector3 center = (StartPos + EndPos) / 2f;

                component.bounds = new(center, size);
                SizeText.text = $"Size: {size.x} {size.y} {size.z}";
                PositionText.text = $"Pos: {center.x} {center.y} {center.z}";
            }
            else
            {
                newBounds = true;
            }
        }
    }
}