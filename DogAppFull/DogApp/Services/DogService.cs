using DogApp.Abstractions;
using DogApp.Data;
using DogApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogApp.Services
{
    public class DogService : IDogService
    {
        private readonly ApplicationDbContext _context;
            public DogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(string name, int age, string breed, string picture)
        {
            Dog item = new Dog
            {
                Name = name,
                Age = age,
                Breed = breed,
                Picture = picture,
            };
            _context.Dogs.Add(item);
            return _context.SaveChanges() != 0; 
        }

        public Dog GetDogById(int dogId)
        {
            return _context.Dogs.Find(dogId);
        }

        public List<Dog> GetDogs()
        {
            List<Dog> dogs = _context.Dogs.ToList();
            return dogs;
        }

        public bool RemoveById(int dogId)
        {
            var dog = GetDogById(dogId);
            if(dog ==default(Dog))
            {
                return false;
            }
            _context.Remove(dog);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateDog(int dogId, string name, int age, string breed, string picture)
        {
            var dog = GetDogById(dogId);
            if(dog == default(Dog))
            {
                return false;
            }
            dog.Name = name;
            dog.Age = age;
            dog.Breed = breed;
            dog.Picture = picture;
            _context.Update(dog);
            return _context.SaveChanges() != 0;
        }
        public List<Dog> GetDogs(string searchedStringBreed, string searchedStringName)
        {
            List<Dog> dogs = _context.Dogs.ToList();
            if (!String.IsNullOrEmpty(searchedStringBreed) && !String.IsNullOrEmpty(searchedStringName))
            {
                dogs = dogs.Where(d => d.Breed.Contains(searchedStringBreed) && d.Name.Contains(searchedStringName)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchedStringBreed))
            {
                dogs = dogs.Where(d => d.Breed.Contains(searchedStringBreed)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchedStringName))
            {
                dogs = dogs.Where(d => d.Name.Contains(searchedStringName)).ToList();
            }
            return dogs;
        }
    }
}
