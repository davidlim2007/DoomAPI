using Microsoft.AspNetCore.Mvc;
using DoomAPI.Models;
using System.Text.Json;
// See :
// How to serialize and deserialize (marshal and unmarshal) JSON in .NET
// https://learn.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/json-and-xml-serialization

namespace DoomAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponsController : ControllerBase
    {
        private readonly ILogger<WeaponsController> _logger;

        public WeaponsController(ILogger<WeaponsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Weapon> Get()
        {
            List<Weapon> weaponsList;

            if (System.IO.File.Exists(".\\database.json"))
            {
                // If JSON file exists, read it and de-serialize into
                // an array of WeatherForecast objects.
                string strJson = System.IO.File.ReadAllText(".\\database.json");
                weaponsList = JsonSerializer.Deserialize<List<Weapon>>(strJson);
            }
            else
            {
                // If JSON file does not exist, get a pre-set list of
                // default Weapon objects.
                weaponsList = GetDefaultWeapons();

                // Save the set of Weapon objects into a JSON File.
                string jsonString = JsonSerializer.Serialize(weaponsList);
                System.IO.File.WriteAllText(".\\database.json", jsonString);
            }

            return weaponsList.ToArray();
        }

        [HttpPost]
        public IActionResult Post(Weapon data)
        {
            List<Weapon> weaponsList;

            if (System.IO.File.Exists(".\\database.json"))
            {
                // If JSON file exists, read it and de-serialize into
                // an array of WeatherForecast objects.
                string strJson = System.IO.File.ReadAllText(".\\database.json");
                weaponsList = JsonSerializer.Deserialize<List<Weapon>>(strJson);
            }
            else
            {
                // If JSON file does not exist, get a pre-set list of
                // default Weapon objects.
                weaponsList = GetDefaultWeapons();
            }

            Weapon weapon = weaponsList.FirstOrDefault(i => i.Id == data.Id);

            if (weapon != null)
            {
                return BadRequest("Id already exists.");
            }

            weaponsList.Add(data);

            // The updated weaponsList is saved into the database JSON file.
            string jsonString = JsonSerializer.Serialize(weaponsList.ToArray());
            System.IO.File.WriteAllText(".\\database.json", jsonString);

            return Ok(data);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Weapon data)
        {
            List<Weapon> weaponsList;

            if (System.IO.File.Exists(".\\database.json"))
            {
                // If JSON file exists, read it and de-serialize into
                // an array of WeatherForecast objects.
                string strJson = System.IO.File.ReadAllText(".\\database.json");
                weaponsList = JsonSerializer.Deserialize<List<Weapon>>(strJson);
            }
            else
            {
                // If JSON file does not exist, get a pre-set list of
                // default Weapon objects.
                weaponsList = GetDefaultWeapons();
            }

            Weapon weapon = weaponsList.FirstOrDefault(i => i.Id == id);

            if (weapon == null)
            {
                return BadRequest("Invalid Id.");
            }

            weaponsList.Remove(weapon);

            // Check which fields of the input Weapon data have been filled,
            // and replace those fields in the Weapon object to update.
            weapon.Name = (String.IsNullOrEmpty(data.Name)) ? weapon.Name : data.Name;
            weapon.Description = (String.IsNullOrEmpty(data.Description)) ? weapon.Description : data.Description;
            weapon.DamagePerShot = (String.IsNullOrEmpty(data.DamagePerShot.ToString())) ? weapon.DamagePerShot : data.DamagePerShot;
            weapon.RateOfFire = (String.IsNullOrEmpty(data.RateOfFire)) ? weapon.RateOfFire : data.RateOfFire;
            weapon.Mods = (data.Mods == null) ? weapon.Mods : data.Mods;

            weaponsList.Add(weapon);

            // The updated weaponsList is saved into the database JSON file.
            string jsonString = JsonSerializer.Serialize(weaponsList.ToArray());
            System.IO.File.WriteAllText(".\\database.json", jsonString);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            List<Weapon> weaponsList;

            if (System.IO.File.Exists(".\\database.json"))
            {
                // If JSON file exists, read it and de-serialize into
                // an array of WeatherForecast objects.
                string strJson = System.IO.File.ReadAllText(".\\database.json");
                weaponsList = JsonSerializer.Deserialize<List<Weapon>>(strJson);
            }
            else
            {
                // If JSON file does not exist, get a pre-set list of
                // default Weapon objects.
                weaponsList = GetDefaultWeapons();
            }

            Weapon weapon = weaponsList.FirstOrDefault(i => i.Id == id);

            if (weapon == null)
            {
                return BadRequest("Invalid Id.");
            }

            weaponsList.Remove(weapon);

            // The updated weaponsList is saved into the database JSON file.
            string jsonString = JsonSerializer.Serialize(weaponsList.ToArray());
            System.IO.File.WriteAllText(".\\database.json", jsonString);

            return Ok();
        }

        private List<Weapon> GetDefaultWeapons()
        {
            return new List<Weapon>
            {
                new Weapon(1, "Super Shotgun", "A powerful Double-Barrel Shotgun with a grappling hook", 1600, "Low",
                    new List<string>{ "Meathook" }),

                new Weapon(2, "Rocket Launcher", "An explosive rocket-firing cannon", 900, "Low",
                    new List<string>{ "Lock-On Burst", "Remote Detonation" })
            };
        }
    }
}
