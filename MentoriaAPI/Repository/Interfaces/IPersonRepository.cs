using MentoriaAPI.Models;

namespace MentoriaAPI.Repository.Interfaces
{
    public interface IPersonRepository
    {
        public List<Person> ListPerson();
        public Person GetPersonById(int id);
        public string DeletePerson(int id);
        public string CreateOrUpdatePerson(Person person);
    }
}
