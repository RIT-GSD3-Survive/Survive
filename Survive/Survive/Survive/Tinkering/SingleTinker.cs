using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survive.Tinkering {
    class SingleTinker {
        private Player host;

        private Button[] btns;
        private Button newGunButton;

        private int weapScrollY = 0, partScrollY = 0;
        private TinkerBackend.BitType bt = TinkerBackend.BitType.None;

        private string error = "";

        public SingleTinker(Player p) {
            host = p;

            newGunButton = new Button(Resources.Tinker.NewGun, false, 60, 0, delegate() {
                if(host.TBackend.WIP == null) {
                    host.TBackend.BeginGun();
                    foreach(int i in new int[] { 6, 7, 8, 9, 10 }) {
                        btns[i].Visible = true;
                        btns[i].Active = true;
                    }
                } else {
                    try {
                        host.TBackend.FinishGun();
                        bt = TinkerBackend.BitType.None;
                        foreach(int i in new int[] { 4, 5, 6, 7, 8, 9, 10 }) {
                            btns[i].Visible = false;
                            btns[i].Active = false;
                        }
                        error = "";
                    } catch (Exception ex) {
                        error = ex.Message;
                    }
                }
            });

            btns = new Button[] {
                newGunButton, 
                new Button(Resources.Tinker.SingleBack, false, 346, 20, delegate() { // Exit Tinkering
                    host.Tinkering = false;
                    host.TBackend.WIP = null;
                    bt = TinkerBackend.BitType.None;
                    error = "";
                    weapScrollY = 0;

                    foreach(int i in new int[] { 4, 5, 6, 7, 8, 9, 10 }) {
                        btns[i].Visible = false;
                        btns[i].Active = false;
                    }
                }),
                new Button(Resources.Tinker.ScrollUp, true, 240, 20, delegate() { // Scroll Weapons Up
                    weapScrollY = Math.Max(0, weapScrollY - 4);
                }),
                new Button(Resources.Tinker.ScrollDn, true, 240, 468, delegate() { // Scroll Weapons Down
                    weapScrollY = Math.Min(Math.Max(0, 100 * (host.Weapons.Count - 4)), weapScrollY + 4);
                }),
                new Button(Resources.Tinker.ScrollUp, true, 542, 20, delegate() { // Scroll Parts Up
                    partScrollY = Math.Max(0, partScrollY - 4);
                }),
                new Button(Resources.Tinker.ScrollDn, true, 542, 468, delegate() { // Scroll Parts Down
                    if(bt != TinkerBackend.BitType.None) {
                        int count = 0;
                        switch(bt) {
                            case TinkerBackend.BitType.Barrel:
                                count = host.TBackend.GetBits<GunBarrel>().Count;
                                break;
                            case TinkerBackend.BitType.Body:
                                count = host.TBackend.GetBits<GunBody>().Count;
                                break;
                            case TinkerBackend.BitType.Clip:
                                count = host.TBackend.GetBits<GunClip>().Count;
                                break;
                            case TinkerBackend.BitType.Scope:
                                count = host.TBackend.GetBits<GunScope>().Count;
                                break;
                            case TinkerBackend.BitType.Stock:
                                count = host.TBackend.GetBits<GunStock>().Count;
                                break;
                        }
                        partScrollY = Math.Min(Math.Max(0, 100 * (count - 5)), partScrollY + 4);
                    }
                }),
                new Button(Resources.GunSheet, SurviveGame.gunImagesList["GunBarrel"], 2, false, 310, 175, delegate() {
                    bt = TinkerBackend.BitType.Barrel;
                    partScrollY = 0;
                    foreach(int i in new int[] { 4, 5 }) {
                        btns[i].Visible = true;
                        btns[i].Active = true;
                    }
                }),
                new Button(Resources.GunSheet, SurviveGame.gunImagesList["GunBody"], 2, false, 370, 175, delegate() {
                    bt = TinkerBackend.BitType.Body;
                    partScrollY = 0;
                    foreach(int i in new int[] { 4, 5 }) {
                        btns[i].Visible = true;
                        btns[i].Active = true;
                    }
                }),
                new Button(Resources.GunSheet, SurviveGame.gunImagesList["GunClip"], 2, false, 360, 200, delegate() {
                    bt = TinkerBackend.BitType.Clip;
                    partScrollY = 0;
                    foreach(int i in new int[] { 4, 5 }) {
                        btns[i].Visible = true;
                        btns[i].Active = true;
                    }
                }),
                new Button(Resources.GunSheet, SurviveGame.gunImagesList["GunScope"], 2, false, 370, 150, delegate() {
                    bt = TinkerBackend.BitType.Scope;
                    partScrollY = 0;
                    foreach(int i in new int[] { 4, 5 }) {
                        btns[i].Visible = true;
                        btns[i].Active = true;
                    }
                }),
                new Button(Resources.GunSheet, SurviveGame.gunImagesList["GunStock"], 2, false, 430, 175, delegate() {
                    bt = TinkerBackend.BitType.Stock;
                    partScrollY = 0;
                    foreach(int i in new int[] { 4, 5 }) {
                        btns[i].Visible = true;
                        btns[i].Active = true;
                    }
                })
            };

            foreach(int i in new int[] { 4, 5, 6, 7, 8, 9, 10 }) {
                btns[i].Visible = false;
                btns[i].Active = false;
            }
        }

        public void Update() {
            newGunButton.Y = (100 * host.Weapons.Count) + 34 - weapScrollY;
            if(host.Controls.CurrentMS.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) {
                foreach(Button btn in btns) {
                    if(btn.CheckClicked(host.Controls.CurrentMS.X, host.Controls.CurrentMS.Y)) return;
                }
                // Didn't click a certain button...
                if(host.Controls.PreviousMS.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released) {
                    if(host.Controls.CurrentMS.X > -1 && host.Controls.CurrentMS.X < 201) { // In weapons list
                        int index = (host.Controls.CurrentMS.Y + weapScrollY) / 100;
                        if(index > host.Weapons.Count || index < 0) return;
                        if(index == host.Weapons.Count) {
                            newGunButton.Clicked();
                        } else {
                            Weapon weap = host.Weapons[index];
                            if(weap is WeaponCustomizable) {
                                host.TBackend.ModGun((WeaponCustomizable)weap);
                                foreach(int i in new int[] { 6, 7, 8, 9, 10 }) {
                                    btns[i].Visible = true;
                                    btns[i].Active = true;
                                }
                            }
                        }
                    } else if(bt != TinkerBackend.BitType.None && host.Controls.CurrentMS.X > 599 && host.Controls.CurrentMS.X < 801) { // In parts list
                        int index = (host.Controls.CurrentMS.Y + partScrollY) / 100;
                        switch(bt) {
                            case TinkerBackend.BitType.Barrel:
                                List<GunBarrel> barrels = host.TBackend.GetBits<GunBarrel>();
                                if(index >= barrels.Count || index < 0) return;
                                else host.TBackend.WIP.Barrel = barrels[index];
                                return;
                            case TinkerBackend.BitType.Body:
                                List<GunBody> bodies = host.TBackend.GetBits<GunBody>();
                                if(index >= bodies.Count || index < 0) return;
                                else host.TBackend.WIP.Body = bodies[index];
                                return;
                            case TinkerBackend.BitType.Clip:
                                List<GunClip> clips = host.TBackend.GetBits<GunClip>();
                                if(index >= clips.Count || index < 0) return;
                                else host.TBackend.WIP.Clip = clips[index];
                                return;
                            case TinkerBackend.BitType.Scope:
                                List<GunScope> scopes = host.TBackend.GetBits<GunScope>();
                                if(index >= scopes.Count || index < 0) return;
                                else host.TBackend.WIP.Scope = scopes[index];
                                return;
                            case TinkerBackend.BitType.Stock:
                                List<GunStock> stocks = host.TBackend.GetBits<GunStock>();
                                if(index >= stocks.Count || index < 0) return;
                                else host.TBackend.WIP.Stock = stocks[index];
                                return;
                        }
                    }
                }
            } else {
                foreach(Button btn in btns) {
                    btn.Last = false;
                }
            }

        }

        public void Draw(SpriteBatch sb) {
            sb.GraphicsDevice.Clear(Color.LightGray);
            foreach(Button btn in btns) {
                btn.Draw(sb);
            }
            if(error != "") {
                sb.DrawString(Resources.CourierSmall, error, new Vector2(5, (100 * host.Weapons.Count) + 70 - weapScrollY), Color.Red);
            }
            for(int i = 0; i < host.Weapons.Count; i++) {
                Weapon thisWeap = host.Weapons[i];
                Vector2 location = new Vector2(0, i * 100 - weapScrollY);
                sb.Draw(Resources.Tinker.Box, location, (thisWeap is WeaponStock)?Color.FromNonPremultiplied(255,255,220,255):Color.White);
                sb.Draw(Resources.GunSheet, new Rectangle((int)location.X + 10, (int)location.Y + 10, SurviveGame.gunImagesList[thisWeap.Type].Width * ((thisWeap.Type == "Pistol") ? 2 : 1), SurviveGame.gunImagesList[thisWeap.Type].Height * ((thisWeap.Type == "Pistol") ? 2 : 1)), SurviveGame.gunImagesList[thisWeap.Type], Color.White);
                String name = thisWeap.Name;
                if(name.Length > 10) {
                    name = name.Substring(0, 7) + "...";
                }
                sb.DrawString(Resources.Courier, name, location + new Vector2(50, 10), Color.Black);
                sb.DrawString(Resources.CourierSmall, "Power:  " + thisWeap.AttackPower, location + new Vector2(10, 40), Color.Black);
                sb.DrawString(Resources.CourierSmall, "Weight: " + thisWeap.Weight, location + new Vector2(10, 70), Color.Black);
                sb.DrawString(Resources.CourierSmall, "Rate: " + thisWeap.FireRate, location + new Vector2(110, 40), Color.Black);
                sb.DrawString(Resources.CourierSmall, "Acc:  " + thisWeap.Accuracy, location + new Vector2(110, 70), Color.Black);
            }
            if(host.TBackend.WIP != null) {
                Vector2 location = new Vector2(320, 250);
                sb.DrawString(Resources.CourierSmall, "Power:  " + host.TBackend.WIP.AttackPower, location + new Vector2(10, 40), Color.Black);
                sb.DrawString(Resources.CourierSmall, "Weight: " + host.TBackend.WIP.Weight, location + new Vector2(10, 70), Color.Black);
                sb.DrawString(Resources.CourierSmall, "Rate: " + host.TBackend.WIP.FireRate, location + new Vector2(110, 40), Color.Black);
                sb.DrawString(Resources.CourierSmall, "Acc:  " + host.TBackend.WIP.Accuracy, location + new Vector2(110, 70), Color.Black);
                
            }
            if(bt != TinkerBackend.BitType.None) {
                List<GunBits> bits = null;
                Rectangle? srcRect = null;
                switch(bt) {
                    case TinkerBackend.BitType.Barrel:
                        bits = host.TBackend.GetBits<GunBarrel>().ToList<GunBits>();
                        srcRect = SurviveGame.gunImagesList["GunBarrel"];
                        break;
                    case TinkerBackend.BitType.Body:
                        bits = host.TBackend.GetBits<GunBody>().ToList<GunBits>();
                        srcRect = SurviveGame.gunImagesList["GunBody"];
                        break;
                    case TinkerBackend.BitType.Clip:
                        bits = host.TBackend.GetBits<GunClip>().ToList<GunBits>();
                        srcRect = SurviveGame.gunImagesList["GunClip"];
                        break;
                    case TinkerBackend.BitType.Scope:
                        bits = host.TBackend.GetBits<GunScope>().ToList<GunBits>();
                        srcRect = SurviveGame.gunImagesList["GunScope"];
                        break;
                    case TinkerBackend.BitType.Stock:
                        bits = host.TBackend.GetBits<GunStock>().ToList<GunBits>();
                        srcRect = SurviveGame.gunImagesList["GunStock"];
                        break;
                }
                for(int i = 0; i < bits.Count; i++) {
                    GunBits thisBit = bits[i];
                    Vector2 location = new Vector2(600, i * 100 - partScrollY);
                    sb.Draw(Resources.Tinker.Box, location, Color.White);
                    sb.Draw(Resources.GunSheet, location + new Vector2(10, 10), srcRect, Color.White);

                    sb.DrawString(Resources.Courier, bt.ToString(), location + new Vector2(50, 10), Color.Black);

                    Vector2[] slots = new Vector2[] { new Vector2(10, 40), new Vector2(10, 70), new Vector2(110, 40), new Vector2(110, 70) };

                    switch(bt) {
                        case TinkerBackend.BitType.Barrel:
                            sb.DrawString(Resources.CourierSmall, "Power:  " + thisBit.AttackPower, location + slots[0], Color.Black);
                            sb.DrawString(Resources.CourierSmall, "Acc:    " + thisBit.Accuracy, location + slots[1], Color.Black);
                            sb.DrawString(Resources.CourierSmall, "Weight: " + thisBit.Weight, location + slots[3], Color.Black);
                            break;
                        case TinkerBackend.BitType.Body:
                            sb.DrawString(Resources.CourierSmall, "Power:  " + thisBit.AttackPower, location + slots[0], Color.Black);
                            sb.DrawString(Resources.CourierSmall, "Acc:    " + thisBit.Accuracy, location + slots[1], Color.Black);
                            sb.DrawString(Resources.CourierSmall, "Rate:   " + thisBit.FireRate, location + slots[2], Color.Black);
                            sb.DrawString(Resources.CourierSmall, "Weight: " + thisBit.Weight, location + slots[3], Color.Black);
                            break;
                        case TinkerBackend.BitType.Clip:
                            sb.DrawString(Resources.CourierSmall, "ReloadSpd: " + thisBit.ReloadSpeed, location + slots[0], Color.Black);
                            sb.DrawString(Resources.CourierSmall, "Capacity:  " + thisBit.ClipCapacity, location + slots[1], Color.Black);
                            break;
                        case TinkerBackend.BitType.Scope:
                            sb.DrawString(Resources.Courier, "Acc: " + thisBit.Accuracy, location + slots[0], Color.Black);
                            break;
                        case TinkerBackend.BitType.Stock:
                            sb.DrawString(Resources.CourierSmall, "Acc:    " + thisBit.Accuracy, location + slots[0], Color.Black);
                            sb.DrawString(Resources.CourierSmall, "Weight: " + thisBit.Weight, location + slots[1], Color.Black);
                            break;
                    }
                    
                }
            }
        }
    }
}
