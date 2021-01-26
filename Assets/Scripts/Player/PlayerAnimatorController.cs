using BBO.BBO.TeamManagement;
using BBO.BBO.TeamManagement.UI;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField]
        private Texture[] spriteList = default;

        [SerializeField]
        private int spriteNumber = 0;

        [SerializeField]
        private Animator playerAnimator = default;

        private Renderer renderer = default;

        public void SetTrigger(int triggerHash)
        {
            playerAnimator.SetTrigger(triggerHash);
        }

        public bool IsInState(string stateName)
        {
            return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            renderer.material.SetTexture("_MainTex", spriteList[spriteNumber < spriteList.Length - 1 ? spriteNumber : spriteList.Length - 1]);
        }
    }
}
