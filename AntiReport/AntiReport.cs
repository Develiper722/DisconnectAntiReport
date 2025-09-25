using System;
using BepInEx;
using Photon.Pun;
using UnityEngine;

namespace AntiReport
{
    // Token: 0x02000002 RID: 2
    [BepInPlugin("com.Figureheads_1.gorillatag.AntiReport", "AntiReport", "1.0.0")]
    public class AntiReport : BaseUnityPlugin
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private void Update()
        {
            // This check ensures the code only runs when the player is in a multiplayer room.
            if (PhotonNetwork.InRoom)
            {
                try
                {
                    foreach (GorillaPlayerScoreboardLine gorillaPlayerScoreboardLine in GorillaScoreboardTotalUpdater.allScoreboardLines)
                    {
                        bool flag = gorillaPlayerScoreboardLine.linePlayer == NetworkSystem.Instance.LocalPlayer;
                        if (flag)
                        {
                            Transform transform = gorillaPlayerScoreboardLine.reportButton.gameObject.transform;
                            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                            {
                                bool flag2 = vrrig != GorillaTagger.Instance.offlineVRRig;
                                if (flag2)
                                {
                                    float num = Vector3.Distance(vrrig.rightHandTransform.position, transform.position);
                                    float num2 = Vector3.Distance(vrrig.leftHandTransform.position, transform.position);
                                    bool flag3 = num < this.reportButtonDistance || num2 < this.reportButtonDistance;
                                    if (flag3)
                                    {
                                        NetworkSystem.Instance.ReturnToSinglePlayer();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        // Token: 0x04000001 RID: 1
        public float reportButtonDistance = 0.35f;
    }
}
