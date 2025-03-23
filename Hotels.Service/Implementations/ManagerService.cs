﻿using AutoMapper;
using Hotels.Models.Dtos.Manager;
using Hotels.Models.Dtos.Managers;
using Hotels.Models.Entities;
using Hotels.Repository.Implementations;
using Hotels.Repository.Interfaces;
using Hotels.Service.Exceptions;
using Hotels.Service.Interfaces;

namespace Hotels.Service.Implementations
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public ManagerService(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
            
        }
        public async Task AddNewManager(ManagerAddingDto managerAddingDto)
        {
            var obj = _mapper.Map<Manager>(managerAddingDto);
            await _managerRepository.AddAsync(obj);
        }

        public async Task DeleteManager(int id)
        {
            var managerToDelete = await _managerRepository.GetAsync(x => x.Id == id, includeProperties:"Hotel");
            if(managerToDelete.Hotel is not null)
            {
                throw new DeletionNotAllowedException("Manager manages hotel. You can not delete it");
            }

            _managerRepository.Remove(managerToDelete);
        }

        public async Task DeleteManagerWithHotel(int id)
        {
            var managerToDelete = await _managerRepository.GetAsync(x => x.Id == id, includeProperties: "Hotel");
            _managerRepository.Remove(managerToDelete);
        }

        public async Task<List<ManagerGettingDto>> GetAllManagers()
        {
            List<Manager> managers=await _managerRepository.GetAllAsync();

            var result=_mapper.Map<List<ManagerGettingDto>>(managers);
            return result;
         
        }

        public async Task<ManagerGettingDto> GetManager(int id)
        {
            Manager manager= await _managerRepository.GetAsync(x=>x.Id==id);

            var result=_mapper.Map<ManagerGettingDto>(manager);
            return result;
        }

        public async Task<ManagerGettingDto> GetManagerOfHotel(int hotelId)
        {
            Manager manager = await _managerRepository.GetAsync(x => x.HotelId==hotelId);

            var result = _mapper.Map<ManagerGettingDto>(manager);
            return result;
        }

        public async Task SaveManager()
        {
            await _managerRepository.Save();
        }

        public async Task UpdateManager(ManagerUpdatingDto managerUpdatingDto)
        {
            var ManagerToUpdate = await _managerRepository.GetAsync(x => x.Id == managerUpdatingDto.Id);
            var obj = _mapper.Map(managerUpdatingDto, ManagerToUpdate);
            await _managerRepository.Update(obj);
        }
    }
}
