using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Some.Services
{
    //Implementation for classorette
    public interface ISomeService
    {
        SomeProperty CreateSomething(string firstName, string lastName);
        Task <List<SomeProperty>> GetAll();
        Task Add(SomeProperty item);
        Task Remove(string id);
        Task Clear();
    }

    //type stuff
    public class SomeProperty
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
    //id only prop
    public class RemoveParams
    {
        public string id { get; set; }
    }

    //main class
    public class SomeService : ISomeService
    {
        private List<SomeProperty> _user { get; set; }
        private IDistributedCache _cache { get; set; }

        //DI cache
        public SomeService(IDistributedCache cache)
        {
            _user = new List<SomeProperty>();
            _cache = cache;
        }

        //method1
        public SomeProperty CreateSomething(string firstName, string lastName)
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var id = new String(stringChars);

            SomeProperty newSomebody = new SomeProperty
            {
                id = id,
                firstName = firstName,
                lastName = lastName,
            };
            return newSomebody;
        }
        //method2
        public async Task <List<SomeProperty>> GetAll()
        {
           
            byte[] toByte = await _cache.GetAsync("USER_CACHE");
            if (toByte!=null)
            {
                string toString = Encoding.UTF8.GetString(toByte);
                List<SomeProperty> toList = JsonConvert.DeserializeObject<List<SomeProperty>>(toString);

                return toList;
            }
            else
            {
                string toString = JsonConvert.SerializeObject(_user);
                byte[] toByten = Encoding.UTF8.GetBytes(toString);
                await _cache.SetAsync("USER_CACHE", toByten);
                return _user;
            }
        }
        //method3
        public async Task Add(SomeProperty item)
        {
            var userList = await GetAll();
            userList.Add(item);
            string toString = JsonConvert.SerializeObject(userList);
            byte[] toByte = Encoding.UTF8.GetBytes(toString);
            await _cache.SetAsync("USER_CACHE", toByte);
        }
        //method4
        public async Task Remove(string id)

        {
            var userList = await GetAll();
            userList = userList.Where(n => n.id != id).ToList();
            string toString = JsonConvert.SerializeObject(userList);
            byte[] toByte = Encoding.UTF8.GetBytes(toString);
            await _cache.SetAsync("USER_CACHE", toByte);
        }

        //method5
        public async Task Clear()
        {
            await _cache.RemoveAsync("USER_CACHE");
        }
    }
}