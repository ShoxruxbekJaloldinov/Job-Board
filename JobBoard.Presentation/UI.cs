using JobBoard.Service.DTOs.Jobs;
using JobBoard.Service.Exceptions;
using JobBoard.Service.Interfaces;
using JobBoard.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Presentation
{
    public class UI
    {
        public async Task<string> Print()
        {
            IJobService jobService = new JobService();
            JobForCreationDto dto = new JobForCreationDto();
            JobForUpdateDto dtoForUpdate = new JobForUpdateDto();


            while (true)
            {
                Console.WriteLine("1 -> Create");
                Console.WriteLine("2 -> Update");
                Console.WriteLine("3 -> Delete");
                Console.WriteLine("4 -> GetById");
                Console.WriteLine("5 -> Getall");

                int n = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (n)
                {
                    case 1:
                        #region Create Job
                        try
                        {
                            Console.WriteLine("Enter the JobTitle: ");
                            dto.JobTitle = Console.ReadLine();
                            Console.WriteLine("Enter the Job description: ");
                            dto.Description = Console.ReadLine();
                            Console.WriteLine("Enter the Price of job: ");
                            dto.Price = decimal.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the Conditions of Job: ");
                            dto.Conditions = Console.ReadLine();
                            Console.WriteLine("Enter the Phone: ");
                            dto.Phone = Console.ReadLine();
                            Console.WriteLine("Enter the Company Name");
                            dto.CompanyName = Console.ReadLine();
                            var result = await jobService.CreateAsync(dto);
                            string str = $"{result.Id} | {result.JobTitle} | {result.Description}| {result.Conditions} | {result.CompanyName} | {result.Price} | {result.Phone} |  ";
                            Console.WriteLine(str);
                        }
                        catch (CustomException ex)
                        {
                            Console.WriteLine($"{ex.StatusCode}  {ex.Message}");
                        }
                        #endregion
                        break;
                    case 2:
                        #region Update Job 
                        try
                        {
                            Console.WriteLine("Enter the Id : ");
                            dtoForUpdate.Id = long.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the JobTitle: ");
                            dtoForUpdate.JobTitle = Console.ReadLine();
                            Console.WriteLine("Enter the Job description: ");
                            dtoForUpdate.Description = Console.ReadLine();
                            Console.WriteLine("Enter the Price of job: ");
                            dtoForUpdate.Price = decimal.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the Conditions of Job: ");
                            dtoForUpdate.Conditions = Console.ReadLine();
                            Console.WriteLine("Enter the Phone: ");
                            dtoForUpdate.Phone = Console.ReadLine();
                            Console.WriteLine("Enter the Company Name");
                            dtoForUpdate.CompanyName = Console.ReadLine();
                            var result = await jobService.UpdateAsync(dtoForUpdate);

                            string str = $"{result.Id} | {result.JobTitle} | {result.Description} | {result.CompanyName} | {result.Price} | {result.Phone} |  ";
                            Console.WriteLine(str);
                        }
                        catch (CustomException ex)
                        {
                            Console.WriteLine($"{ex.StatusCode}  {ex.Message}");
                        }
                        #endregion
                        break;
                    case 3:
                        #region Delete Job
                        try
                        {
                            Console.WriteLine("Enter the Id: ");
                            var id = long.Parse(Console.ReadLine());
                            Console.WriteLine(await jobService.DeleteAsync(id));
                        }
                        catch (CustomException ex)
                        {
                            Console.WriteLine($"{ex.StatusCode}     {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        #endregion
                        break;
                    case 4:
                        #region GetById Computer
                        try
                        {
                            Console.WriteLine("Enter the Id");
                            long id = long.Parse(Console.ReadLine());
                            var result = await jobService.GetById(id);
                            string str = $"{result.Id} | {result.JobTitle} | {result.Description} | {result.CompanyName} | {result.Price} | {result.Phone} |  ";
                            Console.WriteLine(str);
                        }
                        catch (CustomException ex)
                        {
                            Console.WriteLine($"{ex.StatusCode}     {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        #endregion
                        break;
                    case 5:
                        #region GetAll Computer
                        try
                        {
                            var jobs = await jobService.GetAllAsync();
                            foreach (var job in jobs)
                            {
                                string str = $"{job.Id} | {job.JobTitle} | {job.Description} | {job.CompanyName} | {job.Price} | {job.Phone} |  ";
                                Console.WriteLine(str);
                            }
                        }
                        catch (CustomException ex)
                        {
                            Console.WriteLine($"{ex.StatusCode}     {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        #endregion
                        break;
                }
                Console.ReadKey(); Console.Clear();
            }
        }
    }
}
