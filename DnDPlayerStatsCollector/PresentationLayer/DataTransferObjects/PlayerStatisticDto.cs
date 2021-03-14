namespace Roll20Stats.PresentationLayer.DataTransferObjects
{
    public class PlayerStatisticDto
    {
        public string CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int DamageDealt { get; set; }
        public int DamageTaken { get; set; }
        public int HealingDone { get; set; }
        public string GameName { get; set; }

        public PlayerStatisticDto(string characterId, string characterName, string gameName, int damageDealt, int damageTaken, int healingDone)
        {
            CharacterId = characterId;
            CharacterName = characterName;
            DamageDealt = damageDealt;
            DamageTaken = damageTaken;
            HealingDone = healingDone;
            GameName = gameName;
        }
    }
}
