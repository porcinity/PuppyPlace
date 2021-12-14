// using PuppyPlace.Domain;
// using PuppyPlace.Repository;
//
// namespace PuppyPlace.Service;
//
// public class PersonsService : IPersonsService
// {
//     private readonly IPersonsRepository _personsRepository;
//
//     public PersonsService(IPersonsRepository personsRepository)
//     {
//         _personsRepository = personsRepository;
//     }
//     private static PersonDto ItemToDto(Person person) =>
//         new PersonDto
//         {
//             Id = person.Id, 
//             Name = person.Name,
//             Dogs = person.Dogs
//         };
//     public async Task<IEnumerable<PersonDto>> FindPersons()
//     {
//         var persons = await _personsRepository.FindPersons();
//         return persons.Select(m => ItemToDto(m)).ToList();
//     }
//
//     public async Task<PersonDto> FindPerson(Guid id)
//     {
//         var person = await _personsRepository.FindPerson(id);
//         return ItemToDto(person);
//     }
//
//     public async Task AddPerson(PersonDto personDto)
//     {
//         Person person = new(personDto.Name);
//         await _personsRepository.AddPerson(person);
//     }
//
//     public async Task UpdatePerson(PersonDto personDto)
//     {
//         var person = await _personsRepository.FindPerson(personDto.Id);
//         person.Name = personDto.Name;
//         person.Id = personDto.Id;
//         await _personsRepository.UpdatePerson(person);
//     }
//
//     public async Task DeletePerson(Guid id)
//     {
//         await _personsRepository.DeletePerson(id);
//     }
// }