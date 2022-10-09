namespace DoomAPI.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? DamagePerShot { get; set; }
        public string? RateOfFire { get; set; }
        public List<string>? Mods { get; set; }

        public Weapon()
        {

        }

        public Weapon(int id, string name, string description, int damagePerShot, string rateOfFire, List<string> mods)
        {
            Id = id;
            Name = name;
            Description = description;
            DamagePerShot = damagePerShot;
            RateOfFire = rateOfFire;
            Mods = mods;
        }
    }
}
