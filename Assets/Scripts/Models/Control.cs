using Assets.Scripts.Controllers;
using Assets.Scripts.Models.GUI;
using Assets.Scripts.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Models
{
    public class Control : MonoBehaviour
    {
        private static Control instance;
        [SerializeField] private GUIPlayGame gUIPlayGame;
        public LayerMask PlatformLayerMask = 2000000;

        private Platform currentPlatform = null;
        private Platform selectedPlatform = null;

        private GameObject reviewTurret;
        private Shop.ShopItem itemBuild;

        void Awake()
        {
            instance = this;
        }

        void Update()
        {
            CheckUserInput();
        }

        public static Platform GetPlatformTarget()
        {
            return instance.selectedPlatform;
        }

        public static void MenuItemSelected(Shop.ShopItem item)
        {
            if (BaseGameCTLs.Instance.State == EGameState.PLAYING)
            {
                if (item != null && instance.selectedPlatform != null)
                {
                    EBuildTurretState buildTurretState = instance.selectedPlatform.BuildTurret(item);
                    if (buildTurretState == EBuildTurretState.Sale)
                    {
                        instance.Reset();
                    }
                    else
                    {
                        instance.gUIPlayGame.SetMenuShop(instance.selectedPlatform.GetListShop());
                        instance.gUIPlayGame.SetInfo(instance.selectedPlatform.GetTurret);
                        instance.selectedPlatform.ShowRangeIndicator();
                    }
                }
                else
                {
                    if (instance.reviewTurret != null)
                    {
                        Destroy(instance.reviewTurret);
                    }

                    instance.itemBuild = item;

                    if (instance.itemBuild != null)
                    {
                        instance.reviewTurret = Instantiate(PrefabCTL.Instance.TurrretReview(instance.itemBuild.Value));
                        instance.reviewTurret.SetActive(false);
                        if (instance.currentPlatform != null)
                        {
                            instance.reviewTurret.SetActive(true);
                            instance.reviewTurret.transform.position = instance.currentPlatform.PositionOnPlatform;
                        }
                    }

                }
            }
        }

        private void Reset()
        {
            if (reviewTurret != null)
            {
                Destroy(reviewTurret);
                reviewTurret = null;
            }
            currentPlatform = null;
            selectedPlatform = null;
            itemBuild = null;
            gUIPlayGame.ResetMenuShop();
            RangeIndicatorSystem.HideTarget();
            RangeIndicatorSystem.HideReview();
            TooltipSystem.HideNotEnoughMoneyPanel();
        }

        private void CheckUserInput()
        {
            if (reviewTurret!=null)
            {
                if (BaseGameCTLs.Instance.Money < itemBuild.Cost)
                {
                    TooltipSystem.ShowNotEnoughMoneyPanel();
                } else
                {
                    TooltipSystem.HideNotEnoughMoneyPanel();
                }
            }
            if (BaseGameCTLs.Instance.State == EGameState.PLAYING)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    Reset();
                    return;
                }
                if (!gUIPlayGame.IsPointerOverUIObject(new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, 1000, PlatformLayerMask.value))
                    {
                        Debug.DrawLine(Camera.main.transform.position, hit.point);
                        Platform platform = hit.collider.GetComponent<Platform>();
                        if (platform != currentPlatform)
                        {
                            //if (currentPlatform != null)
                            //{
                            //    currentPlatform.SetState(EPlatformState.Hover);
                            //}
                            currentPlatform = platform;
                            if (currentPlatform != null)
                            {
                                //currentPlatform.SetState(EPlatformState.Hover);
                                if (reviewTurret != null)
                                {
                                    reviewTurret.SetActive(true);
                                    currentPlatform.ShowRangeIndicator(itemBuild.Range.value);
                                    reviewTurret.transform.position = currentPlatform.PositionOnPlatform;
                                }
                            }
                            else
                            {
                                if (reviewTurret != null)
                                {
                                    reviewTurret.SetActive(false);
                                    RangeIndicatorSystem.HideTarget();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (currentPlatform != null)
                        {
                            //currentPlatform.SetState(EPlatformState.Normal);
                            currentPlatform = null;
                        }
                        if (reviewTurret != null)
                        {
                            reviewTurret.SetActive(false);
                            RangeIndicatorSystem.HideTarget();
                        }
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (currentPlatform != null)
                        {
                            if (reviewTurret != null && !currentPlatform.HaveTurret)
                            {
                                currentPlatform.BuildTurret(itemBuild);
                                Reset();
                            }
                            else if (currentPlatform.HaveTurret)
                            {
                                if (selectedPlatform != currentPlatform)
                                {
                                    selectedPlatform = currentPlatform;
                                    gUIPlayGame.SetMenuShop(selectedPlatform.GetListShop());
                                    gUIPlayGame.SetInfo(selectedPlatform.GetTurret);
                                    selectedPlatform.ShowRangeIndicator();
                                }
                            }
                            else
                            {
                                Reset();
                            }
                        }
                        else
                        {
                            Reset();
                        }

                        if (reviewTurret != null)
                        {
                            Destroy(reviewTurret);
                        }
                    }
                }
                else
                {
                    currentPlatform = null;
                }
            }
        }

        public void PauseResumeGame()
        {
            BaseGameCTLs.Instance.PauseGame();
            UIGameStatus.ShowSetting();
        }
    }
}
