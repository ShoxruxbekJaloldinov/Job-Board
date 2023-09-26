using JobBoard.Data.IRepositories;
using JobBoard.Data.Repositories;
using JobBoard.Domain.Entities;
using JobBoard.Service.DTOs.Jobs;
using JobBoard.Service.Exceptions;
using JobBoard.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Service.Services
{
    public class JobService : IJobService
    {
        private long _id;
        private readonly IRepository<Job> jobRepository = new Repository<Job>();
        public async Task<JobForResultDto> CreateAsync(JobForCreationDto dto)
        {
            var job = (await jobRepository.SelectAllAsync())
                .FirstOrDefault(j => j.JobTitle == dto.JobTitle);
            if (job != null)
                throw new CustomException(409, "Student is already exist");

            await GenerateIdAsync();
            Job jobMapped = new Job()
            {
                Id = _id,
                JobTitle = dto.JobTitle,
                Description = dto.Description,
                Conditions = dto.Conditions,
                Phone = dto.Phone,
                Price = dto.Price,
                CompanyName = dto.CompanyName,
                CreatedAt = DateTime.UtcNow
            };

            await jobRepository.InsertAsync(jobMapped);

            var result = new JobForResultDto()
            {
                Id = _id,
                JobTitle = dto.JobTitle,
                Description = dto.Description,
                Conditions = dto.Conditions,
                CompanyName = dto.CompanyName,
                Phone = dto.Phone,
                Price = dto.Price,
            };

            return result;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var job = await jobRepository.SelectByIdAsync(id);
            if (job is null)
                throw new CustomException(404, "Student is not found");

            await jobRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<JobForResultDto>> GetAllAsync()
        {
            var jobs = await jobRepository.SelectAllAsync();
            List<JobForResultDto> mappedJobs = new List<JobForResultDto>();

            foreach (var job in jobs)
            {
                var dto = new JobForResultDto()
                {
                    Id = _id,
                    JobTitle = job.JobTitle,
                    Description = job.Description,
                    Phone = job.Phone,
                    Price = job.Price,
                    Conditions = job.Conditions,
                    CompanyName = job.CompanyName
                };
                mappedJobs.Add(dto);
            }

            return mappedJobs;
        }

        public async Task<JobForResultDto> GetById(long id)
        {
            var job = await jobRepository.SelectByIdAsync(id);
            if (job is null)
                throw new CustomException(404, "" +
                    "User is not found!");

            var result = new JobForResultDto()
            {
                Id = _id,
                JobTitle = job.JobTitle,
                Description = job.Description,
                Phone = job.Phone,
                Price = job.Price,
            };

            return result;
        }

        public async Task<JobForResultDto> UpdateAsync(JobForUpdateDto dto)
        {
            var job = await jobRepository.SelectByIdAsync(dto.Id);
            if (job is null)
                throw new CustomException(404, "User is not found");

            var mappedJob = new Job()
            {
                Id = dto.Id,
                JobTitle = dto.JobTitle,
                Description = dto.Description,
                Phone = dto.Phone,
                Price = dto.Price,
                UpdatedAt = DateTime.UtcNow
            };

            await jobRepository.UpdateAsync(mappedJob);

            var result = new JobForResultDto()
            {
                Id = mappedJob.Id,
                JobTitle = mappedJob.JobTitle,
                Description = mappedJob.Description,
                Phone = mappedJob.Phone,
                Price = mappedJob.Price,
            };
            return result;
        }
        public async Task GenerateIdAsync()
        {
            var jobs = await jobRepository.SelectAllAsync();
            if (jobs.Count == 0)
            {
                _id = 1;
            }
            else
            {
                var job = jobs[jobs.Count - 1];
                _id = ++job.Id;
            }
        }
    }
}
