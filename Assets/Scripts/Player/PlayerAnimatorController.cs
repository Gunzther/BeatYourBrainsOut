using BBO.BBO.TeamManagement;
using BBO.BBO.TeamManagement.UI;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField]
        private Texture[] spriteList;
        [SerializeField]
        private int spriteNumber = 0;

        private Renderer renderer;
        void Awake()
        {
            renderer = GetComponent<Renderer>();
        }
        void Update()
        {
            renderer.material.SetTexture("_MainTex", spriteList[spriteNumber < spriteList.Length - 1 ? spriteNumber : spriteList.Length - 1]);
        }
    }
}
