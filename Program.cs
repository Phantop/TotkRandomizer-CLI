using Cead;
using Cead.Interop;
using CsRestbl;
using Native.IO.Services;
using System.ComponentModel;
using System.Media;
using TotkRSTB;
using static TotkRandomizer.ActorList;

namespace TotkRandomizer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length <= 0) {
                Console.WriteLine("Please provide your TotK romfs path.");
                return;
            }

            DllManager.LoadCead();

            currentProgress = 0;
            maxProgress = 0;

            HashTable.InitHashTable(Path.Combine(args[0], "Pack", "ZsDic.pack.zs"));

            doRando(args[0]);
        }

        private static int currentProgress = 0;
        private static int maxProgress = 0;

        private static int currentChest = 0;
        private static int chestCount = 1531;

        private static Dictionary<string, uint> rstbModifiedTable = new Dictionary<string, uint>();

        private static string randomizerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "romfs");

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.TopDirectoryOnly))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                maxProgress++;
            }
        }

        private enum EnemyWeaponSet
        {
            LargeSwordOrSpear,
            SwordAndShield,
            BowAndArrows,
        }

        private static Byml ReplaceEnemy(Byml actor)
        {
            // Replace Enemy
            string gyamlValue = actor.GetHash()["Gyaml"].GetString();

            if (actor.GetHash().ContainsKey("Dynamic"))
            {
                Byml.Hash dynamicArray = actor.GetHash()["Dynamic"].GetHash();

                foreach (KeyValuePair<List<string>, List<List<string>>> enemyDictList in EnemyReplaceList)
                {
                    foreach (string enemyList in enemyDictList.Key)
                    {
                        if (gyamlValue.Equals(enemyList))
                        {
                            List<string> enemyTypeList = enemyDictList.Value[RNG.Next(enemyDictList.Value.Count)];
                            string newEnemyName = enemyTypeList[RNG.Next(enemyTypeList.Count)];
                            actor.GetHash()["Gyaml"] = newEnemyName;

                            if (actor.GetHash().ContainsKey("Rails"))
                            {
                                actor.GetHash().Remove("Rails");
                            }

                            // Remove Existing Weapons & Attachments
                            if (dynamicArray.ContainsKey("EquipmentUser_Weapon"))
                            {
                                dynamicArray.Remove("EquipmentUser_Weapon");
                            }

                            if (dynamicArray.ContainsKey("EquipmentUser_Shield"))
                            {
                                dynamicArray.Remove("EquipmentUser_Shield");
                            }

                            if (dynamicArray.ContainsKey("EquipmentUser_Bow"))
                            {
                                dynamicArray.Remove("EquipmentUser_Bow");
                            }

                            if (dynamicArray.ContainsKey("EquipmentUser_Attachment_Weapon"))
                            {
                                dynamicArray.Remove("EquipmentUser_Attachment_Weapon");
                            }

                            if (dynamicArray.ContainsKey("EquipmentUser_Attachment_Shield"))
                            {
                                dynamicArray.Remove("EquipmentUser_Attachment_Shield");
                            }

                            if (dynamicArray.ContainsKey("EquipmentUser_Attachment_Arrow"))
                            {
                                dynamicArray.Remove("EquipmentUser_Attachment_Arrow");
                            }

                            if (dynamicArray.ContainsKey("Equipment_Attachment"))
                            {
                                dynamicArray.Remove("Equipment_Attachment");
                            }

                            if (dynamicArray.ContainsKey("EquipmentUser_Accessory1"))
                            {
                                dynamicArray.Remove("EquipmentUser_Accessory1");
                            }

                            if (dynamicArray.ContainsKey("EquipmentUser_Tool"))
                            {
                                dynamicArray.Remove("EquipmentUser_Tool");
                            }

                            if (dynamicArray.ContainsKey("IsSoundAlarmOnDiscoverBattleTarget"))
                            {
                                dynamicArray.Remove("IsSoundAlarmOnDiscoverBattleTarget");
                            }

                            if (dynamicArray.ContainsKey("IsSoundAlarmOnDiscoverBattleTarget"))
                            {
                                dynamicArray.Remove("IsSoundAlarmOnDiscoverBattleTarget");
                            }

                            if (dynamicArray.ContainsKey("Role"))
                            {
                                dynamicArray.Remove("Role");
                            }

                            if (dynamicArray.ContainsKey("Rider_RidingTarget"))
                            {
                                dynamicArray.Remove("Rider_RidingTarget");
                            }

                            // Add New Weapons & Attachments
                            if (newEnemyName.StartsWith("Enemy_Bokoblin_") ||
                                    newEnemyName.StartsWith("Enemy_Zonau_Robot_") ||
                                    newEnemyName.StartsWith("Enemy_Horablin_") ||
                                    newEnemyName.StartsWith("Enemy_Moriblin_") ||
                                    newEnemyName.StartsWith("Enemy_Lizalfos_") ||
                                    newEnemyName.StartsWith("Enemy_Lynel_") ||
                                    newEnemyName.StartsWith("Enemy_Wizzrobe_"))
                            {
                                Array values = Enum.GetValues(typeof(EnemyWeaponSet));
                                EnemyWeaponSet randomWeaponSet = (EnemyWeaponSet)values.GetValue(RNG.Next(values.Length));

                                switch (randomWeaponSet)
                                {
                                    case EnemyWeaponSet.LargeSwordOrSpear:
                                        TwoHandedWeaponList.Shuffle();
                                        dynamicArray.Add("EquipmentUser_Weapon", TwoHandedWeaponList[0]);

                                        AttachmentList.Shuffle();
                                        dynamicArray.Add("EquipmentUser_Attachment_Weapon", AttachmentList[0]);
                                        break;

                                    case EnemyWeaponSet.SwordAndShield:
                                        OneHandedWeaponList.Shuffle();
                                        dynamicArray.Add("EquipmentUser_Weapon", OneHandedWeaponList[0]);

                                        ShieldList.Shuffle();
                                        dynamicArray.Add("EquipmentUser_Shield", ShieldList[0]);

                                        AttachmentList.Shuffle();
                                        dynamicArray.Add("EquipmentUser_Attachment_Weapon", AttachmentList[0]);

                                        AttachmentList.Shuffle();
                                        dynamicArray.Add("EquipmentUser_Attachment_Shield", AttachmentList[0]);
                                        break;

                                    case EnemyWeaponSet.BowAndArrows:
                                        BowList.Shuffle();
                                        dynamicArray.Add("EquipmentUser_Bow", BowList[0]);

                                        ArrowAttachmentList.Shuffle();
                                        dynamicArray.Add("EquipmentUser_Attachment_Arrow", ArrowAttachmentList[0]);
                                        break;
                                }
                            }

                            return actor;
                        }
                    }
                }
            }

            return actor;
        }

        private static Byml ReplaceChest(Byml actor)
        {
            string gyamlValue = actor.GetHash()["Gyaml"].GetString();

            if (gyamlValue.StartsWith("TBox_"))
            {
                if (actor.GetHash().ContainsKey("Dynamic"))
                {
                    Byml.Hash dynamicArray = actor.GetHash()["Dynamic"].GetHash();

                    if (dynamicArray.ContainsKey("Drop__DropActor_Attachment"))
                    {
                        actor.GetHash()["Dynamic"].GetHash().Remove("Drop__DropActor_Attachment");
                    }

                    if (dynamicArray.ContainsKey("Drop__DropActor"))
                    {
                        string dropValue = actor.GetHash()["Dynamic"].GetHash()["Drop__DropActor"].GetString();
                        if (dropValue.Equals("KeySmall"))
                        {
                            return actor;
                        }

                        actor.GetHash()["Dynamic"].GetHash()["Drop__DropActor"] = ChestList.ChestNumberList[currentChest];

                        string newDropActor = actor.GetHash()["Dynamic"].GetHash()["Drop__DropActor"].GetString();
                        if (newDropActor.StartsWith("Weapon_") && !newDropActor.Contains("_Bow_"))
                        {
                            AttachmentList.Shuffle();

                            if (!dynamicArray.ContainsKey("Drop__DropActor_Attachment"))
                            {
                                actor.GetHash()["Dynamic"].GetHash().Add("Drop__DropActor_Attachment", AttachmentList[0]);
                            }

                            actor.GetHash()["Dynamic"].GetHash()["Drop__DropActor_Attachment"] = AttachmentList[0];
                        }
                    }

                    currentChest++;
                }
            }

            return actor;
        }

        private static Byml ReplaceDogs(Byml actor)
        {
            // Replace Dogs
            string gyamlValue = actor.GetHash()["Gyaml"].GetString();

            if (gyamlValue.StartsWith("Animal_Dog_"))
            {
                actor.GetHash()["Gyaml"] = "Enemy_Lynel_Dark";

                Byml.Hash dynamicArray = actor.GetHash()["Dynamic"].GetHash();

                if (actor.GetHash().ContainsKey("Rails"))
                {
                    actor.GetHash().Remove("Rails");
                }

                Array values = Enum.GetValues(typeof(EnemyWeaponSet));
                EnemyWeaponSet randomWeaponSet = (EnemyWeaponSet)values.GetValue(RNG.Next(values.Length));

                switch (randomWeaponSet)
                {
                    case EnemyWeaponSet.LargeSwordOrSpear:
                        TwoHandedWeaponList.Shuffle();
                        dynamicArray.Add("EquipmentUser_Weapon", TwoHandedWeaponList[0]);

                        AttachmentList.Shuffle();
                        dynamicArray.Add("EquipmentUser_Attachment_Weapon", AttachmentList[0]);
                        break;

                    case EnemyWeaponSet.SwordAndShield:
                        OneHandedWeaponList.Shuffle();
                        dynamicArray.Add("EquipmentUser_Weapon", OneHandedWeaponList[0]);

                        ShieldList.Shuffle();
                        dynamicArray.Add("EquipmentUser_Shield", ShieldList[0]);

                        AttachmentList.Shuffle();
                        dynamicArray.Add("EquipmentUser_Attachment_Weapon", AttachmentList[0]);

                        AttachmentList.Shuffle();
                        dynamicArray.Add("EquipmentUser_Attachment_Shield", AttachmentList[0]);
                        break;

                    case EnemyWeaponSet.BowAndArrows:
                        BowList.Shuffle();
                        dynamicArray.Add("EquipmentUser_Bow", BowList[0]);

                        ArrowAttachmentList.Shuffle();
                        dynamicArray.Add("EquipmentUser_Attachment_Arrow", ArrowAttachmentList[0]);
                        break;
                }

                return actor;
            }

            return actor;
        }

        private static Byml ReplaceBasics(Byml actor)
        {
            // Replace Basic Objects
            string gyamlValue = actor.GetHash()["Gyaml"].GetString();

            foreach (List<string> basicObjectList in BasicObjectsReplaceList)
            {
                foreach (string basicObject in basicObjectList)
                {
                    if (gyamlValue.Equals(basicObject))
                    {
                        basicObjectList.Shuffle();
                        actor.GetHash()["Gyaml"] = basicObjectList[0];

                        return actor;
                    }
                }
            }

            return actor;
        }

        private static Byml ReplaceFloorWeapon(Byml actor)
        {
            // Replace Floor Weapon
            string gyamlValue = actor.GetHash()["Gyaml"].GetString();

            if (gyamlValue.StartsWith("Weapon_"))
            {
                if (gyamlValue.Contains("_Shield_"))
                {
                    ShieldList.Shuffle();
                    actor.GetHash()["Gyaml"] = ShieldList[0];
                }
                else if (gyamlValue.Contains("_Bow_"))
                {
                    BowList.Shuffle();
                    actor.GetHash()["Gyaml"] = BowList[0];
                }
                else if (gyamlValue.Equals("Weapon_Sword_071_Broken"))
                {
                    SharpWeaponList.Shuffle();
                    actor.GetHash()["Gyaml"] = SharpWeaponList[0];
                    return actor;
                }
                else
                {
                    WeaponList.Shuffle();
                    actor.GetHash()["Gyaml"] = WeaponList[0];
                }

                if (actor.GetHash().ContainsKey("Dynamic"))
                {
                    Byml.Hash dynamicArray = actor.GetHash()["Dynamic"].GetHash();

                    if (!gyamlValue.Contains("_Bow_"))
                    {
                        AttachmentList.Shuffle();

                        if (!dynamicArray.ContainsKey("Equipment_Attachment"))
                        {
                            actor.GetHash()["Dynamic"].GetHash().Add("Equipment_Attachment", AttachmentList[0]);
                        }

                        actor.GetHash()["Dynamic"].GetHash()["Equipment_Attachment"] = AttachmentList[0];
                    }
                }
            }

            return actor;
        }

        private static void doRando(string gamePath)
        {
            string largeDungeonPath = Path.Combine(gamePath, "Banc", "LargeDungeon");
            string mainFieldPath = Path.Combine(gamePath, "Banc", "MainField");
            string mainFieldCastlePath = Path.Combine(gamePath, "Banc", "MainField", "Castle");
            string mainFieldCavePath = Path.Combine(gamePath, "Banc", "MainField", "Cave");
            string mainFieldLargeDungeonPath = Path.Combine(gamePath, "Banc", "MainField", "LargeDungeon");
            string mainFieldSkyPath = Path.Combine(gamePath, "Banc", "MainField", "Sky");
            string minusFieldPath = Path.Combine(gamePath, "Banc", "MinusField");
            string minusFieldLargeDungeonPath = Path.Combine(gamePath, "Banc", "MinusField", "LargeDungeon");
            string smallDungeonPath = Path.Combine(gamePath, "Banc", "SmallDungeon");

            currentChest = 0;
            ChestList.InitChestNumberList(chestCount);

            RandomizeCutscenes();

            string[] mapFiles = new string[] {
                mainFieldPath,
                    largeDungeonPath,
                    mainFieldCastlePath,
                    mainFieldCavePath,
                    mainFieldLargeDungeonPath,
                    mainFieldSkyPath,
                    minusFieldPath,
                    minusFieldLargeDungeonPath,
                    smallDungeonPath
            };

            string romfsEnd;
            string finalPath;

            foreach (string mapFilePath in mapFiles)
            {
                romfsEnd = mapFilePath.Replace(gamePath, "");
                finalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "romfs", romfsEnd);
                Directory.CreateDirectory(finalPath);
                CopyFilesRecursively(mapFilePath, finalPath);
            }

            rstbModifiedTable.Clear();

            string resourceFolderPath = Path.Combine(gamePath, "System", "Resource");
            romfsEnd = resourceFolderPath.Replace(gamePath, "");
            finalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "romfs", romfsEnd);
            Directory.CreateDirectory(finalPath);
            CopyFilesRecursively(resourceFolderPath, finalPath);

            string rstbFile = Path.Combine(randomizerPath, "System", "Resource");
            string mapfilesPath = Path.Combine(randomizerPath, "Banc");

            rstbFile = Directory.GetFiles(rstbFile, "*.rsizetable.zs")[0];

            string[] allFiles = Directory.GetFiles(mapfilesPath, "*.bcett.byml.zs", SearchOption.AllDirectories);
            foreach (string mapFile in allFiles)
            {
                Span<byte> myByteArray = HashTable.DecompressMapData(File.ReadAllBytes(mapFile));
                Byml byaml = Byml.FromBinary(myByteArray);

                // For every object in the map, randomize it!
                Byml.Hash byamlFileArray = byaml.GetHash();

                if (byamlFileArray.ContainsKey("Actors"))
                {
                    Byml.Array actorList = byamlFileArray["Actors"].GetArray();

                    for (int i = 0; i < actorList.Length; i++)
                    {
                        actorList[i] = ReplaceFloorWeapon(actorList[i]);
                        actorList[i] = ReplaceEnemy(actorList[i]);
                        actorList[i] = ReplaceChest(actorList[i]);

                        actorList[i] = ReplaceBasics(actorList[i]);

                        //actorList[i] = ReplaceDogs(actorList[i]);
                    }
                }

                byte[] fs1Array = byaml.ToBinary(false, 7).ToArray();

                string rstbPath = Path.GetDirectoryName(mapFile).Replace(randomizerPath, "").Replace("romfs\\", "").Replace("\\", "/")[1..] + "/" + Path.GetFileNameWithoutExtension(mapFile);
                rstbModifiedTable.Add(rstbPath, (uint)(fs1Array.Length + 20000));

                File.WriteAllBytes(mapFile, HashTable.CompressMapData(fs1Array));
                
                currentProgress++;

                Console.WriteLine(currentProgress + "/" + maxProgress);
            }

            //rstbModifiedTable.Add("Event/EventFlow/DefeatGanondorf.110.bfevfl", 200000);
            //rstbModifiedTable.Add("Event/EventFlow/DefeatGanondorf.bfevfl", 200000);

            //RSTB Table
            Restbl rstbFileData = Restbl.FromBinary(HashTable.DecompressFile(File.ReadAllBytes(rstbFile)));

            for (int i = 0; i < rstbModifiedTable.Keys.Count; i++)
            {
                rstbFileData.NameTable[rstbModifiedTable.Keys.ElementAt(i)] = rstbModifiedTable.Values.ElementAt(i);
            }

            byte[] compressedRSTB = HashTable.CompressDataOther(rstbFileData.ToBinary().ToArray());
            File.WriteAllBytes(rstbFile, compressedRSTB);
        }

        private static void RandomizeCutscenes()
        {
            string customEventFlowFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Events");

            string finalPath = Path.Combine(customEventFlowFolder, "..", "romfs", "Event", "EventFlow");
            Directory.CreateDirectory(finalPath);

            foreach (string cutsceneFile in Directory.GetFiles(customEventFlowFolder, "*.zs"))
            {
                finalPath = Path.Combine(customEventFlowFolder, "..", "romfs", "Event", "EventFlow", Path.GetFileName(cutsceneFile));
                File.Copy(cutsceneFile, finalPath, true);
            }
        }
    }
}
