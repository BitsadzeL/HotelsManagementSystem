﻿using Hotels.Models.Dtos.Manager;
using Hotels.Models.Dtos.Managers;

namespace Hotels.Service.Interfaces
{
    public interface IManagerService
    {
        Task<ManagerGettingDto> GetManager(int id);
        Task<List<ManagerGettingDto>> GetAllManagers();
        Task AddNewManager(ManagerAddingDto managerAddingDto);
        Task UpdateManager(ManagerUpdatingDto managerUpdatingDto);
        Task DeleteManager(int id);
        Task SaveManager();

    }
}
