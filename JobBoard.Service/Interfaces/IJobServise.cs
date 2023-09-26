using JobBoard.Service.DTOs.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Service.Interfaces
{
    public interface IJobService
    {
        public Task<bool> DeleteAsync(long id);
        public Task<JobForResultDto> GetById(long id);
        public Task<List<JobForResultDto>> GetAllAsync();
        public Task<JobForResultDto> UpdateAsync(JobForUpdateDto dto);
        public Task<JobForResultDto> CreateAsync(JobForCreationDto dto);
    }
}
