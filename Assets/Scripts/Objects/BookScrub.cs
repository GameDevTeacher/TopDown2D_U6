using UnityEngine;

namespace Objects
{
    [CreateAssetMenu(fileName = "New Book", menuName = "Scrubs/Interactables/Book")]
    public class BookScrub : ScriptableObject
    {
        public new string name;
        public Sprite image;
        [TextArea(2, 30)] public string contents;
    }
}