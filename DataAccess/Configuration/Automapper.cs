using AutoMapper;
using DataAccess.Entities;
using DataAccess.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class Automapper
    {
        public IMapper mapper { get; set; }

        public Automapper()
        {
            mapper = SetMapperConfiguration();
        }

        private IMapper SetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(configuration =>
            {
                //Aqui se crean los mapeos necesarios:

                configuration.CreateMap<EmployeeDTO, Employee>();
                configuration.CreateMap<Employee, EmployeeDTO>();

                configuration.CreateMap<Turn, TurnDTO>();
                configuration.CreateMap<TurnDTO, Turn>();

                configuration.CreateMap<Client, ClientDTO>();
                configuration.CreateMap<ClientDTO, Client>();
            });
            return configuration.CreateMapper();
        }
    }
}
